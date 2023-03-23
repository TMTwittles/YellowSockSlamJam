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
        planetManager.InstantiatePlanets(1);
        stateManager.ConfigureGameState();
        GameConfigured?.Invoke();
    }

    private void Start()
    {
        
    }
    
    
}
