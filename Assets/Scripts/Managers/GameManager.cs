using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("planetPositionMapper")] [SerializeField] private PlanetPositionManager planetPositionManager;
    public PlanetPositionManager PositionManager => planetPositionManager;
    [SerializeField] private PlanetManager planetManager;
    public PlanetManager PlanetManager => planetManager;
    [SerializeField] private ResourceManager resourceManager;
    public ResourceManager ResourceManager => resourceManager;
    [SerializeField] private TimeManager timeManager;
    public TimeManager TimeManager => timeManager;
    [SerializeField] private StateManager stateManager;
    public StateManager StateManager => stateManager;

    [SerializeField] private UIManager uiManager;
    public UIManager UIManager => uiManager;
    [SerializeField] private StructureManager structureManager;
    public StructureManager StructureManager => structureManager;

    public Action GameConfigured;
    public Action<bool> UserPlacingStructure;
    public Action<bool> UserPlacingShuttle;

    public static GameManager Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        Configure();
    }

    private void Configure()
    {
        planetPositionManager.GeneratePlanetPositions();
        resourceManager.ConfigureResources();
        structureManager.ConfigureStructureManager();
        planetManager.InstantiateStartingPlanets();
        stateManager.ConfigureGameState();
        GameConfigured?.Invoke();
    }

    public void InvokeUserPlacingStructure(bool placingStructure)
    {
        UserPlacingStructure?.Invoke(placingStructure);
    }

    public void InvokeUserPlacingShuttle(bool placingShuttle)
    {
        UserPlacingShuttle?.Invoke(placingShuttle);
    }

    private void Start()
    {
        
    }
    
    
}
