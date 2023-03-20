using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulationMilestoneBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI populationText;
    [SerializeField] private Image progressImage;

    void Update()
    {
        float currentPopulation = GameManager.Instance.ResourceManager.GetGlobalResourceAmount(ResourceNames.HUMAN);
        float requiredPopulation = GameManager.Instance.StateManager.GetNumRequiredForNextMilestone();
        populationText.text = $"{currentPopulation} / {requiredPopulation}";
        progressImage.fillAmount = currentPopulation / requiredPopulation;
    }
}
