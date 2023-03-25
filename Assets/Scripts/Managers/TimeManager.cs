using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private TimeModifierState currentTimeModifierState = TimeModifierState.NORMAL;
    public TimeModifierState CurrentTimeModifierState => currentTimeModifierState;
    public float TimeModifier => (float) currentTimeModifierState;
    
    public enum TimeModifierState
    {
        PAUSED, NORMAL, TWO_TIMES, THREE_TIMES
    }
    
    public void ToggleTimeModifierState()
    {
        switch (currentTimeModifierState)
        {
            case TimeModifierState.PAUSED:
                currentTimeModifierState = TimeModifierState.NORMAL;
                break;
            case TimeModifierState.NORMAL:
                currentTimeModifierState = TimeModifierState.TWO_TIMES;
                break;
            case TimeModifierState.TWO_TIMES:
                currentTimeModifierState = TimeModifierState.THREE_TIMES;
                break;
            case TimeModifierState.THREE_TIMES:
                currentTimeModifierState = TimeModifierState.PAUSED;
                break;
            default:
                break;
        }
    }

    public void SetState(TimeModifierState _state)
    {
        currentTimeModifierState = _state;
    }
}
