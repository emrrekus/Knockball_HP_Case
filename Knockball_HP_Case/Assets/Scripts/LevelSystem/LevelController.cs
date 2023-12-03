using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSO[] _levels;
    [SerializeField] private GameObject[] _levelGameObjects;


    private void OnEnable()
    {
        GameManager.Instance.levelCurrentChildCount += GetLevelChildCount;
        GameManager.Instance.levelCurrentBall += GetNeededBall;
        GameManager.Instance.levelOpenAndClose += LevelOpenAndClose;
        GameManager.Instance.levelsLenght += GetLevelLenght;
    }

    private void OnDisable()
    {
        GameManager.Instance.levelCurrentChildCount -= GetLevelChildCount;
        GameManager.Instance.levelCurrentBall -= GetNeededBall;
        GameManager.Instance.levelOpenAndClose -= LevelOpenAndClose;
        GameManager.Instance.levelsLenght -= GetLevelLenght;
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

    public void LevelOpenAndClose(int level, bool activiy)
    {
        _levelGameObjects[level].SetActive(activiy);
    }

    public int GetLevelLenght()
    {
       
        return _levelGameObjects.Length;
    }
}