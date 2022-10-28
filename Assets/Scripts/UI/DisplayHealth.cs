using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public Sprite emptyHeart;
    // public Sprite halfHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerHealth playerHealth;

    void Update()
    {

        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            
            // else if(i = halfHealth)
            // {
            //     hearts[i].sprite = halfHeart;
            // }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            
            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
