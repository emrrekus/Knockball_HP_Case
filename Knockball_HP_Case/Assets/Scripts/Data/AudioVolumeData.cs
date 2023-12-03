using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeData : MonoBehaviour
{
    private const string AudioVolume = "AudioVolume";
    public static void SaveAudioVolume(float volume)
    {
        PlayerPrefs.SetFloat(AudioVolume,volume);
        PlayerPrefs.Save();
    }

    public static float GetAudioVolume()
    {
        return PlayerPrefs.GetFloat(AudioVolume);
    }


    public static void ResetAudioVolume()
    {
        PlayerPrefs.DeleteKey(AudioVolume);
        PlayerPrefs.Save();
    }
}
