using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopNavMeshSteeringBehaviour : SteeringBehaviour
{
    public override void Act()
    {
        if (!GetComponent<NavMeshAgent>().isStopped){ //navmesh is enabled
            GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
}
