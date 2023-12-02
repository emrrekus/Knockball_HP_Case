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

        _beginLevel = 0;
    }


    [Header("Levels")] [SerializeField] private GameObject[] _levels;
    private int _beginLevel;
    private int _currentLevel;
    private bool _isLevelCompleted;
    public int CurrentLevel => _currentLevel;

    public event Func<int, int> levelCurrentBall;
    public event Func<int, int> levelCurrentChildCount;


    public int CurrentBall;
    public int ChildCount;

    private float _currentTime;
    public event Action<float, bool> timer;

    private float _droppedObject;
    private bool _isDroppedCompleted;
    public float DroppedObject => _droppedObject;

    private float _score;
    public float Point => _score;
    private bool _isCanShoot = true;
    public bool CanShoot => _isCanShoot;


    public float NeededBall;


    public event Action vignetteOpen;
    public event Action vignetteClose;

    public event Action loseUI;


    private void Start()
    {
        _isCanShoot = false;
        Startup();
    }


    private void Update()
    {
        LevelControll();
        SaveScore();
    }

    public void CameraShake(float shakeDuration, float shakeStrenght)
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrenght, fadeOut: true);
        Camera.main.DOShakeRotation(shakeDuration, shakeStrenght, fadeOut: true);
    }


    public void LevelControll()
    {
        if (CurrentBall <= 0)
        {
            Timer(true);
            vignetteOpen?.Invoke();
        }

        if (_currentTime > 5)
        {
            Timer(false);
            Lose();
            _currentTime = 0;
        }

        if (_isDroppedCompleted)
        {
            vignetteClose?.Invoke();
            Timer(false);
            LevelUp();
        }
    }

    public void LevelUp()
    {
        _currentTime = 0f;
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
        _isLevelCompleted = true;
    }

    public void Lose()
    {
        _isCanShoot = false;
        _isDroppedCompleted = false;
        loseUI?.Invoke();
    }

    public void Startup()
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
    }

    private void Timer(bool activity)
    {
        _isCanShoot = activity ? false : true;
        _currentTime += Time.deltaTime;
        timer?.Invoke(_currentTime, activity);
    }

    public void GameisPlay(bool canPlay)
    {
        _isCanShoot = canPlay;
    }


    private void SaveScore()
    {
        if (_score > ScoreData.GetPlayerScore())
            ScoreData.SavePlayerScore(_score);
    }
}