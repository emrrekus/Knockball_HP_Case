using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBox : MonoBehaviour
{
    private int _fallingObject;
    [SerializeField] private string _triggerObjectName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_triggerObjectName) || other.CompareTag("Explode"))
        {
            _fallingObject++;

            if (GameManager.Instance.DroppedObjectCheck(_fallingObject)) _fallingObject = 0;
        }
    }
}