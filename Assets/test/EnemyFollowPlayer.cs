using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed;
    private Transform player;
    private float canAttack;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackDamage = 10f;
    //lets enemy move towards player
    private void Update()
    {
        if (player != null)
        {
            float moveSpeed = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, 
                player.position, moveSpeed);
        }
    }

    //detects player if in range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.transform;
            Debug.Log(player);
        }
    }
    //detection check, if player not in range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
    //calls health function from player health script to update player health    
    private void OnCollisionStay2D(Collision2D other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }
}
