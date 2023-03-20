using System.Collections;
using System.Collections.Generic;
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
    }

    private void Start()
    {
        planetPositionManager.GeneratePlanetPositions();
        resourceManager.ConfigureResources();
        planetManager.InstantiatePlanets();
    }
    
    
}
