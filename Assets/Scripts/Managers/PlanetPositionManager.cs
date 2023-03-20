using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlanetPositionManager : MonoBehaviour
{
    [SerializeField] private Vector3 origin;
    [SerializeField] private float wireSphereRadius;
    [SerializeField] private PlanetPositionGenerationData data;
    private List<List<Vector3>> planetPositions;
    public List<List<Vector3>> PlanetPositions => planetPositions;

    private void OnDrawGizmos()
    {
        /*Gizmos.DrawWireSphere(origin, wireSphereRadius);
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
        }*/
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

    public void ConsumePlanetPosition(int planetCycleIndex, int planetPositionIndex)
    {
        planetPositions[planetCycleIndex].RemoveAt(planetPositionIndex);
    }
}
