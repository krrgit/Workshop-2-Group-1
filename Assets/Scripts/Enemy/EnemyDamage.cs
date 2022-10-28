using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    [SerializeField] private HitstunAnimation hsAnim;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private Collider2D coll;
    
    private void OnParticleCollision(GameObject other)
    {
        hsAnim.PlayAnimation(coll);
        health.TakeDamage(1);
        SoundManager.Instance.PlayEnemyHit();
    }
}
