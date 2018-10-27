using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighestScore : MonoBehaviour {

    public GameObject HighText;
    Text _highText;

    public GameObject CurrentText;
    Text _currentText;

    void Awake()
    {
        _highText = HighText.GetComponent<Text>();
        _currentText = CurrentText.GetComponent<Text>();
    }

    private void Start()
    {
        SetHighestScoreText();
    }

    public void ResetHighestScore()
    {
        PlayerPrefs.SetInt("highestScore", 0);
        SetHighestScoreText();
    }

    private void SetHighestScoreText()
    {
        _highText.text = "" + PlayerPrefs.GetInt("highestScore");
        _currentText.text = "" + PlayerPrefs.GetInt("currentScore");
    }
}
