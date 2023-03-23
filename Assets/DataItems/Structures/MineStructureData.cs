using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create MineStructureData", fileName = "MineStructureData", order = 0)]
public class MineStructureData : StructureData
{
    public override void ConfigureStructureData(PlanetData data)
    {
        requiredResources = new List<StaticResourceData>();
        outputResourcesDict = new Dictionary<string, StaticResourceData>();
        foreach (StaticResourceData staticResourceData in data.PlanetNaturalResources)
        {
            requiredResources.Add(staticResourceData);
            outputResourcesDict.Add(staticResourceData.ResourceName, staticResourceData);
        }
    }

    public override void Tick(PlanetData planetData)
    {
        if (planetData.PlanetNaturalResources.Count <= 0.0f || planetData.PlanetPopulation < requiredHumans)
        {
            return;
        }
        
        elapsedTime += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;

        if (elapsedTime > timeStructureGenerates)
        {
            foreach (StaticResourceData staticResourceData in planetData.PlanetNaturalResources)
            {
                planetData.AddShippableResource(staticResourceData.ResourceName, resourceGain);
                planetData.RemoveNaturalResource(staticResourceData.ResourceName, resourceGain);
            }
            elapsedTime = 0.0f;
        }
    }
}
