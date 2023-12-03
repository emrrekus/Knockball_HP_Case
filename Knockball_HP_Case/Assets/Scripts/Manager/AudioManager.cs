using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


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
    }

    [SerializeField] private AudioClip[] _oneShotclips;
    [SerializeField] private AudioSource _audioSource;

   


    private void Update()
    {
        Volume();
    }

    public void PlayOneShotClip(int clips)
    {
        _audioSource.PlayOneShot(_oneShotclips[clips]);
    }

    private void Volume()
    {
        _audioSource.volume = AudioVolumeData.GetAudioVolume();
    }
}