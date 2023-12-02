using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBox : MonoBehaviour
{
    private int _fallingObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            _fallingObject++;
            Debug.Log(_fallingObject);
            if (GameManager.Instance.DroppedObjectCheck(_fallingObject))
            {
                _fallingObject = 0;
            }
           
        }
    }
}