using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public abstract class NPCStatesBehaviour : MonoBehaviour {

    public List<Transition> transitions;
    public List<State> states;
    public abstract void SetTransitions();
    public abstract void SetStates();

    public State currentState;
    public List<Transition> currentTransitions;

    public Room currentRoom;
    public Room nextRoom;

    public List<RoomProbability> roomProbabilities;
    public bool roomSelected;
    public bool destinationFixed;
    public bool canDetectNPC = true;
    public float timeToFindAgain = 5f;
    public bool goingToAlarm = false;
    public bool talkingWithSomeone = false;

    public void ActBehaviours()
    {
        if (currentState.behaviours != null)
        {
            foreach (SteeringBehaviour behaviour in currentState.behaviours)
            {
                behaviour.Act();
            }
        }
    }

    public void CheckConditions()
    {
        foreach (Transition trans in currentTransitions)
        {
            foreach (NextStateInfo nsi in trans.nextStateInfo)
            {
                bool result = nsi.changeCondition.Check();

                if (result == true)
                {  
                    if (nsi.stateCaseTrue.stateName == STATE.None)
                    {
                        continue;
                    }
                    currentState = nsi.stateCaseTrue;
                }
                else
                {
                    if (nsi.stateCaseFalse.stateName == STATE.None)
                    {
                        continue;
                    }         
                    currentState = nsi.stateCaseFalse;
                   
                }
                currentTransitions = transitions.FindAll(x => x.currentState == currentState);
                return;
            }
        }
    }
}

[System.Serializable]
public class RoomProbability{
    public ROOM roomName;
    public float probability;
    public float cumulativeProbability;
}

