using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private GameObject planetGameObject;
    private int currentCycle;
    private int numPlanetsCreated = 0;
    private int currentPlanetPositionIndex = 0;
    private int currentPlanetCycleIndex = 0;
    private Dictionary<string, GameObject> planetGameObjectsDict;

    // This script needs to be cleaned, shits fucked. - Arvie
    
    public void InstantiatePlanets(int numPlanetsToInstantiate)
    {
        if (planetGameObjectsDict == null)
        {
            planetGameObjectsDict = new Dictionary<string, GameObject>();
        }
        
        int newPlanetsCreated = 0;

        for (int planetCycle = currentPlanetCycleIndex; planetCycle < GameManager.Instance.PositionManager.PlanetPositions.Count; planetCycle++)
        {
            int numPlanetsInCycle = GameManager.Instance.PositionManager.PlanetPositions[planetCycle].Count;
            for (int planetPosition = currentPlanetPositionIndex; planetPosition < numPlanetsInCycle; planetPosition++)
            {
                Vector3 newPlanetPosition =
                    GameManager.Instance.PositionManager.PlanetPositions[planetCycle][planetPosition];
                GameObject newPlanet = Instantiate(planetGameObject, newPlanetPosition, Quaternion.identity);
                PlanetData newPlanetData = ScriptableObject.CreateInstance<PlanetData>();
                string newPlanetName = $"planet {numPlanetsCreated + 1}";
                planetGameObjectsDict.Add(newPlanetName, planetGameObject);
                newPlanetData.PopulatePlanetData(newPlanetName, newPlanetPosition, GameManager.Instance.ResourceManager.GetStartingPlanetResources(numPlanetsCreated));
                newPlanet.GetComponentInChildren<PlanetController>().ConfigurePlanet(newPlanetData);
                newPlanetsCreated += 1;
                numPlanetsCreated += 1;
                currentPlanetPositionIndex += 1;

                if (newPlanetsCreated >= numPlanetsToInstantiate)
                {
                    return;
                }
            }
            currentPlanetPositionIndex = 0;
            currentPlanetCycleIndex += 1;
        }
        
        /*foreach (List<Vector3> planetCycles in GameManager.Instance.PositionManager.PlanetPositions)
        {
            foreach (Vector3 planetPosition in planetCycles)
            {
                if (planetPosition.Taken == false)
                {
                    Vector3 newPlanetPosition = planetPosition.ConsumePlanetPosition();
                    GameObject newPlanet = Instantiate(planetGameObject, newPlanetPosition, Quaternion.identity);
                    PlanetData newPlanetData = ScriptableObject.CreateInstance<PlanetData>();
                    newPlanetData.PopulatePlanetData("Poo Poo Pee Pee", GameManager.Instance.ResourceManager.GetStartingPlanetResources(numPlanetsCreated));
                    newPlanet.GetComponentInChildren<PlanetController>().ConfigurePlanet(newPlanetData);
                    newPlanetsCreated += 1;
                    numPlanetsCreated += 1;
                }

                if (newPlanetsCreated >= numPlanetsToInstantiate)
                {
                    return;
                }
            }
        }*/
    }

    public PlanetController GetPlanetController(string planetName)
    {
        return planetGameObjectsDict[planetName].GetComponentInChildren<PlanetController>();
    }
}
