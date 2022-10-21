using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public EnemyHealthBar healthBar;
    
    
    //starts current health to max health
    void Start() 
    {
        currentHealth = maxHealth;
        healthBar.SetEnemyMaxHealth(maxHealth);
    }

    //how much damage they take(see player script)
    void Update()
    {
        DebugDealDamage();
    }

   //attacks deplete health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetEnemyHealth(currentHealth);
        
        SoundManager.Instance.PlayEnemyHit();
    }

    void DebugDealDamage()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }
}
