using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private PlayerMovement pm;
    
    void Update()
    {
        transform.up = pm.PredictedPosition(0.4f) - transform.position;
    }
}
