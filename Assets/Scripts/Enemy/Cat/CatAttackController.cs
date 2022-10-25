using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatAttackController : MonoBehaviour {
    [SerializeField] private Animator anim;
    [SerializeField] private CatAnimEvents animEvents;
    [SerializeField] private BulletHellSpawner stompSpawner;
    [SerializeField] private BulletHellSpawner staffSpawner;
    [SerializeField] private bool useStomp;
    [SerializeField] private Transform staffPoint;
    [SerializeField] private float staffAttkDur = 10;

    [SerializeField] StaffParticleAnimator staffFX;

    private int attackPattern;

    private Vector3 localPos;
    
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StaffCharge();
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
        StartCoroutine(IStaffAttack());
    }

    IEnumerator IStaffAttack()
    {
        // Charge
        staffFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        staffFX.gameObject.SetActive(false);
        
        
        // Attack
        staffSpawner.ToggleEmit(true);
        float timer = staffAttkDur;
        CameraShake.Instance.Shake(staffAttkDur, 0.02f);
        while (timer > 0)
        {
            staffSpawner.transform.position = staffPoint.position;
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        staffSpawner.ToggleEmit(false);
    }

    void StaffAttack()
    {
        staffSpawner.transform.position = staffPoint.position;
        staffSpawner.EmitOnce();
        
        CameraShake.Instance.Shake(0.5f, 0.1f);
    }


}
