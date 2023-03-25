using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private GameStateData data;

    public GameStateData GetGameStateData()
    {
        return data;
    }
    
    public void ConfigureGameState()
    {
        data.ConfigureGameStateData();
    }

    public void AddTime(float amount)
    {
        data.AddTime(amount);
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
