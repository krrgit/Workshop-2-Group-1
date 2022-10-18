/*
 * This class is for objects in a scene which physically react to collisions with bullets
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PhysicsInteractable : MonoBehaviour {
    public float velocity = 2;
    public float halfHeight = 1.25f;
    public int hitsToFly = 1; //Amount of hits left for this object to fly
    public Rigidbody2D rb;
    public HitsunAnimation hsAnim;

    private Vector2 direction;
    private Vector2 startPos;

    void Start()
    {
    }
    
    private void OnParticleCollision(GameObject other)
    {
        --hitsToFly;
        StartCoroutine(HitStun(other));
    }

    IEnumerator HitStun(GameObject other)
    {
        startPos = transform.position;
        hsAnim.SetToWhite();

        if (hsAnim.wiggle)
        {
            float timer = hsAnim.Duration;
            while (timer > 0)
            {
                hsAnim.Wiggle(startPos);
                yield return new WaitForEndOfFrame();
                timer -= Time.deltaTime;
            }

            transform.position = startPos;
        } else
        {
            yield return new WaitForSeconds(hsAnim.Duration);
        }

        hsAnim.RevertSprite();
        
        if (hitsToFly <= 0)
        {
            ComputeDirection(other.transform.position);
            MoveRigidbody();
            FlipSprite();
        
            StartCoroutine(AirTime());
        }
    }
    

    void ComputeDirection(Vector3 position)
    {
        direction = transform.position - position;
        direction = direction.normalized;
    }

    void MoveRigidbody()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = direction * velocity;
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y),
            transform.localScale.z);
        transform.localScale *= 1.05f;
    }
    
    // This coroutine disables collision, makes object "fly" then re-enables collision after landing
    IEnumerator AirTime()
    {
        Destroy(GetComponent<Collider2D>());
        yield return new WaitForSeconds(halfHeight*(velocity/2f)/(9.8f));
        transform.localScale /= 1.05f;
        
        while (rb.velocity.magnitude > 2*Time.deltaTime)
        {
            rb.velocity -= rb.velocity.normalized * Time.deltaTime * velocity;
            yield return new WaitForEndOfFrame();
        }
        
        rb.velocity = Vector2.zero;
        gameObject.AddComponent<BoxCollider2D>();
        rb.drag = 2;
        Destroy(this);
    }

}
