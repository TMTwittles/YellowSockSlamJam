using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private GameStateData data;

    public void ConfigureGameState()
    {
        data.ConfigureGameStateData();
    }

    public float GetNumRequiredForNextMilestone()
    {
        return data.CurrentMilestone;
    }

    private void Update()
    {
        if (data != null)
        {
            data.Tick();
        }
    }
}
