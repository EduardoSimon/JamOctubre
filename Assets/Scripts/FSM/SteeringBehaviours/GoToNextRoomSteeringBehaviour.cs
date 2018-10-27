using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GoToNextRoomSteeringBehaviour : SteeringBehaviour
{
    private NPC npc;
    public Room currentRoom;
    private NavMeshAgent navMesh;


    private void Awake()
    {
        npc = GetComponent<NPC>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override void Act()
    {
        if (!npc.roomSelected){
            float result = Random.value;
            print(result);
            //returns a value between 0 and 1
            foreach (var prob in npc.roomProbabilities)
            {
                if (result >= prob.cumulativeProbability && result < prob.cumulativeProbability + prob.probability){
                    print(prob.roomName);
                    foreach (var room in GameManager.I.rooms)
                    {
                        if (room.roomName == prob.roomName){
                            currentRoom = room;
                            npc.roomSelected = true;
                            Debug.Log("room selected!");
                            //TODO: cuando lleguemos a esta habitación se debe poner roomSelected a FALSE!!
                            return;
                        }
                    }

                }
            }
        }
        else{
            Debug.Log("room already selected!");
            if (!npc.destinationFixed){
                navMesh.SetDestination(currentRoom.transform.position);
                npc.destinationFixed = true;
            }

        }

    }

}
