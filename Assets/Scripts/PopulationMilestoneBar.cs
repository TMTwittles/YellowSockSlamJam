using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulationMilestoneBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI populationText;
    [SerializeField] private Image progressImage;
    private GameStateData data;

    void Start()
    {
        data = GameManager.Instance.StateManager.GetGameStateData();
    }
    
    void Update()
    {
        progressImage.fillAmount = data.NormalizedTimeTillDoomsday();
    }
}
