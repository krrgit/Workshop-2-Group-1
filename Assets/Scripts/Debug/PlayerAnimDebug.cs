using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimDebug : MonoBehaviour {
    [SerializeField] private Animator anim;

    private Vector2 prevPos;
    private void Update()
    {
        
        anim.SetFloat("speed", ((Vector2)transform.position-prevPos).magnitude);

        prevPos = transform.position;
    }
}
