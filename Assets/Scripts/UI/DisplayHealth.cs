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

    public void Awake()
    {
        hearts = new Image[playerHealth.maxHealth];
        for (int i = 0; i < playerHealth.maxHealth; ++i)
        {
            var go = new GameObject();
            go.transform.parent = transform;
            hearts[i] = go.AddComponent<Image>();
            hearts[i].rectTransform.localPosition = new Vector3(100 * i, 0, 0);
            hearts[i].rectTransform.localScale = Vector3.one;
            hearts[i].sprite = fullHeart;
        }
    }

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
