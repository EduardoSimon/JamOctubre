using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Parpadeo : MonoBehaviour
{

	public Color TweenColor;
	public Image item;
	
	// Use this for initialization
	void Start ()
	{
		item.DOColor(TweenColor, 2.0f).SetLoops(-1, LoopType.Yoyo);
	}
}
