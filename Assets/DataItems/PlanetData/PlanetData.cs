using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlanetData", fileName = "PlanetData", order = 0)]
public class PlanetData : ScriptableObject
{
    [SerializeField] private GameObject planetGameObject;
    public GameObject PlanetGameObject => planetGameObject;
}
