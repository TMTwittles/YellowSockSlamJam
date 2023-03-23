using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create ResourcesData", fileName = "ResourcesData", order = 0)]
public class ResourcesData : ScriptableObject
{
    [SerializeField] private List<StaticResourceData> globalResources;
    private Dictionary<string, StaticResourceData> globalResourcesDict;
    public Dictionary<string, StaticResourceData> GlobalResourcesDict => globalResourcesDict;
    private Dictionary<string, float> globalResourceAmountDict;
    public Dictionary<string, float> GlobalResourceAmountDict => globalResourceAmountDict;

    [SerializeField] private StaticResourceData startingPlanetPopulationResource;
    public StaticResourceData StartingPlanetPopulationResource => startingPlanetPopulationResource;
    
    [SerializeField] private StaticResourceData planetPopulationResource;
    public StaticResourceData PlanetPopulationResource => planetPopulationResource;
    [FormerlySerializedAs("startingPlanetResources")] [SerializeField] private List<StaticResourceData> startingPlanetNaturalResources;
    public List<StaticResourceData> StartingPlanetNaturalResources => startingPlanetNaturalResources;

    public void ConfigureResourcesData()
    {
        globalResourcesDict = new Dictionary<string, StaticResourceData>();
        globalResourceAmountDict = new Dictionary<string, float>();
        foreach (StaticResourceData resourceData in globalResources)
        {
            globalResourcesDict.Add(resourceData.ResourceName, resourceData);
            globalResourceAmountDict.Add(resourceData.ResourceName, 0.0f);
        }
    }

    public void AddToGlobalShippableResourceAmount(string resourceName, float amount)
    {
        globalResourceAmountDict[resourceName] += amount;
    }
}
