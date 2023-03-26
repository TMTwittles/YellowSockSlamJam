using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private RatKingData ratKingData;
    public RatKingData RatKingData => ratKingData;
    
    [SerializeField] private GameObject planetGameObject;
    [SerializeField] private GameObject ratKingGameObject;
    [SerializeField] private LazyCameraController cameraController; //sorry, not ideal, but this is the easiest way to access the planet position index
    private int currentCycle;
    private int numPlanetsCreated = 0;
    private int numRatKingsCreated = 0;
    private int currentPlanetPositionIndex = 0;
    private int currentPlanetCycleIndex = 0;
    private Dictionary<string, GameObject> planetGameObjectsDict;
    // Every 5 planets create new rat king.
    private int numPlanetsCreateRatKing = 5;

    // This script needs to be cleaned, shits fucked. - Arvie

    public void InstantiateStartingPlanets()
    {
        InstantiateRatKing(Vector3.zero);
        InstantiatePlanets(GameManager.Instance.StateManager.GetGameStateData().NumStartingPlanets);
    }

    public void InstantiateRatKing(Vector3 position)
    {
        if (planetGameObjectsDict == null)
        {
            planetGameObjectsDict = new Dictionary<string, GameObject>();
        }

        numRatKingsCreated += 1;
        Vector3 newPlanetPosition = position;
        GameObject newPlanet = Instantiate(ratKingGameObject, newPlanetPosition, Quaternion.identity);
        RatKingData newPlanetData = ScriptableObject.CreateInstance<RatKingData>();
        string newPlanetName = $"RatKing {numRatKingsCreated + 1}";
        planetGameObjectsDict.Add(newPlanetName, planetGameObject);
        newPlanetData.SetRatKingStructureToBuy(ratKingData.StructuresToBuy);
        // Really need to fix magic numbers on planet radius - Arvie
        newPlanetData.PopulateRatKingData(newPlanetName, newPlanetPosition, 0.03f * 500, GameManager.Instance.ResourceManager.GetStartingPlanetPopulation(numPlanetsCreated), GameManager.Instance.ResourceManager.GetStartingPlanetResources(numPlanetsCreated));
        newPlanet.GetComponentInChildren<RatKingController>().ConfigureRatKing(newPlanetData);
    }
    
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
                if (numPlanetsCreated != 0 && numPlanetsCreated % numPlanetsCreateRatKing == 0)
                {
                    InstantiateRatKing(newPlanetPosition);
                }
                else
                {
                    GameObject newPlanet = Instantiate(planetGameObject, newPlanetPosition, Quaternion.identity);
                    PlanetData newPlanetData = ScriptableObject.CreateInstance<PlanetData>();
                    string newPlanetName = $"planet {numPlanetsCreated + 1}";
                    planetGameObjectsDict.Add(newPlanetName, planetGameObject);
                    // Really need to fix magic numbers on planet radius - Arvie
                    newPlanetData.PopulatePlanetData(newPlanetName, newPlanetPosition, 0.5f * 8, GameManager.Instance.ResourceManager.GetStartingPlanetPopulation(numPlanetsCreated), GameManager.Instance.ResourceManager.GetStartingPlanetResources(numPlanetsCreated));
                    newPlanet.GetComponentInChildren<PlanetController>().ConfigurePlanet(newPlanetData);
                    newPlanetsCreated += 1;
                }
                numPlanetsCreated += 1;
                currentPlanetPositionIndex += 1;
                
                if (newPlanetsCreated >= numPlanetsToInstantiate)
                {
                    if (newPlanetPosition.magnitude > cameraController.FurthestPlanetMagnitude)
                        cameraController.FurthestPlanetMagnitude = newPlanetPosition.magnitude;
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
