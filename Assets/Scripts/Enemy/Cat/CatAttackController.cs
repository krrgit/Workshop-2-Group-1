using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class CatAttackController : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private CatAnimEvents animEvents;
    [Header("Stomp Pattern")]
    [SerializeField] private BulletHellSpawner stompSpawner;
    [SerializeField] private bool useStomp;

    [Header("Staff Pattern")] 
    [SerializeField] private BulletHellSpawner staffSpawner;
    [SerializeField] private StaffParticleAnimator miniStaffFX;
    [SerializeField] private int staffLoops = 3;
    [SerializeField] private float staffShootTime = 0.4f;
    [SerializeField] private float staffWaitTime = 0.5f;
    [Header("Charge Pattern")]
    [SerializeField] private BulletHellSpawner chargeSpawner;
    [SerializeField] StaffParticleAnimator staffFX;
    [SerializeField] private Transform staffPoint;
    [SerializeField] private float chargeAtkDur = 10;

    [Header("Butterfly Pattern")] 
    [SerializeField] private BulletHellSpawner bfSpawner1;
    [SerializeField] private BulletHellSpawner bfSpawner2;
    [SerializeField] private BulletHellSpawner bfSpawner3;
    [SerializeField] private Transform butterfly1;
    [SerializeField] private Transform butterfly2;
    [SerializeField] private Transform butterfly3;

    private int attackPattern;

    bool staffAtkActive;
    private bool chargeAtkActive;
    private bool butterflyAtkActive;
    
    private Vector3 localPos;


    public bool IsAttacking
    {
        get { return butterflyAtkActive || chargeAtkActive || staffAtkActive;}
    }

    public float StaffAtkTotalDur
    {
        get { return 5 + chargeAtkDur; }
    }
    
    public void DoStompAttack(float duration)
    {
        StartCoroutine(IStompAttack(duration));
    }

    public void DoStaffAttack()
    {
        StartCoroutine(IStaffAttack());
    }

    public void DoChargeAttack()
    {
        ChargeAttack();
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
        chargeSpawner.Initialize();
        bfSpawner1.Initialize();
        bfSpawner2.Initialize();
        bfSpawner3.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChargeAttack();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ButterflyAttack();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            DoStaffAttack();
        }
    }

    void StompAttack(Vector2 position)
    {
        if (!useStomp) return;
        stompSpawner.transform.position = position;
        stompSpawner.EmitOnce();
        
        CameraShake.Instance.Shake(0.2f,0.05f);
    }

    void ChargeAttack()
    {
        if (chargeAtkActive) return;
        StartCoroutine(IChargeAttack());
    }
    
    void ButterflyAttack()
    {
        if (butterflyAtkActive) return;
        StartCoroutine(IButterflyAttack(7));
    }


    IEnumerator IStompAttack(float duration)
    {
        useStomp = true;
        yield return new WaitForSeconds(duration);
        useStomp = false;
    }

    IEnumerator IChargeAttack()
    {
        chargeAtkActive = true;
        // Charge
        staffFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        staffFX.gameObject.SetActive(false);
        
        
        // Attack
        chargeSpawner.ToggleEmit(true);
        float timer = chargeAtkDur;
        CameraShake.Instance.Shake(chargeAtkDur, 0.1f);
        while (timer > 0)
        {
            chargeSpawner.transform.position = staffPoint.position;
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        chargeSpawner.ToggleEmit(false);
        chargeAtkActive = false;
    }
    
    IEnumerator IButterflyAttack(float duration)
    {
        butterflyAtkActive = true;
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

        butterflyAtkActive = false;
    }

    IEnumerator IStaffAttack()
    {
        staffAtkActive = true;
        
        miniStaffFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        miniStaffFX.gameObject.SetActive(false);
        
        int count = 0;
        float shootTime = staffShootTime;
        while (count < staffLoops)
        {
            
            staffSpawner.ToggleEmit(true);
            CameraShake.Instance.Shake(staffShootTime, 0.01f);
            while (shootTime > 0)
            {
                staffSpawner.transform.position = staffPoint.transform.position;
                yield return new WaitForEndOfFrame();
                shootTime -= Time.deltaTime;
            }
            staffSpawner.ToggleEmit(false);
            
            ++count;
            shootTime = staffShootTime;
            yield return new WaitForSeconds(staffWaitTime);
        }
        staffAtkActive = false;
    }
}
