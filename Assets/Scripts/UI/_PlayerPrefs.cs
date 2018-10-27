using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _PlayerPrefs : MonoBehaviour {

    GameObject player;
    float playerHealth;

    private int highestScore;

    private void Awake()
    {
        LoadPlayerProgress();   
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(playerHealth<= 0)
        {
            if (ScoreManager.score > highestScore)
            {
                PlayerPrefs.SetInt("highestScore", ScoreManager.score);
            }

            PlayerPrefs.SetInt("currentScore", ScoreManager.score);

            SceneManager.LoadScene("MenuJAMScore");
        }
    }

    private void LoadPlayerProgress()
    {
        if (PlayerPrefs.HasKey("highestScore"))
        {
            highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    public int GetHighestPlayerScore()
    {
        return highestScore;
    }

}
