using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatAttackController : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private CatAnimEvents animEvents;
    [SerializeField] private BulletHellSpawner spawner;
    

    private int attackPattern;

    private Vector3 localPos;
    
    private void OnEnable()
    {
        animEvents.landPawDel += AttackPattern1;
    }

    private void OnDisable()
    {
        animEvents.landPawDel -= AttackPattern1;
    }

    void Start()
    {
      spawner.Initialize();   
    }

    void AttackPattern1(Vector2 position)
    {
        spawner.EmitOnce();
        spawner.transform.localPosition = position;
        print("Emit");
    }


}
