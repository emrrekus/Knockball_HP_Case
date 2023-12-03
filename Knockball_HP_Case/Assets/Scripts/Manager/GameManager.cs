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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _beginLevel = 0;
    }


    private int _beginScore;
    private int _beginLevel;
    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public event Func<int, int> levelCurrentBall;
    public event Func<int, int> levelCurrentChildCount;


    public int CurrentBall;
    public int ChildCount;

    private float _currentTime;
    private float _clipsPlay;
    public event Action<float, bool> timer;

    private float _droppedObject;
    private bool _isDroppedCompleted;
  
    public float DroppedObject => _droppedObject;

    private float _score;
    public float Point => _score;
    private bool _isCanShoot = true;
    public bool CanShoot => _isCanShoot;


    public float NeededBall;

    public bool isWinorLosePanelOpen;


    public event Action loseUI;
    public event Action winUI;

    public event Action<int, bool> levelOpenAndClose;
    public event Func<int> levelsLenght;


    private void Start()
    {
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
            Timer(true, false);
        }

        if (_currentTime > 5)
        {
            Timer(false, true);
            Lose();
        }


        if (_isDroppedCompleted)
        {
            Timer(false, true);
            LevelUp();
        }
    }

    public void LevelUp()
    {
        _currentTime = 0f;
        
        _isDroppedCompleted = false;

        levelOpenAndClose?.Invoke(_currentLevel, false);
        _currentLevel++;

        AudioManager.Instance.PlayOneShotClip(2);
        if (_currentLevel == levelsLenght?.Invoke())
        {
            winUI?.Invoke();
            _isCanShoot = false;
            _isDroppedCompleted = false;
            _currentLevel = _beginLevel;
            return;
        }

        levelOpenAndClose?.Invoke(_currentLevel, true);

        CurrentLevelDefinition(_currentLevel);
        _droppedObject = 0f;
    }

    public void Lose()
    {
        
        if (_clipsPlay < 1)
            AudioManager.Instance.PlayOneShotClip(3);

        _clipsPlay++;
        _isCanShoot = false;
        _isDroppedCompleted = false;
        _currentTime = 0;
        loseUI?.Invoke();
    }

    public void Startup()
    {
        _clipsPlay = 0;
        Timer(false, false);
        isWinorLosePanelOpen = false;
        _isCanShoot = false;
        _score = _beginScore;
        levelOpenAndClose?.Invoke(_currentLevel, false);
        _currentLevel = _beginLevel;
        levelOpenAndClose?.Invoke(_currentLevel, true);
        CurrentLevelDefinition(_currentLevel);
    }

    public bool DroppedObjectCheck(float fallingObject)
    {
        _droppedObject = fallingObject;


        if (_droppedObject >= ChildCount && !isWinorLosePanelOpen)
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

    private void Timer(bool activity, bool canShoot)
    {
        _isCanShoot = canShoot;
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