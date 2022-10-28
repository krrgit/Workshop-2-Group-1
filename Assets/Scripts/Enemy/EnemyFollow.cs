using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    [SerializeField] private Transform target; //target is player
    public float stoppingDistance;

    [SerializeField] private SpiderAIController attack;

    //follows player
    private void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            }
        }
    }
    //player is in range, enemy follows
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            
            attack.RunAI = true;
            SoundManager.Instance.PlaySpiderWalk();
            
        }
    }

    //player is out of enemy range, doesnt follow
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
            

        }
    }
}
