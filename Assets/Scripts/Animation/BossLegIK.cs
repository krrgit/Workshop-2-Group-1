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
    public Transform leg;
    public Transform calf;
    public Transform tip;
    public Transform target;
    public Vector3 hipTargetOffset;
    public float bendDir = -1;
    
    float length0;
    float length1;

    // IK Values
    private Vector2 targetToJoint;
    private float midLength;
    private float parallelLength;
    private Vector2 midVector;
    private Vector2 midPoint;

    void Start()
    {
        length0 = Vector2.Distance(leg.position, calf.position);
        length1 = Vector2.Distance(calf.position, tip.position);
    }

    private void Update()
    {
        ComputeIKValues();
        // Hip segment
        transform.up = target.position - transform.position + hipTargetOffset;
        // Mid Segment
        leg.up = (Vector3)midPoint - leg.position;
        // Calf Segment
        calf.up = target.position - calf.position;
        
        Debug.DrawRay(target.position, targetToJoint);
        Debug.DrawRay(target.position + ((Vector3)targetToJoint.normalized*parallelLength), midVector);
    }

    void ComputeIKValues()
    {
        // Get vector from target to first IK joint
        targetToJoint = leg.position - target.position;
        // Compute length of middle vector
        // Middle vector points to 2nd IK joint perpendicular to targetToJoint vector
        // Clamp length so it doesn't bend backwards
        midLength = length0+length1 - targetToJoint.magnitude;
        midLength = Mathf.Clamp(midLength,0,length1);
        // Get length of 2nd IK leg projected onto targetToJoint.
        // Used to compute 2nd IK Joint position
        parallelLength = Mathf.Sqrt((length1*length1)-(midLength*midLength));
        // Middle vector which points to 2nd IK Joint
        midVector = Vector2.Perpendicular(leg.position-target.position).normalized*midLength*bendDir;
        // Middle IK Joint Position
        midPoint = (Vector2)target.position + (targetToJoint.normalized * parallelLength) + midVector;
    }
    
    
}
