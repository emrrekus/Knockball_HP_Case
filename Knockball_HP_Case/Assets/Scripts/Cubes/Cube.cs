using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : SingletonDerivedClasses
{
    public float Point;


    private void Start()
    {
        Point = Random.Range(1, 20);
    }

    protected override void OnAwake()
    {
        
    }
}