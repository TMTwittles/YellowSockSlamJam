using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    [SerializeField] private ResourcesData resourceData;
    public ResourcesData ResourcesData => resourceData;

    public void ConfigureResources()
    {
        resourceData.ConfigureResourcesData();   
    }
    
    public List<StaticResourceData> GetRandomResourcesForPlanet()
    {
        int numResources = UnityEngine.Random.Range(2, 2);
        List<StaticResourceData> listShuffled = resourceData.StartingPlanetNaturalResources;
        List<StaticResourceData> planetStartingResources = new List<StaticResourceData>();
        // This could be made generic.
        for (int i = listShuffled.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i);
            StaticResourceData valueToShuffle = listShuffled[randomIndex];
            listShuffled[randomIndex] = listShuffled[i];
            listShuffled[i] = valueToShuffle;
        }

        for (int i = 0; i < numResources; i++)
        {
            planetStartingResources.Add(listShuffled[i]);
        }

        return planetStartingResources;
    }

    public StaticResourceData GetStartingPlanetPopulation(int planetIndex)
    {
        if (planetIndex == 0)
        {
            return resourceData.StartingPlanetPopulationResource;
        }
        
        StaticResourceData newPlanetPopulation = resourceData.PlanetPopulationResource;
        return newPlanetPopulation;
    }

    public List<StaticResourceData> GetStartingPlanetResources(int planetIndex)
    {
        return GetRandomResourcesForPlanet();
        /*if (planetIndex == 0)
        {
            return resourceData.StartingPlanetResources;            
        }
        return new List<StaticResourceData>();*/
    }

    public StaticResourceData GetResourceData(string resourceName)
    {
        return resourceData.GlobalResourcesDict[resourceName];
    }

    public void AddToGlobalResourcesAmount(string resourceName, float amount)
    {
        resourceData.AddToGlobalShippableResourceAmount(resourceName, amount);
    }

    public float GetGlobalResourceAmount(string resourceName)
    {
        return resourceData.GlobalResourceAmountDict[resourceName];
    }
}
