using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationController : MonoBehaviour {

    private Animator npcAnimator;
    public float idleSpeed = 0f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public string parameterName = "Blend";

    private void Awake()
    {
        npcAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (GetComponent<NPC>().currentState.stateName){
            case STATE.Talking:
                GetComponent<NavMeshAgent>().speed = idleSpeed;
                break;
            case STATE.Idle:
                GetComponent<NavMeshAgent>().speed = idleSpeed;
                break;
            case STATE.ChangingRoom:
                GetComponent<NavMeshAgent>().speed = walkSpeed;
                break;
            case STATE.GoingAlarm:
                GetComponent<NavMeshAgent>().speed = runSpeed;
                break;
            case STATE.GoingShelter:
                GetComponent<NavMeshAgent>().speed = runSpeed;
                break;
        }

        npcAnimator.SetFloat("Blend", GetComponent<NavMeshAgent>().speed / runSpeed, 0.2f,Time.deltaTime);
    }
}
