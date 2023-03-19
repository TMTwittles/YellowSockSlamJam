using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    void Update()
    {
        transform.RotateAround(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
