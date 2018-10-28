using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stolable : MonoBehaviour{

    public int moneyValue;
    public bool neededToComplete; //strongbox
    public bool stolen = false;

    public void OnItemPickedUp()
    {
        HUDController.I.UpdateScore(moneyValue);
        stolen = true;
        if (neededToComplete)
        {
            GameManager.I.canCompleteLevel = true;
        }
        this.gameObject.SetActive(false);
    }
}
