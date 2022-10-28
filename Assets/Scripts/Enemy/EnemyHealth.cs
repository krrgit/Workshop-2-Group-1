using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public EnemyHealthBar healthBar;

    private bool calledDeathDel;
    
    public delegate void EnemyDeath();
    public EnemyDeath enemyDeathDel;


    //starts current health to max health
    void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetEnemyMaxHealth(maxHealth);
    }


   //attacks deplete health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthBar.SetEnemyHealth(currentHealth);
        SoundManager.Instance.PlayEnemyHit();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (!calledDeathDel)
            {
                enemyDeathDel();
                calledDeathDel = true;
                SoundManager.Instance.PlayBossDeath();
            }
        }
    }
}
