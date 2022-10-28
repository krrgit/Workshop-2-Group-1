using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource playerHit;
    [SerializeField] private AudioSource playerDash;
    [SerializeField] private AudioSource enemyHit;
    [SerializeField] private AudioSource playerWalk;
    [SerializeField] private AudioSource catButterfly;
    [SerializeField] private AudioSource catChargeAtk;
    [SerializeField] private AudioSource catStomp;
    [SerializeField] private AudioSource spiderDeath;
    [SerializeField] private AudioSource bossDeath;
    [SerializeField] private AudioSource catCharge2;
    [SerializeField] private AudioSource catChargeLoop;
    [SerializeField] private AudioSource catStaffAtk;

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

    public void PlayCatStaffAtk()
    {
        catStaffAtk.Play();
    }

    public void PlayChargeLoop(float duration)
    {
        StartCoroutine(IChargeLoop(duration));
    }

    IEnumerator IChargeLoop(float duration)
    {
        catChargeAtk.Stop();
        catChargeLoop.Play();
        catChargeLoop.loop = true;
        yield return new WaitForSeconds(duration);
        catChargeLoop.Stop();
    } 
    

    public void PlayCatCharge()
    {
        catChargeAtk.Play();
    }
    public void PlayCatCharge2()
    {
        catCharge2.Play();
    }

    public void PlayBossDeath()
    {
        bossDeath.Play();
    }

    public void PlayDash()
    {
        playerDash.Play();
    }

    public void PlayCatButterfly()
    {
        catButterfly.Play();
    }

    public void PlaySpiderDeath()
    {
        spiderDeath.Play();
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

    public void PlayCatStomp()
    {
        catStomp.Play();
    }

    public void Dash()
    {
        playerDash.Play();
    }


}
