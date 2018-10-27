using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stolable : MonoBehaviour{

    public int moneyValue;
    public bool enableAlarm;
    public bool neededToComplete; //strongbox

    public void OnItemPickedUp()
    {
        this.gameObject.SetActive(false);
    }
}
