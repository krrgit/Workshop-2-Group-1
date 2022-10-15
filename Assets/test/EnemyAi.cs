using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{
    private Vector3 startingPosition;

    private Vector3 roamPosition;
    //starting position
    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }
    
    //random roaming position
    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }
    
    //generates random direction
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}
