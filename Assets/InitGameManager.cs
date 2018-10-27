using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameManager : MonoBehaviour {

    private void Awake()
    {
        print(GameManager.I.name);
    }

}
