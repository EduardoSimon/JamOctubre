using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOtherPersonCondition : Condition
{
    public float viewRadius = 3f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform currentTarget;

    public override bool Check()
    {

        return FindNPC();

        //return false;

    }

    public bool FindNPC(){
        //currentTarget = null;

        Collider[] targetsInside = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInside.Length; i++)
        {
            Transform target = targetsInside[i].transform;

            if (this.GetComponent<NPC>().canDetectNPC && target.GetComponent<NPC>() != null &&
                target.GetComponent<NPC>() != this.GetComponent<NPC>()){
                Debug.Log(this.name + " ha encontrado a " + target.name);
                GetComponent<NPC>().canDetectNPC = false;
                currentTarget = target;
                return true;
            }
        }
        currentTarget = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
