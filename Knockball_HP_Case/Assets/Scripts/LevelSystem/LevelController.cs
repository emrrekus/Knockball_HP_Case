
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSO[] _levels;


    public int GetLevelChildCount(int i)
    {
        return _levels[i].ChildCount;
    }

    public int GetCurrentLevel(int i)
    {
        return _levels[i].Level;
    }

    public int GetNeededBall(int i)
    {
        return _levels[i].NeededBall;
    }
}