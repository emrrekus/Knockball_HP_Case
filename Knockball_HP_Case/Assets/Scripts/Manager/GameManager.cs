using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
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

    [SerializeField] private GameObject[] _levels;

    public int CurrentBall;
    public int ChildCount;

    private int _beginLevel = 0;
    private int _currentLevel;
    private float _droppedObject;
    private float _score;

    private bool _isDroppedCompleted;

    private bool _isCanShoot = true;

    public bool CanShoot => _isCanShoot;
    public int CurrentLevel => _currentLevel;
    public float DroppedObject => _droppedObject;
    public float NeededBall;
    public float Point => _score;
    
    public event Action vignetteOpen;
    public event Action vignetteClose;
    public event Func<int, int> levelCurrentBall;
    public event Func<int, int> levelCurrentChildCount;

    private void Start()
    {
        _isCanShoot = true;
        Startup();
    }


    private void Update()
    {
        LevelControll();
    }

    public void CameraShake(float shakeDuration, float shakeStrenght)
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrenght, fadeOut: true);
        Camera.main.DOShakeRotation(shakeDuration, shakeStrenght, fadeOut: true);
    }


    public void LevelControll()
    {
        if (CurrentBall <= 0 && !_isDroppedCompleted) Lose();
        if (_isDroppedCompleted)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        vignetteClose?.Invoke();
        _isCanShoot = true;
        _isDroppedCompleted = false;
        _levels[_currentLevel].SetActive(false);
        _currentLevel++;


        if (_currentLevel == _levels.Length)
        {
            _isDroppedCompleted = false;
            return;
        }

        _levels[_currentLevel].SetActive(true);

        CurrentLevelDefinition(_currentLevel);
        _droppedObject = 0f;
    }

    public void Lose()
    {
        Debug.Log("Loseee");
        _isCanShoot = false;
        vignetteOpen?.Invoke();
        _isDroppedCompleted = false;
        return;
    }

    private void Startup()
    {
        _currentLevel = _beginLevel;
        CurrentLevelDefinition(_currentLevel);
    }

    public bool DroppedObjectCheck(float fallingObject)
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


    private void CurrentLevelDefinition(int currentLevel)
    {
        CurrentBall = levelCurrentBall.Invoke(currentLevel);
        NeededBall = CurrentBall;
        ChildCount = levelCurrentChildCount.Invoke(currentLevel);
    }


    public void Score(float score)
    {
        _score += score;
        Debug.Log(_score);
    }
    
    
}