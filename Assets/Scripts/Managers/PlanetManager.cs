using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private GameObject planetGameObject;
    
    public void InstantiatePlanets()
    {
        foreach (List<Vector3> planetCycles in GameManager.Instance.PositionManager.PlanetPositions)
        {
            foreach (Vector3 position in planetCycles)
            {
                GameObject newPlanet = Instantiate(planetGameObject, position, Quaternion.identity);
                PlanetData newPlanetData = ScriptableObject.CreateInstance<PlanetData>();
                newPlanetData.PopulatePlanetData("Poo Poo Pee Pee", GameManager.Instance.ResourceManager.GetStartingPlanetResources());
                newPlanet.GetComponentInChildren<PlanetController>().ConfigurePlanet(newPlanetData);
            }
        }
    }
}
