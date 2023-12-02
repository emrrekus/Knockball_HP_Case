using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public float Point;


    private void Start()
    {
        Point = Random.Range(1, 20);
    }
}
