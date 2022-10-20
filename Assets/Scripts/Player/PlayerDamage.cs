using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        PlayerHealth.Instance.UpdateHealth(-1);
    }
}
