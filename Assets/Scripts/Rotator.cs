using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Rotator : MonoBehaviour
{

    [Header("The smaller the value the faster it will rotate")]
    [Tooltip("The smaller the value the faster it will rotate")]
    public float rotationTime;
    public float oscilationSpeed;
    public float moveYOffset;
    
    private void Start()
    {
        this.transform.DORotate(new Vector3(0, 1f, 0), rotationTime, RotateMode.Fast).SetLoops(-1,LoopType.Incremental);
        this.transform.DOMoveY(transform.position.y + moveYOffset, oscilationSpeed, false).SetLoops(-1, LoopType.Yoyo);
    }
}
