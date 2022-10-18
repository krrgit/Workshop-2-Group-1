using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SpiderLegIK : MonoBehaviour {
    [Header("Movement Values")]
    public float minStayDist = 0.5f;
    public float moveSpeed = 10;
    public bool allowMove = true;
    
    [Header("IK Values")]
    public Transform leg;
    public Transform calf;
    public Transform tip;
    public Transform target;
    public Transform ideal;
    public Vector2 hipTargetOffset;
    public float bendDir = -1;
    
    float length0;
    float length1;

    // IK Values
    private Vector2 targetToJoint;
    private float midLength;
    private float parallelLength;
    private Vector2 midVector;
    private Vector2 midPoint;
    
    // Move Values
    private Vector2 tipToIdealDist;
    private bool isMoving;


    public Vector2 IdealDist
    {
        get { return tipToIdealDist; }
    }

    void Start()
    {
        length0 = Vector2.Distance(leg.position, calf.position);
        length1 = Vector2.Distance(calf.position, tip.position);
    }

    private void Update()
    {
        ComputeIKValues();
        RotateSegments();
    }

    void ComputeIKValues()
    {
        // Get vector from target to first IK joint
        targetToJoint = leg.position - target.position;
        // Compute length of middle vector
        // Middle vector points to 2nd IK joint perpendicular to targetToJoint vector
        // Clamp length so it doesn't bend backwards
        midLength = length0+length1 - targetToJoint.magnitude;
        midLength = Mathf.Clamp(midLength,0.01f,length1);
        // Get length of 2nd IK leg projected onto targetToJoint.
        // Used to compute 2nd IK Joint position
        parallelLength = Mathf.Sqrt((length1*length1)-(midLength*midLength));
        // Middle vector which points to 2nd IK Joint
        midVector = Vector2.Perpendicular(leg.position-target.position).normalized*midLength*bendDir;
        // Middle IK Joint Position
        midPoint = (Vector2)target.position + (targetToJoint.normalized * parallelLength) + midVector;
    }

    void RotateSegments()
    {
        // Hip segment
        transform.up = (Vector2)(target.position - transform.position) + hipTargetOffset;
        // Mid Segment
        leg.up = midPoint - (Vector2)leg.position;
        // Calf Segment
        calf.up = (Vector2)(target.position - calf.position);
        
        Debug.DrawRay(target.position, targetToJoint);
        Debug.DrawRay(target.position + ((Vector3)targetToJoint.normalized*parallelLength), midVector);
    }

    public bool IdealDistanceCheck()
    {
        tipToIdealDist = ideal.position - target.position;
        var color = Color.red;
        if (tipToIdealDist.magnitude < minStayDist)
        {
            color = Color.green;
        }
        else
        {
            if (!isMoving)
            {
                return true;
                //StartCoroutine(MoveToIdeal());
            }
        }
        Debug.DrawRay(target.position, tipToIdealDist, color);
        return false;
    }

    public void Move()
    {
        if (isMoving) return;
        
        StartCoroutine(MoveToIdeal());
    }

    public void MoveIncrement()
    {
        target.position = Vector3.MoveTowards(target.position,ideal.position,Time.deltaTime*moveSpeed);
    }

    public bool IdealDistCheck()
    {
        return (tipToIdealDist).magnitude > 0.01f;
    }

    IEnumerator MoveToIdeal()
    {
        isMoving = true;
        Debug.Log("Moving");
        while (tipToIdealDist.magnitude > 0.01f)
        {
            target.position = Vector3.MoveTowards(target.position,ideal.position,Time.deltaTime*moveSpeed);
            yield return new WaitForEndOfFrame();   
        }
        Debug.Log("Stop");
        
        isMoving = false;
    }
    


}
