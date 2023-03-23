using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create BrothelStructureData", fileName = "BrothelStructureData", order = 0)]
public class BrothelStructureData : StructureData
{
    private bool consumeShippableResource;
    
    public override void ConfigureStructureData(PlanetData planetData)
    {
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

        consumeShippableResource = false;
        foreach (StaticResourceData staticResourceData in requiredResources)
        {
            if (planetData.HasShippableResource(staticResourceData.ResourceName) && planetData.GetShippablePlanetResourceAmount(staticResourceData.ResourceName) > 0)
            {
                consumeShippableResource = true;
            }
            else if (planetData.HasNaturalResource(staticResourceData.ResourceName) == false || planetData.GetNaturalPlanetResourceAmount(staticResourceData.ResourceName) <= 0)
            {
                return;
            }
        }
        
        elapsedTime += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;

        if (elapsedTime > timeStructureGenerates)
        {
            elapsedTime = 0.0f;
            planetData.AddShippableResource(outputResources[0].ResourceName, resourceGain);
            foreach (StaticResourceData staticResourceData in requiredResources)
            {
                if (consumeShippableResource)
                {
                    planetData.RemoveShippableResource(staticResourceData.ResourceName, resourceGain);
                }
                else
                {
                    planetData.RemoveNaturalResource(staticResourceData.ResourceName, resourceGain);
                }
            }
        }
    }
}
