using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiderAIController : MonoBehaviour
{
    [SerializeField] private SpiderAttackController attack;
    [SerializeField] private SpiderDeathAnimator deathAnim;
    [SerializeField] private EnemyHealth health;
    
    public bool PatternActive = false;
    
    public int randomNum = 0;
    
    public bool RunAI = false;
    
    private float timer = 0;
    
    
    void Update()
    {
        if (!RunAI) return;
        randomNum = Random.Range(1, 5);
        Debug.Log(randomNum);
        RandomAttacksGenerator(PatternActive);
        timer -= Time.deltaTime;
    }

    private void OnEnable()
    {
        health.enemyDeathDel += Death;
    }

    private void OnDisable()
    {
        health.enemyDeathDel -= Death;
    }

    public void Death()
    {
        RunAI = false;
        attack.StopAllCoroutines();
        attack.StopAll();
        deathAnim.DisableAttacks();
        deathAnim.StartAnim();
        

    }

    public void RandomAttacksGenerator(bool PatternActive)
    {
        if (timer > 0) return;
        for (int i = 0; i < randomNum; i++)
        {
            if (randomNum == 1 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackOne(3);
                timer = 3;
                PatternActive = false;
            }

            else if (randomNum == 2 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackTwo(3);
                timer = 3;
                PatternActive = false;

            }

            else if (randomNum == 3 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackThree(3);
                timer = 3;
                PatternActive = false;
            }
            
            
            else if (randomNum == 4 && PatternActive == false)
            {
                PatternActive = true;
                attack.DoAttackFour(3);
                timer = 3;
                PatternActive = false;

            }
        }
    }
}





//I CANT TAKE IT ANYMORE EATING A BURGER WITH NO HONEY MUSTARD