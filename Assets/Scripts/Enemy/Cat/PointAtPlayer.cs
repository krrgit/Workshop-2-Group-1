using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] private PlayerMovement pm;

    public void Point()
    {
        transform.up = pm.transform.position - transform.position;
    }
    
}
