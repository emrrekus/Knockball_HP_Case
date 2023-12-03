using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBox : MonoBehaviour
{
    private float _fallingObject;
    [SerializeField] private string _triggerObjectName;
    [SerializeField] private string _triggerSecondObjectName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_triggerObjectName) || other.CompareTag(_triggerSecondObjectName))
        {
            _fallingObject++;

            if (GameManager.Instance.DroppedObjectCheck(_fallingObject)) _fallingObject = 0;
        }

        if (other.TryGetComponent<Cube>(out var cube))
        {
           
            GameManager.Instance.Score(cube.Point);
        }
    }
}