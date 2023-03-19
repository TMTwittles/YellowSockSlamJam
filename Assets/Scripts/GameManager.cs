using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlanetPositionMapper planetPositionMapper;
    public PlanetPositionMapper PositionMapper => planetPositionMapper;
    [SerializeField] private PlanetManager planetManager;
    public PlanetManager PlanetManager => planetManager;
    
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
        planetPositionMapper.GeneratePlanetPositions();
        planetManager.InstantiatePlanets();
    }
    
    
}
