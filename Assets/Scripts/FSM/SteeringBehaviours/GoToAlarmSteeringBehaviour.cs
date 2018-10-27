using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToAlarmSteeringBehaviour : SteeringBehaviour {
    public Transform alarm;
    private NavMeshAgent navMesh;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    public override void Act()
    {
        if (navMesh.destination != alarm.position){
            navMesh.SetDestination(alarm.position);
        }
        if (Vector3.Distance(this.transform.position, alarm.position) <= navMesh.radius * 4f){
            GameManager.I.alarmEnabled = true;
        }

    }
}
