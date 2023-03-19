using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create PlanetPositionGenerationData", fileName = "PlanetPositionGenerationData", order = 0)]
public class PlanetPositionGenerationData : ScriptableObject
{
    // If these variables are confusing and make no sense. I agree. I am bad at naming things - Arvie.
    
    [Header("Planet position general settings")]
    [SerializeField] private int numCycles;
    public int NumCycles => numCycles; 
    [SerializeField] private float distanceBetweenCycles;
    public float DistanceBetweenCycles => distanceBetweenCycles;

    [Header("Planet position modifiers")]
    [SerializeField] private int planetsPerCycleMin;
    public int PlanetsPerCycleMin => planetsPerCycleMin;
    [SerializeField] private int planetsPerCycleMax;
    public int PlanetsPerCycleMax => planetsPerCycleMax;
    [SerializeField] private float planetPositionRandomVariationAmount;

    [SerializeField] private float planetsPerCycleIncrementMin;
    public float PlanetsPerCycleIncrementMin => planetsPerCycleIncrementMin;

    [SerializeField] private float planetsPerCycleIncrementMax;
    public float PlanetsPerCycleIncrementMax => planetsPerCycleIncrementMax;
    
    public float PlanetPositionRandomVariationAmount => planetPositionRandomVariationAmount;
}
