using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSceneController : MonoBehaviour {

    public void ButtonUIAlarmPressed(){
        GameManager.I.alarmEnabled = true;
        Debug.Log("GameManager.I.alarmEnabled");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("A"))
            GameManager.I.alarmEnabled = true;
    }
}
