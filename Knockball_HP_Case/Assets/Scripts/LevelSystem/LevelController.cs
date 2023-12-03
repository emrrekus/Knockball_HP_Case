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

    #region Level Region
     //I transfer the data that I have kept level scriptableobject to the gamemanager with the event
    public int GetLevelChildCount(int i)
    {
        return _levels[i].ChildCount;
    }


    public int GetNeededBall(int i)
    {
        return _levels[i].NeededBall;
    }

    #endregion


    #region LevelGameObjectSettings

    public void LevelOpenAndClose(int level, bool activiy)
    {
        _levelGameObjects[level].SetActive(activiy);
    }

    public int GetLevelLenght()
    {
        return _levelGameObjects.Length;
    }

    #endregion
   
}