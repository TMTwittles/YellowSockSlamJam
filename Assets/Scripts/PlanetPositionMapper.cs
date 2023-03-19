using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPositionMapper : MonoBehaviour
{
    // Start point, in case of this game we could consider this the position of the sun.
    [SerializeField] private Vector3 origin;
    [SerializeField] private float wireSphereRadius;
    [SerializeField] private PlanetPositionGenerationData data;
    private List<List<Vector3>> planetPositions;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(origin, wireSphereRadius);
        Vector3 previousPlanetPosition = Vector3.zero;
        Vector3 startingPlanetInCycle = Vector3.zero;
        if (planetPositions != null)
        {
            foreach (List<Vector3> planetPositionInCycle in planetPositions)
            {
                previousPlanetPosition = Vector3.zero;
                startingPlanetInCycle = Vector3.zero;
                foreach (Vector3 planetPosition in planetPositionInCycle)
                {
                    if (previousPlanetPosition != Vector3.zero)
                    {
                        Gizmos.DrawLine(previousPlanetPosition, planetPosition);
                    }
                    else
                    {
                        startingPlanetInCycle = planetPosition;
                    }
                    Gizmos.DrawWireSphere(planetPosition, wireSphereRadius);
                    previousPlanetPosition = planetPosition;
                }
                Gizmos.DrawLine(startingPlanetInCycle, previousPlanetPosition);
            }
        }
    }
    
    void Start()
    {
        GeneratePlanetPositions();
    }
    
    public void GeneratePlanetPositions()
    {
        planetPositions = new List<List<Vector3>>();
        float thetaInc = 0.0f;
        float currentTheta = 0.0f;
        Vector3 planetPosition = Vector3.zero;
        int numPositionsCurrentCycle = 0;
        for (int currentCycle = 0; currentCycle < data.NumCycles; currentCycle++)
        {
            planetPositions.Add(new List<Vector3>());
            numPositionsCurrentCycle = GetNumPlanetsInCycle(currentCycle);
            for (int currentPosition = 0; currentPosition < numPositionsCurrentCycle; currentPosition++)
            {
                thetaInc = 360.0f / numPositionsCurrentCycle;
                currentTheta = thetaInc * currentPosition;
                Vector3 positionNotRotated = GetNonRotatedRandomPositionWithInCycle(currentCycle);
                planetPosition = RotatePosition(positionNotRotated, currentTheta);
                planetPositions[currentCycle].Add(planetPosition);
            }
        }
    }

    private Vector3 RotatePosition(Vector3 positionToRotate, float rotationDegrees)
    {
        float rotationRadians = rotationDegrees * Mathf.Deg2Rad;
        // This is not fuck off this a baby 2D rotation matrix. - Arvie.
        Vector3 positionRotated = positionToRotate.x * new Vector3(Mathf.Cos(rotationRadians), 0.0f, Mathf.Sin(rotationRadians))
                             + positionToRotate.z * new Vector3(-Mathf.Sin(rotationRadians), 0.0f, Mathf.Cos(rotationRadians));
        return positionRotated;
    }
    
    // This is random everytime, so make sure you use cache this - Arvie.
    public int GetNumPlanetsInCycle(int cycleIndex)
    {
        // Higher planet cycle will result in more planets in that cycle.
        float planetCountModifier = (cycleIndex + 1) * UnityEngine.Random.Range(data.PlanetsPerCycleIncrementMin, data.PlanetsPerCycleIncrementMax);
        return (int) UnityEngine.Random.Range(data.PlanetsPerCycleMin * planetCountModifier, data.PlanetsPerCycleMax * planetCountModifier);
    }

    public Vector3 GetNonRotatedRandomPositionWithInCycle(int cycleIndex)
    {
        float distanceNoRandom = (cycleIndex + 1) * data.DistanceBetweenCycles;
        float randomVariationAmount = data.PlanetPositionRandomVariationAmount;
        float positionX = distanceNoRandom + UnityEngine.Random.Range(-randomVariationAmount, randomVariationAmount);
        float positionZ = distanceNoRandom + UnityEngine.Random.Range(randomVariationAmount, randomVariationAmount);
        return new Vector3(positionX, 0.0f, positionZ);
    }
}
