using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelTrigger : MonoBehaviour
{

	public bool debugCompleted = false;
	private bool playerInside;
	public Canvas canvas;
	
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerController>())
		{
			playerInside = true;
		}	
	}

	void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<PlayerController>())
		{
			playerInside = false;
		}	
	}

	void Update()
	{
		#if UNITY_EDITOR
		if (debugCompleted)
		{
			GameManager.I.canCompleteLevel = true;
		}
		#endif
		
		if (GameManager.I.canCompleteLevel)
		{
			canvas.gameObject.SetActive(true);
		}
		
		if (Input.GetKeyDown(KeyCode.E) && playerInside)
		{
			if (GameManager.I.canCompleteLevel)
			{
				GameManager.I.levelCompleted = true;
			}
		}
		
	}
}
