using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
  

    [SerializeField] private AudioSource _audioSource;

   


    private void Update()
    {
        Volume();
    }

    public void PlayOneShotClip(AudioClip clips)
    {
        _audioSource.PlayOneShot(clips);
    }

    private void Volume()
    {
        _audioSource.volume = AudioVolumeData.GetAudioVolume();
    }
}