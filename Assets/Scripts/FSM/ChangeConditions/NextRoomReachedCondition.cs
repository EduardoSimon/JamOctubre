using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NextRoomReachedCondition : Condition
{
    public override bool Check()
    {
        Debug.Log("checking if we have arrived!");
        Room room = GetComponent< GoToNextRoomSteeringBehaviour > ().currentRoom;

        if ( room != null){
            if(Vector3.Distance(transform.position,room.transform.position) < GetComponent<NavMeshAgent>().radius * 2 ){
                return true;
            }
        }
        return false;
    }
}
