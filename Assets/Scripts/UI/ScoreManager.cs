using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;

    public Text ScoreText;

    void Awake()
    {
        score = 0;
    }

    void Update()
    {
        ScoreText.text = "" + score;
    }
}
