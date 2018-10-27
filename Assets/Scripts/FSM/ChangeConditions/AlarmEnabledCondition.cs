using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlarmEnabledCondition : Condition
{
    public override bool Check()
    {
        return GameManager.I.alarmEnabled;
    }
}
