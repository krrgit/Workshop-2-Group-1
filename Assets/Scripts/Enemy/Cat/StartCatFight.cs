using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCatFight : MonoBehaviour {
    [SerializeField] private CatAI ai;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject fogWall;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ai.runAI = true;
            healthBar.gameObject.SetActive(true);
            fogWall.SetActive(true);
            Destroy(gameObject);
        }

    }
}
