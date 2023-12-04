using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonDerivedClasses : MonoBehaviour
{
    
    protected GameManager _gameManagerInstance;
    protected AudioManager _audioManagerInstance;
    private bool _initialized = false;

    protected void Awake()
    {
        if (!_initialized)
        {
            _initialized = true;
            _gameManagerInstance = GameManager.Instance;
            _audioManagerInstance = AudioManager.Instance;
        }
        OnAwake();
    }

    protected abstract void OnAwake();
}
