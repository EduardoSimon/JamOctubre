using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Stolable))]
public class StolableUI : MonoBehaviour {

	public CanvasGroup canvas;
	public float InteractionTimeNeeded = 2f;
	[Range(0.15f,0.30f)]
	public float fillSpeed = 0.2f;
	public float emptyingDuration = 2.0f;
	public AudioClip InteractingAudioClip;

	private AudioSource _audioSource;
	private Sequence s;
	private Sequence s1;


	private float heldTime = 0.0f;
	public Image image;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
		image = canvas.GetComponentInChildren<Image>();
		_audioSource.clip = InteractingAudioClip;
		s = DOTween.Sequence();
		s1 = DOTween.Sequence();
	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerController controller = other.GetComponent<PlayerController>();

		if (controller != null)
		{
			canvas.gameObject.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		PlayerController controller = other.GetComponent<PlayerController>();

		if (controller != null)
		{
			canvas.gameObject.SetActive(false);
		}
		
		_audioSource.Stop();
	}

	public void OnInteractKeyPressed(KeyCode keyCode)
	{
		StartCoroutine(CheckHeldTime(keyCode));
		_audioSource.Play();
		s1.Append(_audioSource.DOPitch(3.0f, 4.0f));
		s1.OnComplete(() => { _audioSource.Stop(); });
	}

	private IEnumerator CheckHeldTime(KeyCode keyCode)
	{
		image.fillAmount = 0.0f;


		while (heldTime < InteractionTimeNeeded && Input.GetKey(keyCode))
		{
			heldTime += Time.deltaTime;
			image.fillAmount += Time.deltaTime * fillSpeed;

			yield return null;
		}

		if (heldTime >= InteractionTimeNeeded)
			GetComponent<Stolable>().OnItemPickedUp();

		heldTime = 0.0f;
		
		_audioSource.Play();
		s.Append(image.DOFillAmount(0.0f, emptyingDuration));
		s.Append(_audioSource.DOPitch(0.0f, emptyingDuration));
		s.OnComplete(() => { _audioSource.Stop(); });
	}
}
