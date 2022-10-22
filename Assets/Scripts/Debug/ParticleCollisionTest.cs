using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionTest : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        print("Collision: " + other.name);
    }
}
