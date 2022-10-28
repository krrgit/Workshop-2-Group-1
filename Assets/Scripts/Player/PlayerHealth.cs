using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 0f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private PlayerMovement playerMovement;

    public static PlayerHealth Instance;


    void Awake()
    {
        // This only allows one instance of PlayerHealth to exist in any scene
        // This is to avoid the need for GetComponent Calls. Use PlayerHealth.Instance instead.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        health = maxHealth; //allows to change health easily
        
    }

    public void UpdateHealth(float change)
    {
        health += change;
        
        if (health > maxHealth)
        {
            health = maxHealth;
        } else if (health <= 0)
        {
            health = 0f;
            GameStateManager.Instance.PlayerDeath();
        }
        print("Health: " + health);
    }

    public void DealDamage(float change)
    {
        UpdateHealth(change);
        
        SoundManager.Instance.PlayPlayerHit();

        playerMovement.startInvincibility();
    }

}
