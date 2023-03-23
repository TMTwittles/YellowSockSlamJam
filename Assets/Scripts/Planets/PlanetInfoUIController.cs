using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private Transform planetActiveInfoTransform;
    [SerializeField] private Transform planetNotActiveInfoTransform;
    [SerializeField] private TextMeshProUGUI planetNameTmp;
    [SerializeField] private Transform planetNaturalResourceListTransform;
    [SerializeField] private Transform planetShippableResourceListTransform;
    [SerializeField] private GameObject planetShippableResourceUIDisplayGameObject;
    [SerializeField] private GameObject planetNaturalResourceUIDisplayGameObject;
    [SerializeField] private Image humanIconImage;
    [SerializeField] private StructureInfoUIController structureInfoUIController;
    
    private PlanetData data;
    
    public void ConfigurePlanetInfoUIController(PlanetData _data)
    {
        data = _data;
        data.NewResourceAdded += OnResourceModified;
        data.ResourceRemoved += OnResourceModified;
        structureInfoUIController.Configure(data);
        planetActiveInfoTransform.gameObject.SetActive(true);
        planetNotActiveInfoTransform.gameObject.SetActive(false);
        planetNameTmp.text = $" = {data.GetShippablePlanetResourceAmount(ResourceNames.HUMAN)}";
        humanIconImage.gameObject.SetActive(true);
        ConfigureShit();
    }

    private void ConfigureShit()
    {
        planetNameTmp.text = $" = {data.GetShippablePlanetResourceAmount(ResourceNames.HUMAN)}";
        
        foreach (StaticResourceData resourceData in data.PlanetShippableResources)
        {
            GameObject newPlanetResourceUIDisplay =
                Instantiate(planetShippableResourceUIDisplayGameObject, planetShippableResourceListTransform);
            newPlanetResourceUIDisplay.GetComponentInChildren<PlanetResourceInfoUIController>().ConfigurePlanetResourceInfoUIController(resourceData.ResourceName, data);
        }

        foreach (StaticResourceData resourceData in data.PlanetNaturalResources)
        {
            GameObject newPlanetResourceUIDisplay =
                Instantiate(planetNaturalResourceUIDisplayGameObject, planetNaturalResourceListTransform);
            newPlanetResourceUIDisplay.GetComponentInChildren<PlanetResourceInfoUIController>().ConfigurePlanetResourceInfoUIController(resourceData.ResourceName, data);
        }
    }

    private void OnResourceModified()
    {
        foreach (PlanetResourceInfoUIController resources in planetShippableResourceListTransform.GetComponentsInChildren<PlanetResourceInfoUIController>())
        {
            Destroy(resources.gameObject);
        }

        foreach (PlanetResourceInfoUIController resources in planetNaturalResourceListTransform.GetComponentsInChildren<PlanetResourceInfoUIController>())
        {
            Destroy(resources.gameObject);
        }
        ConfigureShit();
    }
    
    
}
