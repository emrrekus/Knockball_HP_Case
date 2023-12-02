using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSO[] _levels;


    private void OnEnable()
    {
        GameManager.Instance.levelCurrentChildCount += GetLevelChildCount;
        GameManager.Instance.levelCurrentBall += GetNeededBall;
    }

    private void OnDisable()
    {
        GameManager.Instance.levelCurrentChildCount -= GetLevelChildCount;
        GameManager.Instance.levelCurrentBall -= GetNeededBall;
    }


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