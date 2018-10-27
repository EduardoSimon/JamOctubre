using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectPlayerCondition))]
[RequireComponent(typeof(TimeElapsedCondition))]
[RequireComponent(typeof(FindOtherPersonCondition))]
[RequireComponent(typeof(AlarmEnabledCondition))]
[RequireComponent(typeof(GoToNextRoomSteeringBehaviour))]
[RequireComponent(typeof(FaceTowardsSteeringBehaviour))]
[RequireComponent(typeof(PerformTalkingSteeringBehaviour))]
[RequireComponent(typeof(StopNavMeshSteeringBehaviour))]
public class NPC : NPCStatesBehaviour
{
    private void Start()
    {
        SetStates();
        SetTransitions();

        currentState = states.Find((x) => x.stateName == STATE.ChangingRoom);
        currentTransitions = transitions.FindAll((x) => x.currentState == currentState);
    }

    public override void SetStates()
    {
        SetChangingRoomState();
        SetTalkingState();
        SetIdleState();
        SetGoingAlarmState();
        SetGoingShelterState();
        SetNoneState();
    }

    public override void SetTransitions()
    {
        List<NextStateInfo> _nextStateInfo = new List<NextStateInfo>(){
            new NextStateInfo(this, STATE.GoingAlarm, STATE.None, GetComponent<DetectPlayerCondition>()),
            new NextStateInfo(this, STATE.Idle, STATE.None, GetComponent<NextRoomReachedCondition>()),
            new NextStateInfo(this, STATE.Talking, STATE.None, GetComponent<FindOtherPersonCondition>()),
            new NextStateInfo(this, STATE.GoingShelter, STATE.None, GetComponent<AlarmEnabledCondition>())
        };
        FSMSystem.I.AddTransition(this, STATE.ChangingRoom, _nextStateInfo);

        List<NextStateInfo> _nextStateInfo2 = new List<NextStateInfo>(){
            new NextStateInfo(this, STATE.GoingAlarm, STATE.None, GetComponent<DetectPlayerCondition>()),
            new NextStateInfo(this, STATE.ChangingRoom, STATE.None, GetComponent<TimeElapsedCondition>()),
            new NextStateInfo(this, STATE.GoingShelter, STATE.None, GetComponent<AlarmEnabledCondition>())
        };
        FSMSystem.I.AddTransition(this, STATE.Talking, _nextStateInfo2);

        List<NextStateInfo> _nextStateInfo3 = new List<NextStateInfo>()
        {
            new NextStateInfo(this, STATE.ChangingRoom, STATE.None, GetComponent<TimeElapsedCondition>()),
            new NextStateInfo(this, STATE.GoingAlarm, STATE.None, GetComponent<DetectPlayerCondition>()),
            new NextStateInfo(this, STATE.GoingShelter, STATE.None, GetComponent<AlarmEnabledCondition>()),
            new NextStateInfo(this, STATE.Talking, STATE.None, GetComponent<FindOtherPersonCondition>())
        };
        FSMSystem.I.AddTransition(this, STATE.Idle, _nextStateInfo3);

        List<NextStateInfo> _nextStateInfo4 = new List<NextStateInfo>()
        {
            new NextStateInfo(this, STATE.GoingShelter, STATE.None, GetComponent<AlarmEnabledCondition>())
        };
        FSMSystem.I.AddTransition(this, STATE.GoingAlarm, _nextStateInfo4);
    }

    public void SetChangingRoomState()
    {
        //create actions.
        List<SteeringBehaviour> actions = new List<SteeringBehaviour>()
        {
            GetComponent<GoToNextRoomSteeringBehaviour>()
        };

        FSMSystem.I.AddState(this, new State(STATE.ChangingRoom));
        FSMSystem.I.AddBehaviours(this, actions, this.states.Find((obj) => obj.stateName == STATE.ChangingRoom));
    }

    public void SetTalkingState()
    {
        List<SteeringBehaviour> actions = new List<SteeringBehaviour>()
        {
            GetComponent<FaceTowardsSteeringBehaviour>(),
            GetComponent<PerformTalkingSteeringBehaviour>(),
            GetComponent<StopNavMeshSteeringBehaviour>()
        };

        FSMSystem.I.AddState(this, new State(STATE.Talking));
        FSMSystem.I.AddBehaviours(this, actions, this.states.Find((obj) => obj.stateName == STATE.Talking));
    }

    public void SetIdleState()
    {
        List<SteeringBehaviour> actions = new List<SteeringBehaviour>(){
            GetComponent<StopNavMeshSteeringBehaviour>()
        };

        FSMSystem.I.AddState(this, new State(STATE.Idle));
        FSMSystem.I.AddBehaviours(this, actions, this.states.Find((obj) => obj.stateName == STATE.Idle));
    }

    public void SetGoingAlarmState(){
        //create actions.
        List<SteeringBehaviour> actions = new List<SteeringBehaviour>()
        {
            GetComponent<GoToAlarmSteeringBehaviour>()
        };

        FSMSystem.I.AddState(this, new State(STATE.GoingAlarm));
        FSMSystem.I.AddBehaviours(this, actions, this.states.Find((obj) => obj.stateName == STATE.GoingAlarm));
    }

    public void SetGoingShelterState(){
        List<SteeringBehaviour> actions = new List<SteeringBehaviour>(){
            GetComponent<GoToNextRoomSteeringBehaviour>()
        };
        FSMSystem.I.AddState(this, new State(STATE.GoingShelter));
        FSMSystem.I.AddBehaviours(this, actions, this.states.Find((obj) => obj.stateName == STATE.GoingShelter));
    }

    public void SetNoneState(){
        FSMSystem.I.AddState(this, new State(STATE.None));
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == (int)SceneLoader.SCENES.Level1){
            roomProbabilities = new List<RoomProbability>(GameManager.I.rooms.Length);
        }
    }

    private void Update()
    {
        ActBehaviours();
        CheckConditions();
    }

    public IEnumerator EnableFindPersonAgain(float seconds){
        yield return new WaitForSeconds(seconds);
        canDetectNPC = true;
        Debug.Log(this.name + "can find NPC again!");
    }
}
