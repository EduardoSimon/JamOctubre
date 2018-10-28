using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;


public class HUDController : Singleton<HUDController>
{
	public GameObject billetes;
	public GameObject alarmIcons;
	public GameObject alarmIcon;
	public Text score;
	public AudioClip alarmClip;
	public AudioClip addScoreClip;
	
	private Image[] scoreLevels;
	private Image[] alarmImages;
	private Sequence s;
	private Image alarmImage;
	private AudioSource _audioSource;

    private bool activatedAlarm = false;
	

	private void Awake()
	{
		scoreLevels = billetes.GetComponentsInChildren<Image>();
		alarmImages = alarmIcons.GetComponentsInChildren<Image>();
		alarmImage = alarmIcon.GetComponentInChildren<Image>();
		_audioSource = GetComponent<AudioSource>();
		s = DOTween.Sequence();


		StartHud();
	}

	private void OnEnable()
	{
		StartHud();
	}

	private void StartHud()
	{
		score.text = 0.ToString();

		if (scoreLevels != null)
		{
			foreach (var scoreLevel in scoreLevels)
			{
				scoreLevel.enabled = false;
			}
		}
	}

    private void Update()
    {
        if (GameManager.I.alarmEnabled && !activatedAlarm)
        {
            InitializeAlarmSprites();
            activatedAlarm = true;
        }
            
    }

    internal void ResetHUD()
    {
        score.text = 0.ToString();
    }

    public void InitializeAlarmSprites()
	{
		_audioSource.PlayOneShot(alarmClip);
		
		alarmImage.transform.DOShakePosition(0.5f, 5f, 40, 90f, false, true).SetLoops(-1);

		s.AppendCallback(() => { alarmImages[0].enabled = true; });
		//s.Append(alarmImages[0].transform.DOShakePosition(0.4f, 6f, 10, 90, false, true));
		s.AppendInterval(0.1f);
		
		s.AppendCallback(() => { alarmImages[1].enabled = true; });
		//s.Append(alarmImages[1].transform.DOShakePosition(0.4f, 6f, 10, 90, false, true));

		s.AppendInterval(0.1f);
		
		s.AppendCallback(() => { alarmImages[2].enabled = true; });
		//s.Append(alarmImages[2].transform.DOShakePosition(0.4f, 6f, 10, 90, false, true));
		s.AppendInterval(0.1f);
		
		s.AppendCallback(() => { alarmImages[2].enabled = false; });
		s.AppendInterval(0.1f);

		s.AppendCallback(() => { alarmImages[1].enabled = false; });
		s.AppendInterval(0.1f);
		
		s.AppendCallback(() => { alarmImages[0].enabled = false; });
		s.AppendInterval(0.1f);

		s.SetLoops(-1, LoopType.Restart);
		
	}


	public void UpdateScore(int scoreValue)
	{
		int tempScore;
		int.TryParse(score.text,out tempScore);
		scoreValue += tempScore;
		score.text = scoreValue.ToString();
		
		_audioSource.PlayOneShot(addScoreClip);

        if(ScoreManager.score <= scoreValue)
        {
            ScoreManager.score = scoreValue;
        }
		
		
		
		//Depending on the score add next level of money
		foreach (var scoreLevel in scoreLevels)
		{
			if (!scoreLevel.enabled)
			{
				scoreLevel.enabled = true;
				return;
			}
		}
		
		
	}
}
