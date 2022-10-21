using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;

    //sets enemy health to health bar
    public void SetEnemyMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    //adjust health value using slider
    public void SetEnemyHealth(int health)
    {
        slider.value = health;
    }
    
}
