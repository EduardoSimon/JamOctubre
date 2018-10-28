using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ROOM
{
    BathRoom = 0,
    KitchenRoom = 1,
    DinningRoom = 2,
    BedRoom = 3,
    LivingRoom = 4,
    ShelterRoom = 5,
    StrongboxRoom = 6
}

public class GameManager : Singleton<GameManager> {

    public Room[] rooms;
    public bool playerDetected;
    public bool alarmEnabled;
    public bool levelCompleted;
    public bool canCompleteLevel;

    private void Awake()
    {
        rooms = FindObjectsOfType<Room>();
    }

    public void Start()
    {
        InitCoroutines();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.buildIndex == (int) SceneLoader.SCENES.Game)
                rooms = FindObjectsOfType<Room>();

            if(scene.buildIndex == (int)SceneLoader.SCENES.MenuBueno)
            {
                ResetGameManager();
            }

        };

    }

    private void ResetGameManager()
    {
        playerDetected = false;
        alarmEnabled = false;
        canCompleteLevel = false;
        levelCompleted = false;
        HUDController.I.ResetHUD();
    }

    IEnumerator TimeElapsed()
    {
        while (!alarmEnabled)
        {
            yield return null;
        }
        yield return new WaitForSeconds(10f);
        PlayerPrefsCustom.HasLevelEnd = true;
        SceneManager.LoadScene((int)SceneLoader.SCENES.Score);
    }

    public void InitCoroutines(){
        StartCoroutine(IsLevelCompleted());
        StartCoroutine(TimeElapsed());

    }

    IEnumerator IsPlayerDetected(){
        while (!playerDetected){
            yield return null;
        }
        //things when player detected
    }

    IEnumerator IsLevelCompleted(){
        while (!levelCompleted){
            yield return null;
        }
        PlayerPrefsCustom.HasLevelEnd = true;
        SceneManager.LoadScene((int)SceneLoader.SCENES.Score);
        //things when level completed
        
    }

    public void SetRooms(Room[] rooms){
        this.rooms = rooms;
    }


}
