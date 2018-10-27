using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FaceTowardsSteeringBehaviour : SteeringBehaviour {

    public float rotationDuration = 2.0f;

    public override void Act()
    {
        if (!GetComponent<NPC>().canDetectNPC){
            FindOtherPersonCondition other = GetComponent<FindOtherPersonCondition>();

            if(other != null && other.currentTarget!= null){
                    Vector3 dir = (other.currentTarget.position - transform.position).normalized;
                    dir.y = 0;
                    Quaternion newRotation = Quaternion.LookRotation(dir, Vector3.up);
                    transform.DORotateQuaternion(newRotation, rotationDuration);
                    //transform.rotation = newRotation;
                }


            }
     }
}
