using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCondition : Condition {

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public override bool Check()
    {
        return FindPlayer();
    }

    bool FindPlayer()
    {
        Quaternion spreadAngle = Quaternion.AngleAxis(-viewAngle / 2, new Vector3(0, 1, 0));
        Quaternion spreadAngle2 = Quaternion.AngleAxis(viewAngle / 2, new Vector3(0, 1, 0));
        
        Vector3 newVector = spreadAngle * transform.forward;
        Vector3 newVector2 = spreadAngle2 * transform.forward;

        Debug.DrawRay(transform.position, newVector * viewRadius, Color.red);
        Debug.DrawRay(transform.position, newVector2 * viewRadius, Color.red);

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {

            Transform target = targetsInViewRadius[i].transform;
            
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            dirToTarget.y = 0;

            Debug.DrawRay(transform.position, dirToTarget * viewRadius, Color.green);

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    GetComponent<AudioSource>().Play();
                    return true;
                }
            }
        }
        return false;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
    */



}
