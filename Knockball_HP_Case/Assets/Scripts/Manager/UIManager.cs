using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum UIPage
{
    MainMenu,
    GamePlay,
    Lose,
    Win
}


public class UIManager : SingletonDerivedClasses
{
    [Header("Levels")] [SerializeField] private TMP_Text _level;
    [SerializeField] private Image _backgroundLevelBar;

    [Header("BallsCount")] [SerializeField]
    private TMP_Text _ballsCount;

    [SerializeField] private Image _bacgroundBallsCount;

    [Header("Score")] [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private TMP_Text _loseScoreText;
    [SerializeField] private TMP_Text _winScoreText;

    [Header("Time")] [SerializeField] private TMP_Text _time;
    [SerializeField] private GameObject _timerObject;

    [Header("Audio")] [SerializeField] private GameObject _audioSlider;
    [SerializeField] private Slider _slider;

    [Header("UIs")] [SerializeField] private GameObject[] gameUI;

    [Header("Buttons")] [SerializeField] private GameObject _mainMenuButtons;
    [SerializeField] private GameObject _exitButtons;

    [Header("Implement Class")] [SerializeField]
    private Timer _timer;

    [SerializeField] private LevelController _levelController;

    [Header("Level Scene")] [SerializeField]
    private string _sceneName;

    [Header("Input Handler")] [SerializeField]
    private InputHandler _ınputHandler;

    private bool _sliderActivity;
    private int _activeUI;
    private int _tryPlay;


    private void Start()
    {
        _tryPlay = 0;
        _slider.value = AudioVolumeData.GetAudioVolume();
        BestScore();
    }

    private void OnEnable()
    {
        _timer.timer += TimerText;
        _gameManagerInstance.loseUI += LoseGame;
        _gameManagerInstance.winUI += WinGame;
    }

    private void OnDisable()
    {
        _timer.timer -= TimerText;
        _gameManagerInstance.loseUI -= LoseGame;
        _gameManagerInstance.winUI -= WinGame;
    }

    private void Update()
    {
        LevelBar();
        BallsCount();
        Score();
        
        if(_ınputHandler.TouchStart() && _tryPlay <1)
                PlayGame();
    }


    #region UI Buttons

    public void PlayGame()
    {
        _tryPlay++;
        _gameManagerInstance.CanShot(true);
        gameUI[(int)UIPage.MainMenu].SetActive(false);
        gameUI[(int)UIPage.GamePlay].SetActive(true);
        _activeUI = (int)UIPage.GamePlay;
    }

    private void LoseGame()
    {
        _activeUI = (int)UIPage.Lose;
        _mainMenuButtons.SetActive(false);
        _exitButtons.SetActive(false);
        gameUI[(int)UIPage.GamePlay].SetActive(false);
        gameUI[(int)UIPage.Lose].SetActive(true);
        _loseScoreText.text = "Score: " + _score.text;
    }

    private void WinGame()
    {
        _mainMenuButtons.SetActive(false);
        _exitButtons.SetActive(false);
        gameUI[(int)UIPage.GamePlay].SetActive(false);
        gameUI[(int)UIPage.Win].SetActive(true);
        _activeUI = (int)UIPage.Win;
        _winScoreText.text = "Score: " + _score.text;
    }


    public void TryGame()
    {
        gameUI[_activeUI].SetActive(false);
        _gameManagerInstance.CanShot(false);
        _mainMenuButtons.SetActive(true);
        _exitButtons.SetActive(true);
        _gameManagerInstance.ScoreDelete();
        gameUI[(int)_levelController.CurrentLevel].SetActive(true);
        _gameManagerInstance.SceneLoad(_sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion


    #region UI Text

    private void LevelBar()
    {
        _level.text = (_levelController.CurrentLevel + 1).ToString();
        _backgroundLevelBar.fillAmount = _levelController.DropObject / _levelController.ChildCount;
    }

    private void BallsCount()
    {
        _ballsCount.text = _levelController.CurrentBall.ToString();
        _bacgroundBallsCount.fillAmount = _levelController.CurrentBall / _levelController.NeededBall;
    }

    private void Score()
    {
        _score.text = _gameManagerInstance.Point.ToString();
    }

    private void TimerText(float currentTime, bool activity)
    {
        _timerObject.SetActive(activity);
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _time.text = string.Format("{0:D2},{1:D3}", timeSpan.Seconds, timeSpan.Milliseconds);
    }


    private void BestScore()
    {
        _bestScore.text = "Best: " + ScoreData.GetPlayerScore().ToString();
    }

    #endregion

    #region UI Audio

    public void AudioSettings()
    {
        _sliderActivity = !_sliderActivity;
        _audioSlider.SetActive(_sliderActivity);

        SliderValue();
    }

    private void SliderValue()
    {
        if (_slider.value != AudioVolumeData.GetAudioVolume())
            AudioVolumeData.SaveAudioVolume(_slider.value);
    }

    #endregion


    protected override void OnAwake()
    {
    }
}