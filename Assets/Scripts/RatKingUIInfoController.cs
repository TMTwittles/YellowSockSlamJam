using UnityEngine;

public class RatKingUIInfoController : MonoBehaviour
{
    private RatKingData data;
    [SerializeField] private GameObject activeRatKingInfo;
    [SerializeField] private GameObject inactiveRatKingInfo;
    [SerializeField] private Transform ratKingResourceListTransform;
    [SerializeField] private GameObject ratKingResourceInfoGameObject;
    [SerializeField] private Transform ratKingBoonListTransform;
    [SerializeField] private GameObject ratKingBoonDisplayGameObject;
    
    public void ConfigureRatKingInfoUIController(RatKingData _data)
    {
        data = _data;
        data.NewResourceAdded += OnNewResourceAdded;
        //activeRatKingInfo.SetActive(false);
        //inactiveRatKingInfo.SetActive(true);

        foreach (DynamicResourceData staticResourceData in data.PlanetShippableResourceAmount.Values)
        {
            GameObject instantiatedRatKingResourceGameObject = Instantiate(ratKingResourceInfoGameObject, ratKingResourceListTransform);
            instantiatedRatKingResourceGameObject.GetComponent<PlanetResourceInfoUIController>().ConfigurePlanetResourceInfoUIController(staticResourceData.Data.ResourceName, data);
        }

        foreach (StructureData structureData in data.StructuresToBuy)
        {
            GameObject instantiatedRatKingBoonDisplayGameObject = Instantiate(ratKingBoonDisplayGameObject, ratKingBoonListTransform);
            instantiatedRatKingBoonDisplayGameObject.GetComponent<RatKingBoonUIDisplayController>().ConfigureRatKingBoonUIDisplay(structureData, data);
        }
    }
    

    private void OnNewResourceAdded()
    {
        data.NewResourceAdded -= OnNewResourceAdded;
        activeRatKingInfo.SetActive(true);
        inactiveRatKingInfo.SetActive(false);
        
        
    }
}
