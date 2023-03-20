using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetResourceInfoUIController : MonoBehaviour
{
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceAmountTmp;
    [SerializeField] private Image resourceTickerImage;
    private string resourceName;
    private PlanetData data;

    public void ConfigurePlanetResourceInfoUIController(string _resourceName, PlanetData _data)
    {
        resourceName = _resourceName;
        data = _data;
        resourceImage.sprite = GameManager.Instance.ResourceManager.GetResourceData(resourceName).ResourceSprite;
        resourceAmountTmp.text = data.GetPlanetResourceAmount(resourceName).ToString();
    }

    void Update()
    {
        if (data != null)
        {
            resourceAmountTmp.text = data.GetPlanetResourceAmount(resourceName).ToString();
            resourceTickerImage.fillAmount = data.GetNormalizedTimeTillNextResourceGain(resourceName);
        }
    }
}
