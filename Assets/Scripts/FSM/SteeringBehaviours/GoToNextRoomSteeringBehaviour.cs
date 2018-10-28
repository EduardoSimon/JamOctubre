using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GoToNextRoomSteeringBehaviour : SteeringBehaviour
{
    private NPC npc;
    private NavMeshAgent navMesh;

    private void Awake()
    {
        npc = GetComponent<NPC>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override void Act()
    {
        if (GameManager.I.alarmEnabled){
            navMesh.isStopped = false;
            Debug.Log("go to alarm room!!");
            foreach (Room r in GameManager.I.rooms){
                if (r.roomName == ROOM.ShelterRoom){
                    if (navMesh.destination != r.transform.position){
                        navMesh.SetDestination(r.transform.position);
                    }
                }
            }
            return;
        }
        
        if (!npc.roomSelected){
        
            GetNewRoom();

            if (npc.currentRoom != null){
                while (npc.currentRoom == npc.nextRoom){
                    GetNewRoom();
                }
            }
            npc.roomSelected = true;

        }
        else{
            navMesh.isStopped = false;
            if (!npc.destinationFixed){
                navMesh.SetDestination(npc.nextRoom.transform.position);
                npc.destinationFixed = true;
            }

        }

    }

    public void GetNewRoom(){

        float result = Random.value;
        foreach (var prob in npc.roomProbabilities)
        {
            if (result >= prob.cumulativeProbability && result < prob.cumulativeProbability + prob.probability)
            {
                foreach (var room in GameManager.I.rooms)
                {
                    if (room.roomName == prob.roomName)
                    {
                        npc.nextRoom = room;
                        return;
                    }
                }

            }
        }
    }

}
