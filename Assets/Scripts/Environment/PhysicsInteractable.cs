/*
 * This class is for objects in a scene which physically react to collisions with bullets
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsInteractable : MonoBehaviour {
    public float velocity = 2;
    public float halfHeight = 1.25f;
    private Rigidbody2D rb;

    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnParticleCollision(GameObject other)
    {
        
        Debug.Log("collision");
        
        direction = transform.position - other.transform.position;
        direction = direction.normalized;

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = direction * velocity;

        FlipSprite();
        Destroy(GetComponent<Collider2D>());

        StartCoroutine(AirTime());
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y),
            transform.localScale.z);
        transform.localScale *= 1.05f;
    }

    IEnumerator AirTime()
    {
        yield return new WaitForSeconds(halfHeight*(velocity/2f)/(9.8f));
        transform.localScale /= 1.05f;
        
        while (rb.velocity.magnitude > 2*Time.deltaTime)
        {
            rb.velocity -= rb.velocity.normalized * Time.deltaTime*velocity;
            yield return new WaitForEndOfFrame();
        }
        
        rb.velocity = Vector2.zero;
    }

}
