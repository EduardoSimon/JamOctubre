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

    private void Awake()
    {
        rooms = FindObjectsOfType<Room>();
        Debug.Log("rooms has " + rooms.Length + " components. ");
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
        };
    }

    public void InitCoroutines(){
        StartCoroutine(IsLevelCompleted());
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
        //things when level completed
    }

    public void SetRooms(Room[] rooms){
        this.rooms = rooms;
    }

}
