using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlanetData", fileName = "PlanetData", order = 0)]
public class PlanetData : ScriptableObject
{
    protected string planetName;
    public string PlanetName => planetName;
    private List<StaticResourceData> planetShippableResources;
    public List<StaticResourceData> PlanetShippableResources => planetShippableResources;
    private List<StaticResourceData> planetNaturalResources;
    public List<StaticResourceData> PlanetNaturalResources => planetNaturalResources;
    
    protected Dictionary<string, DynamicResourceData> planetShippableResourceAmounts;
    public Dictionary<string, DynamicResourceData> PlanetShippableResourceAmount => planetShippableResourceAmounts;
    private Dictionary<string, DynamicResourceData> planetNaturalResourceAmounts;

    protected Vector3 planetPosition;
    public Vector3 PlanetPosition => planetPosition;

    protected bool planetSettled = false;
    public bool PlanetSettled => planetSettled;

    private StructureData planetStructure;
    public StructureData PlanetStructure => planetStructure;

    protected float planetRadius;
    public float PlanetRadius => planetRadius;
    private float planetPopulation = 0.0f;
    public float PlanetPopulation => planetPopulation; 
    
    public Action NewResourceAdded;
    public Action ResourceRemoved;
    public Action StructureAdded;

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

    public bool HasShippableResource(string resourceName)
    {
        return planetShippableResourceAmounts.ContainsKey(resourceName);
    }

    public bool HasNaturalResource(string resourceName)
    {
        return planetNaturalResourceAmounts.ContainsKey(resourceName);
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

    public void TickPlanetStructure()
    {
        if (planetStructure != null)
        {
            planetStructure.Tick(this);
        }
    }

    public void TickNaturalPlanetResources()
    {
        foreach (DynamicResourceData dynamicResourceData in planetNaturalResourceAmounts.Values)
        {
            dynamicResourceData.TickNaturalResource(planetPopulation);
        }
    }

    public void AddShippableResource(string resourceName, float amount, bool isRatking = false)
    {
        planetSettled = true;
        if (planetShippableResourceAmounts.ContainsKey(resourceName))
        {
            if (resourceName == ResourceNames.HUMAN)
            {
                planetPopulation = planetPopulation + amount;
            }
            planetShippableResourceAmounts[resourceName].AddCustomAmount(amount, isRatking);
        }
        else
        {
            StaticResourceData newResourceData = GameManager.Instance.ResourceManager.GetResourceData(resourceName);
            planetShippableResources.Add(newResourceData);
            DynamicResourceData newDynamicResourceData = ScriptableObject.CreateInstance<DynamicResourceData>();
            newDynamicResourceData.PopulateDynamicResourceData(newResourceData, amount);
            planetShippableResourceAmounts.Add(resourceName, newDynamicResourceData);
            planetShippableResourceAmounts[resourceName].AddCustomAmount(amount, isRatking);
        }
        NewResourceAdded?.Invoke();
    }

    public void RemoveShippableResource(string resourceName, float amount)
    {
        if (GetShippablePlanetResourceAmount(resourceName) < amount)
        {
            amount = amount - GetShippablePlanetResourceAmount(resourceName);
        }

        if (resourceName != ResourceNames.HUMAN && amount <= 0)
        {
            planetShippableResourceAmounts.Remove(resourceName);
        }
        else
        {
            if (resourceName == ResourceNames.HUMAN)
            {
                planetPopulation = planetPopulation - amount;
            }
            
            planetShippableResourceAmounts[resourceName].RemoveCustomAmount(amount, true);    
        }
        ResourceRemoved?.Invoke();
    }
    
    public void RemoveNaturalResource(string resourceName, float amount)
    {
        if (GetNaturalPlanetResourceAmount(resourceName) < amount)
        {
            amount = amount - GetNaturalPlanetResourceAmount(resourceName);
        }

        if (amount <= 0)
        {
            planetNaturalResourceAmounts.Remove(resourceName);
        }
        else
        {
            planetNaturalResourceAmounts[resourceName].RemoveCustomAmount(amount, true);    
        }
    }

    public void AddStructure(StructureData _structureData)
    {
        planetStructure = _structureData;
        planetStructure.ConfigureStructureData(this);
        StructureAdded.Invoke();
    }
}
