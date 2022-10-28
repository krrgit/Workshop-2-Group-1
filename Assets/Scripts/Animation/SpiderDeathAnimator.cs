using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDeathAnimator : MonoBehaviour {

    [SerializeField] private float fallSpeed = 2;
    //[SerializeField] private BulletHellSpawner spawner;
    [Header("Controlled Components")]
    [SerializeField] private GameObject system;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private Transform body;
    [SerializeField] private Transform shadow;
    [SerializeField] private EnemyHealthBar healthBar;
    [Header("Secondary Animation Components")]
    [SerializeField] private EnemyFollow follow;
    [SerializeField] private SpiderLegManager legManager;
    [SerializeField] private SpiderIdleAnimator idleAnim;
    [SerializeField] private BulletHellSpawner AttackOne;
    [SerializeField] private BulletHellSpawner AttackTwo;
    [SerializeField] private BulletHellSpawner AttackThree;
    [SerializeField] private BulletHellSpawner AttackFour;
    
    private Vector2 shadowPos;
    private Vector2 fallDir;

    public bool startedAnim = false;

    /*private void OnEnable()
    {
        health.enemyDeathDel += StartAnim;
    }

    private void OnDisable()
    {
        health.enemyDeathDel -= StartAnim;
    }*/

    public void DisableAttacks()
    {
        AttackOne.ToggleEmit(false);
        AttackOne.DestroyAllParticles();
        AttackOne.gameObject.SetActive(false);
        
        AttackTwo.ToggleEmit(false);
        AttackTwo.DestroyAllParticles();
        AttackTwo.gameObject.SetActive(false);
        
        AttackThree.ToggleEmit(false);
        AttackThree.DestroyAllParticles();
        AttackThree.gameObject.SetActive(false);
       
        AttackFour.ToggleEmit(false);
        AttackFour.DestroyAllParticles();
        AttackFour.gameObject.SetActive(false);
                    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnim()
    {
        if (startedAnim) return;
        DisableOtherAnimators();
        //StopAttacks();
        healthBar.gameObject.SetActive(false);
        startedAnim = true;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        
        shadowPos = shadow.transform.position;
        fallDir = shadowPos - (Vector2)body.position;
        while (fallDir.magnitude > 0.25f)
        {
            body.transform.position = Vector3.Lerp(body.transform.position, shadowPos, Time.deltaTime * fallSpeed);
            yield return new WaitForEndOfFrame();
            fallDir = shadowPos - (Vector2)body.position;
        }
        Instantiate(system, transform.position, Quaternion.identity);
        CameraShake.Instance.Shake(13, 0.015f);
        SoundManager.Instance.PlaySpiderDeath();
    }
    
    // This function disables other animators that would interfere with this one
    void DisableOtherAnimators()
    {
        follow.enabled = false;
        legManager.enabled = false;
        idleAnim.enabled = false;
        idleAnim.SetEnabled(false);
    }

    /*void StopAttacks()
    {
        spawner.ToggleEmit(false);
        spawner.DestroyAllParticles();
        spawner.gameObject.SetActive(false);
    }*/
}
