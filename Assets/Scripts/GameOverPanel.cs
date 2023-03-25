using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI gameOverReasonTMP;
    
    public void ShowGameOverPanel(string gameOverReason)
    {
        gameOverReasonTMP.text = gameOverReason;
        button.onClick.AddListener(GameManager.Instance.UIManager.StartGame);
    }
}
