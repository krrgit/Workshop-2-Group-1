using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource playerHit;
    [SerializeField] private AudioSource enemyHit;
    [SerializeField] private AudioSource playerWalk;
    [SerializeField] private AudioSource bossShoot;
    [SerializeField] private AudioSource SpiderShoot;
    
    public static SoundManager Instance;

    void Awake()
    {
        // This only allows one instance of GameStateManager to exist in any scene
        // This is to avoid the need for GetComponent Calls. Use GameStateManager.Instance instead.
        if (Instance == null) {
            Instance = this;
        }else {
            Destroy(this);
        }
    }

    public void PlayShoot()
    {
        shoot.Play();
    }

    public void PlayPlayerHit()
    {
        playerHit.Play();
    }

    public void PlayEnemyHit()
    {
        enemyHit.Play();
    }

    public void playPlayerWwalk()
    {
        playerWalk.Play();
    }

    public void PlayBossShoot()
    {
        bossShoot.Play();
    }

    public void PlaySpiderShoot()
    {
        SpiderShoot.Play();
    }
    
}
