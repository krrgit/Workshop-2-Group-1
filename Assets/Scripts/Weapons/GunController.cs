using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum WeaponState {
    Ready,
    Cooldown,
    Empty
}

public class GunController : MonoBehaviour {
    public WeaponBulletSpawner spawner;
    public WeaponSO w;
    public WeaponAnimator wAnim;
    public int ammo;

    private bool isReloading;
    private float cooldown; // for semi auto firing
    private bool isEmitting;

    private WeaponState state;

    public WeaponState State
    {
        get { return state; }
    }

    void Start()
    {
        ammo = w.maxAmmo;
        spawner.WeaponInit(w);
        wAnim.SetSprites(w.wpnReady,w.wpnEmpty);
    }

    // Update is called once per frame
    void Update()
    {
        DebugControlTest();

        CooldownTick();
        AutoAmmoTick();
        StopAutoFireCheck();
    }

    void DebugControlTest() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (w.isAutomatic) {
                AutoFire();
            }
            else {
                Fire();   
            }
        }

        if (Input.GetKeyUp(KeyCode.E)) {
            StopAutoFire();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            Reload();
        }

    }

    void CooldownTick() {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 6 * Time.deltaTime && ammo != 0)
            {
                state = WeaponState.Ready;
                wAnim.UpdateState(state);
            }
        }
    }
    
    void Fire() {
        if (ammo == 0) return;

        if (cooldown <= 0) {
            spawner.EmitOnce();
            cooldown = w.fireRate;
            --ammo;
            state = WeaponState.Cooldown;
            wAnim.PlayRecoil(w.fireRate - 0.05f);
            wAnim.UpdateState(state);
        }
    }

    void AutoAmmoTick() {
        if (!w.isAutomatic || !spawner.IsEmitting) return;
        
        ammo = w.maxAmmo - spawner.EmitAmount;
        ammo = ammo >= 0 ? ammo : 0;
        if (ammo == 0)
        {
            state = WeaponState.Empty;
            wAnim.UpdateState(state);;
        }
    }

    void StopAutoFireCheck() {
        if (ammo == 0) {
            StopAutoFire();
        }
    }

    void AutoFire() {
        if (isEmitting || ammo == 0) return;

        isEmitting = true;
        InvokeRepeating("DoAutoEmit", 0, w.fireRate);
        
    }

    void DoAutoEmit()
    {
        spawner.EmitOnce();
        state = WeaponState.Cooldown;
        wAnim.UpdateState(state);
        --ammo;
        cooldown = w.fireRate;
        wAnim.UpdateState(state);
        wAnim.PlayRecoil(w.fireRate * 0.5f);
    }

    void StopAutoFire() {
        if (isEmitting) {
            CancelInvoke();
            isEmitting = false;
        }
    }

    void Reload() {
        if (ammo == w.maxAmmo || isReloading) return;

        StartCoroutine(IReload());
    }

    IEnumerator IReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(w.reloadDur);
        ammo = w.maxAmmo;
        isReloading = false;
        state = WeaponState.Ready;
        wAnim.UpdateState(state);
    }
}
