using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PrintAwake : MonoBehaviour {

    private void Awake()
    {
        DynamicGI.UpdateEnvironment();
    }
}
