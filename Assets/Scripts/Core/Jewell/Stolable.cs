using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stolable : MonoBehaviour{

    public int moneyValue;
    public bool enableAlarm;
    public bool neededToComplete; //strongbox

    public CanvasGroup canvas;

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
    }

    public void OnItemPickedUp()
    {
        this.gameObject.SetActive(false);
    }
}
