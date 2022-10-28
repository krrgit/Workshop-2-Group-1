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
    [SerializeField] private PointAtPlayer pointAtPlayer;
    [SerializeField] private int staffLoops = 3;
    [SerializeField] private float staffShootTime = 0.4f;
    [SerializeField] private float staffWaitTime = 0.5f;
    [Header("Charge Pattern")]
    [SerializeField] private BulletHellSpawner chargeSpawner;
    [SerializeField] StaffParticleAnimator staffFX;
    [SerializeField] private Transform staffPoint;
    [SerializeField] private float chargeAtkDur = 10;

    [Header("Butterfly Pattern")] 
    [SerializeField] private GameObject butterflyFX;
    [SerializeField] private BulletHellSpawner bfSpawner1;
    [SerializeField] private BulletHellSpawner bfSpawner2;
    [SerializeField] private BulletHellSpawner bfSpawner3;
    [SerializeField] private Transform butterfly1;
    [SerializeField] private Transform butterfly2;
    [SerializeField] private Transform butterfly3;
    [SerializeField] private float butterflyAtkDur = 7;

    private int attackPattern;

    bool staffAtkActive;
    private bool chargeAtkActive;
    private bool butterflyAtkActive;
    
    private Vector3 localPos;


    public bool IsAttacking
    {
        get { return butterflyAtkActive || chargeAtkActive || staffAtkActive; }
    }

    public float StaffAtkTotalDur
    {
        get { return 5 + chargeAtkDur + 1; }
    }

    public float ButterFlyAtkTotalDur
    {
        get { return 1.5f + butterflyAtkDur + 1; }
    }
    
    public void DoStompAttack(float duration)
    {
        StartCoroutine(IStompAttack(duration));
    }

    public void DoStaffAttack()
    {
        StartCoroutine(IStaffAttack());
    }

    public void DoChargeAttack(float delay)
    {
        ChargeAttack(delay);
    }

    public void DoButterflyAttack(float delay)
    {
        ButterflyAttack(delay);
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
            ChargeAttack(0);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ButterflyAttack(0);
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

    void ChargeAttack(float delay)
    {
        if (chargeAtkActive) return;
        StartCoroutine(IChargeAttack(delay));
    }
    
    void ButterflyAttack(float delay)
    {
        if (butterflyAtkActive) return;
        StartCoroutine(IButterflyAttack(butterflyAtkDur, delay));
    }


    IEnumerator IStompAttack(float duration)
    {
        useStomp = true;
        yield return new WaitForSeconds(duration);
        useStomp = false;
    }

    IEnumerator IChargeAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
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
    
    IEnumerator IButterflyAttack(float duration, float delay)
    {
        yield return new WaitForSeconds(delay);
        butterflyAtkActive = true;
        butterflyFX.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        
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
        // Charge
        miniStaffFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        miniStaffFX.gameObject.SetActive(false);
        
        staffSpawner.transform.position = staffPoint.transform.position;
        int count = 0;
        float shootTime = staffShootTime;
        
        // Attack
        while (count < staffLoops)
        {
            
            staffSpawner.ToggleEmit(true);
            pointAtPlayer.Point();
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
