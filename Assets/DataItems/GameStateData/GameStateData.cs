using UnityEngine;

[CreateAssetMenu(menuName = "Create GameStateData", fileName = "GameStateData", order = 0)]
public class GameStateData : ScriptableObject
{
    [SerializeField] private float numHumansRequiredToBeatFirstMilestone;
    [SerializeField] private float nextMilestoneIncrement;
    [SerializeField] private int numPlanetsDiscoveredPerMilestone;
    private float currentMilestone;

    public void ConfigureGameStateData()
    {
        currentMilestone = numHumansRequiredToBeatFirstMilestone;
    }
    
    // THIS IS ALL WIP FUNCTIONALITY
    public void Tick()
    {
        if (GameManager.Instance.ResourceManager.GetGlobalResourceAmount(ResourceNames.HUMAN) > currentMilestone)
        {
            currentMilestone = currentMilestone * nextMilestoneIncrement;
            GameManager.Instance.PlanetManager.InstantiatePlanets(numPlanetsDiscoveredPerMilestone);
        }
    }
}
