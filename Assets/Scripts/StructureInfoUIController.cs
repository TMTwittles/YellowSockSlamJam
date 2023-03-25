using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureInfoUIController : MonoBehaviour
{
    [SerializeField] private GameObject structureActiveGameObject;
    [SerializeField] private GameObject structureNotActiveGameObject;
    [SerializeField] private TextMeshProUGUI structureRequirementsText;
    [SerializeField] private Transform consumptionResourceListTransform;
    [SerializeField] private GameObject resourceConsumptionGameObject;
    [SerializeField] private Transform outputResourceListTransform;
    [SerializeField] private GameObject outputResourceDisplayGameObject;
    private PlanetData data;
    
    public void Configure(PlanetData _data)
    {
        data = _data;
        SetActiveMenu();
        data.StructureAdded += OnStructureAdded;
    }

    private void OnDestroy()
    {
        if (data != null)
        {
            data.StructureAdded -= OnStructureAdded;
        }
    }

    private void SetActiveMenu()
    {
        structureActiveGameObject.SetActive(data.PlanetStructure != null);
        structureNotActiveGameObject.SetActive(data.PlanetStructure == null);
    }

    private void OnStructureAdded()
    {
        SetActiveMenu();

        structureRequirementsText.text = $"> {data.PlanetStructure.RequiredHumans}";

        foreach (StaticResourceData consumptionResource in data.PlanetStructure.RequiredResources)
        {
            GameObject instantiatedConsumptionResource = Instantiate(resourceConsumptionGameObject, consumptionResourceListTransform);
            instantiatedConsumptionResource.GetComponent<Image>().sprite = consumptionResource.ResourceSprite;
        }
        
        foreach (StaticResourceData outputResource in data.PlanetStructure.OutputResourceDict.Values)
        {
            GameObject instantiatedOutputResourceDisplayGameObject = Instantiate(outputResourceDisplayGameObject, outputResourceListTransform);
            instantiatedOutputResourceDisplayGameObject.GetComponent<StructureOutputResourceDisplay>().ConfigureOutputResourceDisplay(outputResource.ResourceName, data);
        }
    }

}
