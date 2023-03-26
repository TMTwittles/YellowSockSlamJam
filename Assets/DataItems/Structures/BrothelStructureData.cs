using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create BrothelStructureData", fileName = "BrothelStructureData", order = 0)]
public class BrothelStructureData : StructureData
{
    private bool consumeShippableResource;
    
    public override void ConfigureStructureData(PlanetData planetData)
    {
        requiredResources = new List<StaticResourceData>();
        foreach (StaticResourceData staticResourceData in planetData.PlanetNaturalResources)
        {
            requiredResources.Add(staticResourceData);
        }
        
        outputResourcesDict = new Dictionary<string, StaticResourceData>();
        foreach (StaticResourceData outputResource in outputResources)
        {
            outputResourcesDict.Add(outputResource.ResourceName, outputResource);
        }
    }

    public override void Tick(PlanetData planetData)
    {
        if (planetData.PlanetPopulation < requiredHumans)
        {
            return;
        }

        foreach (StaticResourceData resourceData in planetData.PlanetNaturalResources)
        {
            if (planetData.GetNaturalPlanetResourceAmount(resourceData.ResourceName) <= 0.0f)
            {
                return;
            }
        }
        
        elapsedTime += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;

        if (elapsedTime > timeStructureGenerates)
        {
            elapsedTime = 0.0f;
            planetData.AddShippableResource(outputResources[0].ResourceName, resourceGain);
            foreach (StaticResourceData staticResourceData in planetData.PlanetNaturalResources)
            {
                planetData.RemoveNaturalResource(staticResourceData.ResourceName, resourceGain);
            }
        }
    }
}
