using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private ResourcesData resourceData;

    public void ConfigureResources()
    {
        resourceData.ConfigureResourcesData();   
    }

    public List<StaticResourceData> GetStartingPlanetResources(int planetIndex)
    {
        if (planetIndex == 0)
        {
            return resourceData.StartingPlanetResources;            
        }
        return new List<StaticResourceData>();
    }

    public StaticResourceData GetResourceData(string resourceName)
    {
        return resourceData.GlobalResourcesDict[resourceName];
    }

    public void AddToGlobalResourcesAmount(string resourceName, float amount)
    {
        resourceData.AddToGlobalResourceAmount(resourceName, amount);
    }

    public float GetGlobalResourceAmount(string resourceName)
    {
        return resourceData.GlobalResourceAmountDict[resourceName];
    }
}
