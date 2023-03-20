using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlanetData", fileName = "PlanetData", order = 0)]
public class PlanetData : ScriptableObject
{
    private string planetName;
    public string PlanetName => planetName;
    private List<StaticResourceData> planetResources;
    public List<StaticResourceData> PlanetResources => planetResources;
    private Dictionary<string, DynamicResourceData> planetResourceAmounts;

    private Vector3 planetPosition;
    public Vector3 PlanetPosition => planetPosition;

    private bool planetSettled = false;
    public bool PlanetSettled => planetSettled;

    public Action NewResourceAdded;

    private float planetRadius;
    public float PlanetRadius => planetRadius;

    public void PopulatePlanetData(string _planetName, Vector3 _planetPosition, float _planetRadius, List<StaticResourceData> _planetResources)
    {
        planetRadius = _planetRadius;
        planetPosition = _planetPosition;
        planetName = _planetName;
        planetResources = _planetResources;
        planetSettled = planetResources.Count > 0;
        planetResourceAmounts = new Dictionary<string, DynamicResourceData>();
        foreach (StaticResourceData resourceData in planetResources)
        {
            DynamicResourceData newDynamicResourceData = ScriptableObject.CreateInstance<DynamicResourceData>();
            newDynamicResourceData.PopulateDynamicResourceData(resourceData);
            planetResourceAmounts.Add(resourceData.ResourceName, newDynamicResourceData);
        }
    }

    public float GetNormalizedTimeTillNextResourceGain(string resourceName)
    {
        return planetResourceAmounts[resourceName].NormalizedTimeReachNewResource();
    }

    public float GetPlanetResourceAmount(string resourceName)
    {
        return planetResourceAmounts[resourceName].Amount;
    }

    public void TickPlanetResources()
    {
        foreach (DynamicResourceData dynamicResourceData in planetResourceAmounts.Values)
        {
            dynamicResourceData.Tick();
        }
    }

    public void AddResource(string resourceName, float amount)
    {
        planetSettled = true;
        if (planetResourceAmounts.ContainsKey(resourceName))
        {
            planetResourceAmounts[resourceName].AddCustomAmount(amount);
        }
        else
        {
            StaticResourceData newResourceData = GameManager.Instance.ResourceManager.GetResourceData(resourceName);
            planetResources.Add(newResourceData);
            DynamicResourceData newDynamicResourceData = ScriptableObject.CreateInstance<DynamicResourceData>();
            newDynamicResourceData.PopulateDynamicResourceData(newResourceData, amount);
            planetResourceAmounts.Add(resourceName, newDynamicResourceData);
            planetResourceAmounts[resourceName].AddCustomAmount(amount);
            NewResourceAdded.Invoke();
        }
    }

    public void RemoveResource(string resourceName, float amount)
    {
        if (GetPlanetResourceAmount(resourceName) < amount)
        {
            amount = amount - GetPlanetResourceAmount(resourceName);
        }
        planetResourceAmounts[resourceName].RemoveCustomAmount(amount, false);
    }
}
