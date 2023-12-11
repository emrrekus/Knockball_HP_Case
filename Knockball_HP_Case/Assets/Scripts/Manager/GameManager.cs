using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Camera _camera;
    private bool _isCanShoot = true;
    private int _beginScore = 0;
    private float _score;
    private float _droppedObject;
    private bool _isCanPlay;

    public bool CanShoot => _isCanShoot;
    
    public float Point => _score;
    public event Action LoseUI;
    public event Action WinUI;
   

    public void CanShot(bool canShoot)
    {
        _isCanShoot = canShoot;
    }

  
    public void LevelCompleted()
    {
        WinUI?.Invoke();
        SaveScore();
        
    }

    public void LoseCompleted()
    {
       LoseUI?.Invoke();
       SaveScore();
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void ScoreDelete()
    {
        _score = _beginScore;
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

   


    

    public void CameraShake(float shakeDuration, float shakeStrenght)
    {
        _camera.DOShakePosition(shakeDuration, shakeStrenght, fadeOut: true);
        _camera.DOShakeRotation(shakeDuration, shakeStrenght, fadeOut: true);
    }

   


    


   
}