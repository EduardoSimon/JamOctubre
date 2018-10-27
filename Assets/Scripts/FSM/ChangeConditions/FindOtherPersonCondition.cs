using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindOtherPersonCondition : Condition
{
    public float viewRadius = 3f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public override bool Check()
    {
        if (GetComponent<NPC>().canDetectNPC){
            return FindNPC();
        }
        return false;

    }

    public bool FindNPC(){


        Collider[] targetsInside = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInside.Length; i++)
        {
            Transform target = targetsInside[i].transform;

            if (target.GetComponent<NPC>() != null && target.GetComponent<NPC>() != this.GetComponent<NPC>()){
                Debug.Log(this.name + " ha encontrado a " + target.name);
                GetComponent<NPC>().canDetectNPC = false;
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
