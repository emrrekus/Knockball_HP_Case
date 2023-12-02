using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        
    public static GameManager Instance { get; private set; }
    private void Awake() 
    { 
       
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField] private LevelController _levelController;
    [SerializeField] private GameObject[] _levels;

    public int CurrentBall;
    public int ChildCount;

    private int _beginLevel = 0;
    private int _currentLevel;
    private int _droppedObject;

    private bool _isLevelCompleted;
    private bool _isDroppedCompleted;
    

    private void Start()
    {
       Startup();
    }


    private void Update()
    {
       
    }

    public void CameraShake(float shakeDuration, float shakeStrenght)
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrenght,fadeOut:true);
        Camera.main.DOShakeRotation(shakeDuration, shakeStrenght,fadeOut:true);
    }



    public void LevelControll()
    {
        if(CurrentBall<=0) Lose();
        
    }


    public void Lose()
    {
        
    }

    private void Startup()
    {
        _currentLevel = _beginLevel;
        CurrentBall = _levelController.GetNeededBall(_currentLevel);
        ChildCount = _levelController.GetLevelChildCount(_currentLevel);
    }

    public bool DroppedObjectCheck(int fallingObject)
    {
        _droppedObject = fallingObject;

        if (_droppedObject >= ChildCount)
        {
             return _isDroppedCompleted = true;
        }
        else
        {
            return _isDroppedCompleted = false;
        } 
       
    }
    
}
