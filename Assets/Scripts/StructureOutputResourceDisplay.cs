using UnityEngine;
using UnityEngine.UI;

public class StructureOutputResourceDisplay : MonoBehaviour
{
    [SerializeField] private Image resourceImage;
    [SerializeField] private Image resourceTickerImage;
    private PlanetData data;
    
    public void ConfigureOutputResourceDisplay(string resourceName, PlanetData _data)
    {
        data = _data;
        resourceImage.sprite = data.PlanetStructure.OutputResourceDict[resourceName].ResourceSprite;
        resourceTickerImage.fillAmount = data.PlanetStructure.GetNormalizedTimeNextResourceGain();
    }

    private void Update()
    {
        if (data != null)
        {
            resourceTickerImage.fillAmount = data.PlanetStructure.GetNormalizedTimeNextResourceGain();
        }
    }
}
