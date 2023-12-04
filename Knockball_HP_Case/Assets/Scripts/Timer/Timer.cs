using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : SingletonDerivedClasses
{
    public float CurrentTime;

    public event Action<float,bool> timer;

    public void Counter(bool activity)
    {
        CurrentTime += Time.deltaTime;
        timer?.Invoke(CurrentTime,activity);
    }

    protected override void OnAwake()
    {
        
    }
}