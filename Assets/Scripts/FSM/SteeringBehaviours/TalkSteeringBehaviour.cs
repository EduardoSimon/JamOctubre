using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkSteeringBehaviour : SteeringBehaviour
{
    public override void Act()
    {
        Debug.Log("I'm talking with a person!");
    }
}
