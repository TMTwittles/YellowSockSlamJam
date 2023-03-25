using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create GameStateData", fileName = "GameStateData", order = 0)]
public class GameStateData : ScriptableObject
{
    [SerializeField] private int secondsIncreaseDoomsdayTime;
    [SerializeField] private float timeIncreaseModifierAmount = 0.01f;
    private float timeIncreaseModifier = 0.0f;
    
    [SerializeField] private float timeSecondsTillRatKingDestroysUniverse;
    private float elapsedTime = 0.0f;

    [SerializeField] private float numHumansRequiredToBeatFirstMilestone;
    [SerializeField] private float nextMilestoneIncrement;
    [SerializeField] private int numPlanetsDiscoveredPerMilestone;

    [Header("Starting data")] 
    [SerializeField] private int numStartingPlanets;

    public int NumStartingPlanets => numStartingPlanets;
    [SerializeField] private List<StaticResourceData> startingPlanetResources;
    public List<StaticResourceData> StartingPlanetResources => startingPlanetResources;
    
    private float currentMilestone;
    public float CurrentMilestone => currentMilestone;

    private ResourcesData globalResourcesData;
    private bool gameOver = false;

    public void ConfigureGameStateData()
    {
        timeIncreaseModifier = 0.0f;
        gameOver = false;
        elapsedTime = timeSecondsTillRatKingDestroysUniverse;
        currentMilestone = numHumansRequiredToBeatFirstMilestone;
    }

    public float NormalizedTimeTillDoomsday()
    {
        return Mathf.Clamp(elapsedTime / timeSecondsTillRatKingDestroysUniverse, 0.0f, 1.0f);
    }

    public void AddTime(float time)
    {
        elapsedTime = Mathf.Clamp(elapsedTime + time, 0.0f, timeSecondsTillRatKingDestroysUniverse);
    }
    
    // THIS IS ALL WIP FUNCTIONALITY
    public void Tick()
    {
        if (gameOver == false)
        {
            elapsedTime -= Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;
            elapsedTime -= timeIncreaseModifier * GameManager.Instance.TimeManager.TimeModifier;
            if (elapsedTime % secondsIncreaseDoomsdayTime == 0.0f)
            {
                timeIncreaseModifier = Mathf.Clamp(timeIncreaseModifier + timeIncreaseModifierAmount, 0.5f, timeIncreaseModifier);
            }
            
            
            if (GameManager.Instance.ResourceManager.GetGlobalResourceAmount(ResourceNames.HUMAN) <= 0.0f)
            {
                GameManager.Instance.UIManager.GameOverPanel.gameObject.SetActive(true);
                GameManager.Instance.TimeManager.SetState(TimeManager.TimeModifierState.PAUSED);
                GameManager.Instance.UIManager.GameOverPanel.gameObject.GetComponent<GameOverPanel>().ShowGameOverPanel("The entire rat population has gone. Make sure to increase your population using burrows.");
                gameOver = true;
            }
            if (elapsedTime <= 0.0f)
            {
                GameManager.Instance.UIManager.GameOverPanel.gameObject.SetActive(true);
                GameManager.Instance.TimeManager.SetState(TimeManager.TimeModifierState.PAUSED);
                GameManager.Instance.UIManager.GameOverPanel.gameObject.GetComponent<GameOverPanel>().ShowGameOverPanel("The rat king destroyed the universe! Slow the rat king by sacrificing the rat population and resources!");
                gameOver = true;
            }
        }
    }
}
