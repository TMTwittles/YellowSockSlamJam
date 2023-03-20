using TMPro;
using UnityEngine;

public class PlanetInfoUIController : MonoBehaviour
{
    [SerializeField] private Transform planetActiveInfoTransform;
    [SerializeField] private Transform planetNotActiveInfoTransform;
    [SerializeField] private TextMeshProUGUI planetNameTmp;
    [SerializeField] private Transform planetResourcesListTransform;
    [SerializeField] private GameObject planetResourceUIDisplayGameObject;
    
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
        }
        else
        {
            planetActiveInfoTransform.gameObject.SetActive(false);
            planetNotActiveInfoTransform.gameObject.SetActive(true);   
        }

        planetNameTmp.text = data.PlanetName;
        foreach (StaticResourceData resourceData in data.PlanetResources)
        {
            GameObject newPlanetResourceUIDisplay = Instantiate(planetResourceUIDisplayGameObject, planetResourcesListTransform);
            newPlanetResourceUIDisplay.GetComponentInChildren<PlanetResourceInfoUIController>().ConfigurePlanetResourceInfoUIController(resourceData.ResourceName, data);
        }
    }

    private void OnNewResourceAdded()
    {
        foreach (PlanetInfoUIController resources in planetResourcesListTransform.GetComponentsInChildren<PlanetInfoUIController>())
        {
            Destroy(resources.gameObject);
        }
        ConfigureShit();
    }
    
    
}
