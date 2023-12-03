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

    private void Start()
    {
        Startup();
    }


    private void Update()
    {
        LevelControll();
        SaveScore();
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
    #region UI

    public bool isWinorLosePanelOpen;


    public event Action loseUI;
    public event Action winUI;

    #endregion

    #region Shoot

    private bool _isCanShoot = true;
    public bool CanShoot => _isCanShoot;

    public void GameisPlay(bool canPlay)
    {
        _isCanShoot = canPlay;
    }

    #endregion

    #region Level

    private int _beginLevel;
    private int _currentLevel;
    public int CurrentBall;
    public int ChildCount;
    public float NeededBall;
    public int CurrentLevel => _currentLevel;
    private float _clipsPlay;
    public event Action<int, bool> levelOpenAndClose;
    public event Func<int> levelsLenght;
    public event Func<int, int> levelCurrentBall;
    public event Func<int, int> levelCurrentChildCount;

    public void LevelControll()
    {
        //Timer starts if the number of balls needed at the current level is exhausted
        if (CurrentBall <= 0)
        {
            Timer(true, false);
        }

        //If the timer starts after 5 seconds, the game is considered lost and the lose panel opens
        if (_currentTime > 5)
        {
            Timer(false, true);
            Lose();
        }

        //If the targeted objects have been dropped before the required cannon is exhausted, the current level is checked and the win panel or the next level is passed.
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

    private void CurrentLevelDefinition(int currentLevel)
    {
        CurrentBall = levelCurrentBall.Invoke(currentLevel);
        NeededBall = CurrentBall;
        ChildCount = levelCurrentChildCount.Invoke(currentLevel);
    }

    #endregion


    #region Score

    private int _beginScore;
    private float _score;
    public float Point => _score;
    private float _droppedObject;
    private bool _isDroppedCompleted;
    public float DroppedObject => _droppedObject;

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

    public void Score(float score)
    {
        _score += score;
    }

    private void SaveScore()
    {
        if (_score > ScoreData.GetPlayerScore())
            ScoreData.SavePlayerScore(_score);
    }

    #endregion

    #region Time

    private float _currentTime;
    public event Action<float, bool> timer;

    private void Timer(bool activity, bool canShoot)
    {
        _isCanShoot = canShoot;
        _currentTime += Time.deltaTime;
        timer?.Invoke(_currentTime, activity);
    }

    #endregion

    #region Camera

    public void CameraShake(float shakeDuration, float shakeStrenght)
    {
        Camera.main.DOShakePosition(shakeDuration, shakeStrenght, fadeOut: true);
        Camera.main.DOShakeRotation(shakeDuration, shakeStrenght, fadeOut: true);
    }

    #endregion


    


   
}