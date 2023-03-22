using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlanetData", fileName = "PlanetData", order = 0)]
public class PlanetData : ScriptableObject
{
    private string planetName;
    public string PlanetName => planetName;
    private List<StaticResourceData> planetShippableResources;
    public List<StaticResourceData> PlanetShippableResources => planetShippableResources;
    private List<StaticResourceData> planetNaturalResources;
    public List<StaticResourceData> PlanetNaturalResources => planetNaturalResources;
    
    private Dictionary<string, DynamicResourceData> planetShippableResourceAmounts;
    private Dictionary<string, DynamicResourceData> planetNaturalResourceAmounts;

    private Vector3 planetPosition;
    public Vector3 PlanetPosition => planetPosition;

    private bool planetSettled = false;
    public bool PlanetSettled => planetSettled;

    public Action NewResourceAdded;

    private float planetRadius;
    public float PlanetRadius => planetRadius;
    private float planetPopulation = 0.0f;

    public void PopulatePlanetData(string _planetName, Vector3 _planetPosition, float _planetRadius, StaticResourceData _planetPopulation, List<StaticResourceData> _planetNaturalResources)
    {
        planetRadius = _planetRadius;
        planetPosition = _planetPosition;
        planetName = _planetName;
        planetSettled = _planetPopulation.StartingResourceAmount > 0;
        planetPopulation = _planetPopulation.StartingResourceAmount;
        
        planetShippableResources = new List<StaticResourceData>();
        planetShippableResources.Add(_planetPopulation);
        planetNaturalResources = _planetNaturalResources;
        planetShippableResourceAmounts = new Dictionary<string, DynamicResourceData>();
        planetNaturalResourceAmounts = new Dictionary<string, DynamicResourceData>();
        foreach (StaticResourceData resourceData in planetShippableResources)
        {
            DynamicResourceData newDynamicResourceData = ScriptableObject.CreateInstance<DynamicResourceData>();
            newDynamicResourceData.PopulateDynamicResourceData(resourceData);
            planetShippableResourceAmounts.Add(resourceData.ResourceName, newDynamicResourceData);
        }
        foreach (StaticResourceData resourceData in planetNaturalResources)
        {
            DynamicResourceData newDynamicResourceData = ScriptableObject.CreateInstance<DynamicResourceData>();
            newDynamicResourceData.PopulateDynamicResourceData(resourceData);
            planetNaturalResourceAmounts.Add(resourceData.ResourceName, newDynamicResourceData);
        }
    }

    public float GetNormalizedTimeTillResourceDepleted(string resourceName)
    {
        return planetNaturalResourceAmounts[resourceName].NormalizedAmountResourceHasDepleted();
    }

    /*public float GetNormalizedTimeTillNextResourceGain(string resourceName)
    {
        return planetResourceAmounts[resourceName].NormalizedTimeReachNewResource();
    }*/

    public float GetShippablePlanetResourceAmount(string resourceName)
    {
        return planetShippableResourceAmounts[resourceName].Amount;
    }

    public float GetNaturalPlanetResourceAmount(string resourceName)
    {
        return planetNaturalResourceAmounts[resourceName].Amount;
    }

    public void TickNaturalPlanetResources()
    {
        foreach (DynamicResourceData dynamicResourceData in planetNaturalResourceAmounts.Values)
        {
            dynamicResourceData.TickNaturalResource(planetPopulation);
        }
    }

    public void AddShippableResource(string resourceName, float amount)
    {
        planetSettled = true;
        if (planetShippableResourceAmounts.ContainsKey(resourceName))
        {
            planetShippableResourceAmounts[resourceName].AddCustomAmount(amount);
        }
        else
        {
            StaticResourceData newResourceData = GameManager.Instance.ResourceManager.GetResourceData(resourceName);
            planetShippableResources.Add(newResourceData);
            DynamicResourceData newDynamicResourceData = ScriptableObject.CreateInstance<DynamicResourceData>();
            newDynamicResourceData.PopulateDynamicResourceData(newResourceData, amount);
            planetShippableResourceAmounts.Add(resourceName, newDynamicResourceData);
            planetShippableResourceAmounts[resourceName].AddCustomAmount(amount);
            NewResourceAdded.Invoke();
        }
    }

    public void RemoveShippableResource(string resourceName, float amount)
    {
        if (GetShippablePlanetResourceAmount(resourceName) < amount)
        {
            amount = amount - GetShippablePlanetResourceAmount(resourceName);
        }
        planetShippableResourceAmounts[resourceName].RemoveCustomAmount(amount, true);
    }
    
    public void RemoveNaturalResource(string resourceName, float amount)
    {
        if (GetNaturalPlanetResourceAmount(resourceName) < amount)
        {
            amount = amount - GetNaturalPlanetResourceAmount(resourceName);
        }
        planetNaturalResourceAmounts[resourceName].RemoveCustomAmount(amount, true);
    }
}
