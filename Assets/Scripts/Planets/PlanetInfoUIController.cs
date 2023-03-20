using TMPro;
using UnityEngine;

public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI planetNameTmp;
    [SerializeField] private Transform planetResourcesListTransform;
    [SerializeField] private GameObject planetResourceUIDisplayGameObject;
    
    private PlanetData data;
    
    public void ConfigurePlanetInfoUIController(PlanetData _data)
    {
        data = _data;
        planetNameTmp.text = data.PlanetName;
        foreach (StaticResourceData resourceData in data.PlanetResources)
        {
            GameObject newPlanetResourceUIDisplay = Instantiate(planetResourceUIDisplayGameObject, planetResourcesListTransform);
            newPlanetResourceUIDisplay.GetComponentInChildren<PlanetResourceInfoUIController>().ConfigurePlanetResourceInfoUIController(resourceData.ResourceName, data);
        }
    }
}
