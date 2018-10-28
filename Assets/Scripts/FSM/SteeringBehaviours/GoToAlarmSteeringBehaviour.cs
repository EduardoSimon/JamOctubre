using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToAlarmSteeringBehaviour : SteeringBehaviour {
    public Transform alarm;
    private NavMeshAgent navMesh;
    private NPC npc;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        npc = GetComponent<NPC>();
    }

    public override void Act()
    {
        if (!npc.goingToAlarm){
            navMesh.SetDestination(alarm.position);
            npc.goingToAlarm = true;
        }

        if (Vector3.Distance(this.transform.position, alarm.position) <= navMesh.radius * 4f){
            GameManager.I.alarmEnabled = true;
        }
    }
}
