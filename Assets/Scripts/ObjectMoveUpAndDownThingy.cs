using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveUpAndDownThingy : MonoBehaviour
{
    // This is the laziest script
    
    [SerializeField] private float secondsMoveUpAndDown;
    [SerializeField] private float moveUpAmount;
    private bool moveUp = true;
    private float elapsedTime = 0.0f;
    
    
    private void Update()
    {
        elapsedTime += Time.deltaTime * GameManager.Instance.TimeManager.TimeModifier;

        if (moveUp)
        {
            transform.position += new Vector3(0.0f, moveUpAmount, 0.0f);
        }
        else
        {
            transform.position -= new Vector3(0.0f, -moveUpAmount, 0.0f);
        }

        if (elapsedTime > secondsMoveUpAndDown)
        {
            elapsedTime = 0.0f;
            moveUp = !moveUp;
        }
    }
}
