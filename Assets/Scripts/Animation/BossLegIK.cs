using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BossLegIK : MonoBehaviour {
    public Transform calf;
    public Transform tip;
    public Transform target;
    public float length0;
    public float length1;
    public int ankleDir = -1;

    void Start()
    {
        length0 = Vector2.Distance(transform.position, calf.position);
        length1 = Vector2.Distance(calf.position, tip.position);
    }

    private void Update()
    {
        Vector2 targetToJoint = transform.position - target.position;
        float midLength = length0+length1 - targetToJoint.magnitude;
        midLength = Mathf.Clamp(midLength,0,length1);
        float parallelLength = Mathf.Sqrt((length1*length1)-(midLength*midLength));
        Vector2 midVector = Vector2.Perpendicular(transform.position-target.position).normalized*midLength * ankleDir;
        Vector2 midPoint = (Vector2)target.position + (targetToJoint.normalized * parallelLength) + midVector;

        transform.up = (Vector3)midPoint - transform.position;
        calf.up = target.position - calf.position;
        
        Debug.DrawRay(target.position, targetToJoint);
        Debug.DrawRay(target.position + ((Vector3)targetToJoint.normalized*parallelLength), midVector);
    }
    
    
}
