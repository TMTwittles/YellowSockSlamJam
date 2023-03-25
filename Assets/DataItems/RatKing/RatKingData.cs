using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create RatKingData", fileName = "RatKingData", order = 0)]
public class RatKingData : PlanetData
{
    [SerializeField] private List<StructureData> structuresToBuy;
    public List<StructureData> StructuresToBuy => structuresToBuy;

    public void SetRatKingStructureToBuy(List<StructureData> _structuresToBuy)
    {
        structuresToBuy = _structuresToBuy;
    }
    
    public void PopulateRatKingData(string _planetName, Vector3 _planetPosition, float _planetRadius, StaticResourceData _planetPopulation, List<StaticResourceData> _planetNaturalResources)
    {
        //PopulatePlanetData(_planetName, _planetPosition, _planetRadius, _planetPopulation, _planetNaturalResources);
        planetRadius = _planetRadius;
        planetPosition = _planetPosition;
        planetName = _planetName;
        planetShippableResourceAmounts = new Dictionary<string, DynamicResourceData>();
        foreach (StaticResourceData resourceData in GameManager.Instance.ResourceManager.ResourcesData.GlobalResources)
        {
            DynamicResourceData newDynamicResourceData = ScriptableObject.CreateInstance<DynamicResourceData>();
            newDynamicResourceData.PopulateDynamicResourceData(resourceData, true);
            newDynamicResourceData.SetCustomAmount(0.0f);
            planetShippableResourceAmounts.Add(resourceData.ResourceName, newDynamicResourceData);
        }
    }
}
