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

    [SerializeField] private LevelController _levelController;
    [SerializeField] private LoseVignette _loseVignette;
    [SerializeField] private GameObject[] _levels;

    public int CurrentBall;
    public int ChildCount;

    private int _beginLevel = 0;
    private int _currentLevel;
    private int _droppedObject;

    private bool _isLevelCompleted;
    private bool _isDroppedCompleted;

    private bool _isCanShoot = true;

    public bool CanShoot => _isCanShoot;

    public event Action vignette;
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
        if (CurrentBall <= 0) Lose();
        if (_isDroppedCompleted)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
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
    }

    public void Lose()
    {
        
        _isCanShoot = false;
        vignette?.Invoke();
        
    }

    private void Startup()
    {
        
        _currentLevel = _beginLevel;
        CurrentLevelDefinition(_currentLevel);
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


    private void CurrentLevelDefinition(int currentLevel)
    {
        CurrentBall = _levelController.GetNeededBall(currentLevel);
        ChildCount = _levelController.GetLevelChildCount(currentLevel);
    }
}