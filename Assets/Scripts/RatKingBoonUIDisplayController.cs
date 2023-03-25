using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RatKingBoonUIDisplayController : MonoBehaviour
{
    private StructureData data;
    private RatKingData ratKingData;
    [SerializeField] private Image boonImage;
    [SerializeField] private Image spendButtonPlanetImage;
    [SerializeField] private TextMeshProUGUI boonName;
    [SerializeField] private TextMeshProUGUI boonDescription;
    [SerializeField] private TextMeshProUGUI spendText;
    [SerializeField] private Button buyBoonButton;

    public void ConfigureRatKingBoonUIDisplay(StructureData _data, RatKingData _ratKingData)
    {
        data = _data;
        ratKingData = _ratKingData;
        boonImage.sprite = data.StructureIcon;
        spendButtonPlanetImage.sprite = data.RequiredResources[0].ResourceSprite;
        //boonName.text = data.StructureName;
        boonDescription.text = data.StructureDescription;
        spendText.text = $"Spend {data.RequiredResourceAmount}";
        buyBoonButton.interactable = ratKingData.GetShippablePlanetResourceAmount(data.RequiredResources[0].ResourceName) > 0;
        buyBoonButton.onClick.AddListener(OnBuyBoon);
    }

    void Update()
    {
        buyBoonButton.interactable = ratKingData.GetShippablePlanetResourceAmount(data.RequiredResources[0].ResourceName) > 0;
    }

    void OnBuyBoon()
    {
        ratKingData.RemoveShippableResource(data.RequiredResources[0].ResourceName, data.RequiredResourceAmount);
        if (data.StructureName == StructureNames.Planet)
        {
            GameManager.Instance.PlanetManager.InstantiatePlanets(1);
        }
        else
        {
            data.AddToAmount();
        }
    }
    
    
}
