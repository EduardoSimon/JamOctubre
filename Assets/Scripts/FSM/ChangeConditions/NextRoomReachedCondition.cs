using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextRoomReachedCondition : Condition
{
    public override bool Check()
    {
        Room room = GetComponent< GoToNextRoomSteeringBehaviour > ().nextRoom;

        if ( room != null){
            float distance = Vector3.Distance(transform.position, room.transform.position);
            if (distance < GetComponent<NavMeshAgent>().radius * 4f){
                GetComponent<NPC>().roomSelected = false;
                GetComponent<NPC>().destinationFixed = false;
                GetComponent<NPC>().currentRoom = room;
                GetComponent<GoToNextRoomSteeringBehaviour>().nextRoom = null;
                return true;
            }
        }
        return false;
    }
}
