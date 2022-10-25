using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffParticleAnimator : MonoBehaviour {
    [SerializeField] private float startRadius = 150;
    [SerializeField] private float endRadius = 5;
    [SerializeField] private float speed = 4;
    [SerializeField] private ParticleSystem system;
    
    private ParticleSystem.ShapeModule shape;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        shape = system.shape;

        StartCoroutine(PlayAnim());

    }

    IEnumerator PlayAnim()
    {
        shape.radius = startRadius;

        while (shape.radius > endRadius)
        {
            shape.radius -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        shape.radius = endRadius;
        yield return new WaitForSeconds(2);
        system.Stop();
    }
}
