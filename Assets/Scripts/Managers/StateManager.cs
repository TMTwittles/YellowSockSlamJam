using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private GameStateData data;

    public void ConfigureGameState()
    {
        data.ConfigureGameStateData();
    }

    private void Update()
    {
        if (data != null)
        {
            data.Tick();
        }
    }
}
