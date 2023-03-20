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

    private bool planetSettled = false;
    public bool PlanetSettled => planetSettled;

    public void PopulatePlanetData(string _planetName, List<StaticResourceData> _planetResources)
    {

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
}
