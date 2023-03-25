using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanet : MonoBehaviour
{
    [Header("Explosion Force")]
    public float explosionPower = 800f;
    public float radius = 3f;
    [Header("Scaling Attributes")]
    public float shrinkRate = 10f;
    public float scaleAmount = 0.1f;

    public float lifetime = 0.7f;

    private Transform[] allChildren;

    void Start()
    {
        allChildren = GetComponentsInChildren<Transform>();

        SelfDestruct();

        Destroy(this.gameObject, lifetime);
    }

    void Update()
    {
        foreach (Transform child in allChildren)
        {
            child.localScale -= new Vector3(scaleAmount, scaleAmount, scaleAmount) * shrinkRate * Time.deltaTime;
        }
    }

    void SelfDestruct()
    {
        Rigidbody m_rigidBody;

        foreach (Transform child in allChildren)
        {
            m_rigidBody = child.gameObject.GetComponent<Rigidbody>();
            if (m_rigidBody)
            {
                m_rigidBody.AddExplosionForce(explosionPower, this.transform.position, radius, 1.0f);
            }
        }
    }
}
