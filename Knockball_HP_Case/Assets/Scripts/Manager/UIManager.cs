using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Levels")] [SerializeField] private TMP_Text _level;
    [SerializeField] private Image _backgroundLevelBar;

    [Header("BallsCount")] [SerializeField]
    private TMP_Text _ballsCount;

    [SerializeField] private Image _bacgroundBallsCount;

    [Header("Score")] [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _bestScore;

    [Header("Time")] [SerializeField] private TMP_Text _time;
    [SerializeField] private GameObject _timerObject;

    [Header("UIs")] [SerializeField] private GameObject[] gameUI;

    private int activeUI;

    private void Start()
    {
        BestScore();
    }

    private void OnEnable()
    {
        GameManager.Instance.timer += Timer;
        GameManager.Instance.loseUI += LoseGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.timer -= Timer;
        GameManager.Instance.loseUI -= LoseGame;
    }

    private void Update()
    {
        LevelBar();
        BallsCount();
        Score();
    }


    private void LevelBar()
    {
        _level.text = (GameManager.Instance.CurrentLevel + 1).ToString();
        _backgroundLevelBar.fillAmount = GameManager.Instance.DroppedObject / GameManager.Instance.ChildCount;
    }

    private void BallsCount()
    {
        _ballsCount.text = GameManager.Instance.CurrentBall.ToString();
        _bacgroundBallsCount.fillAmount = GameManager.Instance.CurrentBall / GameManager.Instance.NeededBall;
    }

    private void Score()
    {
        _score.text = GameManager.Instance.Point.ToString();
    }

    private void Timer(float currentTime, bool activity)
    {
        _timerObject.SetActive(activity);
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _time.text = string.Format("{0:D2},{1:D3}", timeSpan.Seconds, timeSpan.Milliseconds);
    }

    private void BestScore()
    {
        _bestScore.text = "Best: " + ScoreData.GetPlayerScore().ToString();
    }

    public void PlayGame()
    {
        GameManager.Instance.GameisPlay(true);
        gameUI[0].SetActive(false);
        gameUI[1].SetActive(true);
    }

    private void LoseGame()
    {
        gameUI[1].SetActive(false);
        gameUI[2].SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void TryGame()
    {
       
        gameUI[2].SetActive(false);
        gameUI[0].SetActive(true);
        GameManager.Instance.Startup();
        SceneManager.LoadScene("SampleScene");

    }
}