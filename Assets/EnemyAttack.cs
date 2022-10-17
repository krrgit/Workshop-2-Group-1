using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public BulletHellSpawner bulletHellSpawner;

    void Awake()
    {
        bulletHellSpawner.Summon();
        bulletHellSpawner.UpdateColumns();
        
    }
    
    
}

    
