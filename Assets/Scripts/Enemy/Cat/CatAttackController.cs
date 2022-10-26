using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatAttackController : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private CatAnimEvents animEvents;
    [Header("Stomp Pattern")]
    [SerializeField] private BulletHellSpawner stompSpawner;
    [SerializeField] private bool useStomp;
    [Header("Staff Pattern")]
    [SerializeField] private BulletHellSpawner staffSpawner;
    [SerializeField] StaffParticleAnimator staffFX;
    [SerializeField] private Transform staffPoint;
    [SerializeField] private float staffAttkDur = 10;

    [Header("Butterfly Pattern")] 
    [SerializeField] private BulletHellSpawner bfSpawner1;
    [SerializeField] private BulletHellSpawner bfSpawner2;
    [SerializeField] private BulletHellSpawner bfSpawner3;
    [SerializeField] private Transform butterfly1;
    [SerializeField] private Transform butterfly2;
    [SerializeField] private Transform butterfly3;

    private int attackPattern;

    private bool staffAttackActive;
    private bool butterflyAttackActive;
    
    private Vector3 localPos;

    public void DoStompAttack(float duration)
    {
        StartCoroutine(IStompAttack(duration));
    }

    public void DoStaffAttack()
    {
        StaffCharge();
    }

    public void DoButterflyAttack()
    {
        ButterflyAttack();
    }
    
    private void OnEnable()
    {
        animEvents.landPawDel += StompAttack;
    }

    private void OnDisable()
    {
        animEvents.landPawDel -= StompAttack;
    }

    void Start()
    {
        stompSpawner.Initialize();   
        staffSpawner.Initialize();
        bfSpawner1.Initialize();
        bfSpawner2.Initialize();
        bfSpawner3.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StaffCharge();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ButterflyAttack();
        }
    }

    void StompAttack(Vector2 position)
    {
        if (!useStomp) return;
        stompSpawner.transform.position = position;
        stompSpawner.EmitOnce();
        
        CameraShake.Instance.Shake(0.2f,0.05f);
    }

    void StaffCharge()
    {
        if (staffAttackActive) return;
        StartCoroutine(IStaffAttack());
    }
    
    void ButterflyAttack()
    {
        if (butterflyAttackActive) return;
        StartCoroutine(IButterflyAttack(7));
    }


    IEnumerator IStompAttack(float duration)
    {
        useStomp = true;
        yield return new WaitForSeconds(duration);
        useStomp = false;
    }

    IEnumerator IStaffAttack()
    {
        staffAttackActive = true;
        // Charge
        staffFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        staffFX.gameObject.SetActive(false);
        
        
        // Attack
        staffSpawner.ToggleEmit(true);
        float timer = staffAttkDur;
        CameraShake.Instance.Shake(staffAttkDur, 0.1f);
        while (timer > 0)
        {
            staffSpawner.transform.position = staffPoint.position;
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        staffSpawner.ToggleEmit(false);
        staffAttackActive = false;
    }
    
    IEnumerator IButterflyAttack(float duration)
    {
        butterflyAttackActive = true;
        bfSpawner1.ToggleEmit(true);
        bfSpawner2.ToggleEmit(true);
        bfSpawner3.ToggleEmit(true);

        while (duration > 0)
        {
            bfSpawner1.transform.position = butterfly1.transform.position;
            bfSpawner2.transform.position = butterfly2.transform.position;
            bfSpawner3.transform.position = butterfly3.transform.position;
            yield return new WaitForEndOfFrame();
            duration -= Time.deltaTime;
        }
        
        
        bfSpawner1.ToggleEmit(false);
        bfSpawner2.ToggleEmit(false);
        bfSpawner3.ToggleEmit(false);

        butterflyAttackActive = false;
    }
}
