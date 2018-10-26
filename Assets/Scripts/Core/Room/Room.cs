using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ROOM{
    BathRoom = 0,
    KitchenRoom = 1,
    DinningRoom = 2,
    BedRoom = 3,
    LivingRoom = 4,
    ShelterRoom = 5,
    StrongboxRoom = 6
}

public class Room : MonoBehaviour{
    public int capacity;
    public bool full;
    public ROOM roomName;
    public List<Jewell> jewells;
}

