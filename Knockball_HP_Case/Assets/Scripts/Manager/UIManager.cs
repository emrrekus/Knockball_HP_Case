using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _ballsCount;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Image _backgroundLevelBar;
    [SerializeField] private Image _bacgroundBallsCount;
    


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
}