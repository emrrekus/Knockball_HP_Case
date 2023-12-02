using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : MonoBehaviour
{
    private const string PlayerScoreKey = "PlayerScore";


    public static void SavePlayerScore(float score)
    {
        PlayerPrefs.SetFloat(PlayerScoreKey,score);
        PlayerPrefs.Save();
    }

    public static float GetPlayerScore()
    {
        return PlayerPrefs.GetFloat(PlayerScoreKey);
    }


    public static void ResetPlayerScore()
    {
        PlayerPrefs.DeleteKey(PlayerScoreKey);
        PlayerPrefs.Save();
    }
    
}
