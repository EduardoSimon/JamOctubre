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
            new NextStateInfo(this, STATE.GoingShelter, STATE.None, GetComponent<AlarmEnabledCondition>())
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
            GetComponent<PerformTalkingSteeringBehaviour>()
        };

        FSMSystem.I.AddState(this, new State(STATE.Talking));
        FSMSystem.I.AddBehaviours(this, actions, this.states.Find((obj) => obj.stateName == STATE.Talking));
    }

    public void SetIdleState()
    {
        //create actions.
        FSMSystem.I.AddState(this, new State(STATE.Idle));
        //add actions to this state.
    }

    public void SetGoingAlarmState(){
        //create actions.
        List<SteeringBehaviour> actions = new List<SteeringBehaviour>()
        {
            GetComponent<GoToPositionSteeringBehaviour>()
        };

        FSMSystem.I.AddState(this, new State(STATE.GoingAlarm));
        //add actions to this state.
    }

    public void SetGoingShelterState(){
        //create actions.
        FSMSystem.I.AddState(this, new State(STATE.GoingShelter));
        //add actions to this state.
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
}
