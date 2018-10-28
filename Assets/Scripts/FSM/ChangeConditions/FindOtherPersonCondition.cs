using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindOtherPersonCondition : Condition
{
    public float viewRadius = 3f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform currentTarget;

    public override bool Check()
    {
        if (!GetComponent<NPC>().canDetectNPC || currentTarget != null){
            return false;
        }
        return FindNPC();
    }

    public bool FindNPC(){
        //currentTarget = null;

        Collider[] targetsInside = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInside.Length; i++)
        {
            Transform target = targetsInside[i].transform;

            if (target.GetComponent<NPC>() != null 
                && target.GetComponent<NPC>().gameObject != this.GetComponent<NPC>().gameObject
                && target.GetComponent<FindOtherPersonCondition>() != null 
                && (target.GetComponent<FindOtherPersonCondition>().currentTarget == null 
                    ||  target.GetComponent<FindOtherPersonCondition>().currentTarget == this.transform) 
                && !this.GetComponent<NPC>().talkingWithSomeone){

                this.GetComponent<NPC>().canDetectNPC = false;
                this.GetComponent<NPC>().talkingWithSomeone = true;
                currentTarget = target.transform;

                //StartCoroutine(GetComponent<NPC>().EnableFindPersonAgain(GetComponent<NPC>().timeToFindAgain));
                GetComponent<NPCSoundController>().PlaySoundTalking();
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
