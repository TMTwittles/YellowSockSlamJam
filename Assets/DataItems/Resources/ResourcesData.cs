using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ResourcesData", fileName = "ResourcesData", order = 0)]
public class ResourcesData : ScriptableObject
{
    [SerializeField] private List<StaticResourceData> globalResources;
    private Dictionary<string, StaticResourceData> globalResourcesDict;
    private Dictionary<string, float> globalResourceAmountDict;
    public Dictionary<string, StaticResourceData> GlobalResourcesDict => globalResourcesDict;

    [SerializeField] private List<StaticResourceData> startingPlanetResources;
    public List<StaticResourceData> StartingPlanetResources => startingPlanetResources;

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

    public void AddToGlobalResourceAmount(string resourceName, float amount)
    {
        globalResourceAmountDict[resourceName] += amount;
    }
}
