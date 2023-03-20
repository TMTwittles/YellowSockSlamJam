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

    public List<StaticResourceData> GetStartingPlanetResources()
    {
        return resourceData.StartingPlanetResources;
    }

    public StaticResourceData GetResourceData(string resourceName)
    {
        return resourceData.GlobalResourcesDict[resourceName];
    }
}
