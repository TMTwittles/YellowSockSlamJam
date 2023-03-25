using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetResourceInfoUIController : MonoBehaviour
{
    [SerializeField] private bool shippableResource;
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

        if (shippableResource)
        {
            resourceAmountTmp.text = data.GetShippablePlanetResourceAmount(resourceName).ToString();   
        }
        else
        {
            resourceTickerImage.fillAmount = data.GetNormalizedTimeTillResourceDepleted(resourceName);
        }
    }

    void Update()
    {
        if (data != null && resourceName != String.Empty)
        {
            if (shippableResource)
            {
                resourceAmountTmp.text = data.GetShippablePlanetResourceAmount(resourceName).ToString();
                //resourceTickerImage.fillAmount = data.GetNormalizedTimeTillNextResourceGain(resourceName);
            }
            else
            {
                resourceTickerImage.fillAmount = data.GetNormalizedTimeTillResourceDepleted(resourceName);
            }
        }
    }
}
