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
    [SerializeField] private Image lockIconImage;
    [SerializeField] private Image humanIconImage;
    
    private PlanetData data;
    
    public void ConfigurePlanetInfoUIController(PlanetData _data)
    {
        data = _data;
        data.NewResourceAdded += OnNewResourceAdded;
        ConfigureShit();
    }

    private void ConfigureShit()
    {
        if (data.PlanetSettled)
        {
            planetActiveInfoTransform.gameObject.SetActive(true);
            planetNotActiveInfoTransform.gameObject.SetActive(false); 
            lockIconImage.gameObject.SetActive(false);
            planetNameTmp.text = $" = {data.GetShippablePlanetResourceAmount(ResourceNames.HUMAN)}";
            humanIconImage.gameObject.SetActive(true);
        }
        else
        {
            planetActiveInfoTransform.gameObject.SetActive(false);
            planetNotActiveInfoTransform.gameObject.SetActive(true);   
            lockIconImage.gameObject.SetActive(true);
            humanIconImage.gameObject.SetActive(false);
            planetNameTmp.text = "";
        }

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

    private void OnNewResourceAdded()
    {
        foreach (PlanetInfoUIController resources in planetShippableResourceListTransform.GetComponentsInChildren<PlanetInfoUIController>())
        {
            Destroy(resources.gameObject);
        }

        foreach (PlanetInfoUIController resources in planetNaturalResourceListTransform.GetComponentsInChildren<PlanetInfoUIController>())
        {
            Destroy(resources.gameObject);
        }
        ConfigureShit();
    }
    
    
}
