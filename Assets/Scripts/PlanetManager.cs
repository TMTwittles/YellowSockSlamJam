using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private PlanetData planetData;
    
    public void InstantiatePlanets()
    {
        foreach (List<Vector3> planetCycles in GameManager.Instance.PositionMapper.PlanetPositions)
        {
            foreach (Vector3 position in planetCycles)
            {
                Instantiate(planetData.PlanetGameObject, position, Quaternion.identity);
            }
        }
    }
}
