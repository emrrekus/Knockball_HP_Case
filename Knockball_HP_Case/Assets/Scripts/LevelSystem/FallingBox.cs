using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBox : SingletonDerivedClasses
{
    private int _fallingObject;

    [SerializeField] private LevelController _levelController;
    

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cube>(out var cube))
        {
            _fallingObject++;

            if (_levelController.DroppedObjectCheck(_fallingObject)) _fallingObject = 0;
            // if (_gameManagerInstance.DroppedObjectCheck(_fallingObject)) _fallingObject = 0;
            
            _gameManagerInstance.Score(cube.Point);
        }
    }

    protected override void OnAwake()
    {
        
    }
}