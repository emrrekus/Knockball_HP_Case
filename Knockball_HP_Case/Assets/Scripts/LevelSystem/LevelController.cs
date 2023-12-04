using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : SingletonDerivedClasses
{
    [SerializeField] private LevelSO[] _levels;
    [SerializeField] private GameObject[] _levelGameObjects;
    [SerializeField] private DroppedObject _droppedObject;
    [SerializeField] private AudioClip _winAudioClip;
    [SerializeField] private AudioClip _loseAudioClip;
    [SerializeField] private Timer _timer;

    [SerializeField] private int _timeRule;
    [SerializeField] private int _ballRule;

    private int _beginLevel;
    private int _currentLevel;
    private int _dropObjects;
    private int _currentChildCount;
    private int _neededBall;
   
    private int _clipsPlay;

    private bool _isDropCompleted;

    public float CurrentBall;
    public float NeededBall => _neededBall;
    public float CurrentLevel => _currentLevel;
    public float ChildCount => _currentChildCount;
    public float DropObject => _dropObjects;
  
    protected override void OnAwake()
    {
       
        _droppedObject=GetComponent<DroppedObject>();
    }

    private void Start()
    {
        
       StartUp();

    }

    private void Update()
    {
        if (CurrentBall <= _ballRule)
        {
            
            _timer.Counter(true);
            _gameManagerInstance.CanShot(false);
        }

        if (_timer.CurrentTime > _timeRule)
        {
            _timer.Counter(false);
            _gameManagerInstance.CanShot(true);
            LevelFail();
            _gameManagerInstance.LoseCompleted();
        }

        if (_isDropCompleted)
        {
            _timer.Counter(false);
            _gameManagerInstance.CanShot(true);
            LevelUp();
            
        }
    }

    private void StartUp()
    {
        _gameManagerInstance.CanShot(false);
        _clipsPlay = 0;
        _beginLevel = 0;
        LevelOpenAndClose(_currentLevel, false);
        _currentLevel = _beginLevel;
        LevelOpenAndClose(_currentLevel, true);
        CurrentLevelDefinition(_currentLevel);
    }
 

    

    public bool DroppedObjectCheck(int fallingObject)
    {
        _dropObjects = _droppedObject.DropObject(fallingObject);

        if (_dropObjects >= _currentChildCount)
        {
            return _isDropCompleted = true;
        }
        else
        {
            return _isDropCompleted = false;
        }
    }


   

    public void LevelUp()
    {
        _isDropCompleted = false;
        LevelOpenAndClose(_currentLevel, false);
        _currentLevel++;
        _audioManagerInstance.PlayOneShotClip(_winAudioClip);
        if (_currentLevel == _levelGameObjects.Length)
        {
            
            _gameManagerInstance.CanShot(false);
            _isDropCompleted = false;
            _currentLevel = _beginLevel;
            _gameManagerInstance.LevelCompleted();
            return;
        }
        LevelOpenAndClose(_currentLevel, true);
        CurrentLevelDefinition(_currentLevel);
        _dropObjects = 0;

    }

    public void LevelFail()
    {
        
        if (_clipsPlay < 1)
            AudioManager.Instance.PlayOneShotClip(_loseAudioClip);

        _clipsPlay++;
        _gameManagerInstance.CanShot(false);
        _isDropCompleted = false;

    }

   
    private void CurrentLevelDefinition(int currentLevel)
    {
        CurrentBall = _levels[currentLevel].NeededBall;
        _neededBall = _levels[currentLevel].NeededBall;
        _currentChildCount = _levels[currentLevel].ChildCount;
    }

   


    public void LevelOpenAndClose(int level, bool activiy)
    {
        _levelGameObjects[level].SetActive(activiy);
    }

   

   
}