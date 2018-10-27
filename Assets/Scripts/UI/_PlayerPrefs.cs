using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefs : MonoBehaviour {

    public bool HasLevelEnd { get; set; }

    public int Score { get; private set; }

    private void Awake()
    {
        LoadPlayerProgress();   
    }
    
    private void Update()
    {
        if(HasLevelEnd)
        {
            if (ScoreManager.score > Score)
            {
                UnityEngine.PlayerPrefs.SetInt("highestScore", ScoreManager.score);
            }

            UnityEngine.PlayerPrefs.SetInt("currentScore", ScoreManager.score);

            //SceneManager.LoadScene("MenuJAMScore");
        }
    }

    private void LoadPlayerProgress()
    {
        if (UnityEngine.PlayerPrefs.HasKey("highestScore"))
        {
            Score = UnityEngine.PlayerPrefs.GetInt("highestScore");
        }
    }


}
