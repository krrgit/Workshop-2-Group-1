using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpiderFight : MonoBehaviour
{
    [SerializeField] private SpiderAIController ai;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject fogWall;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            ai.RunAI = true;
            healthBar.gameObject.SetActive(true);
            fogWall.SetActive(true);
            Destroy(gameObject);
        }
    }
}
