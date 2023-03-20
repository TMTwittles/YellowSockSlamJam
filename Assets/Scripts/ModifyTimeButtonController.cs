using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModifyTimeButtonController : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI tmp;

    void Start()
    {
        button.onClick.AddListener(OnButtonPressed);
        UpdateButtonGraphic();
    }

    void OnButtonPressed()
    {
        GameManager.Instance.TimeManager.ToggleTimeModifierState();
        UpdateButtonGraphic();
    }

    void UpdateButtonGraphic()
    {
        TimeManager.TimeModifierState currentState = GameManager.Instance.TimeManager.CurrentTimeModifierState;
        switch (currentState)
        {
            case (TimeManager.TimeModifierState.PAUSED):
                tmp.text = "ll";
                break;
            case (TimeManager.TimeModifierState.NORMAL):
                tmp.text = ">";
                break;
            case (TimeManager.TimeModifierState.TWO_TIMES):
                tmp.text = ">>";
                break;
            case (TimeManager.TimeModifierState.THREE_TIMES):
                tmp.text = ">>>";
                break;
        }
    }
}
