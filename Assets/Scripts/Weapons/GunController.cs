using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
    public GameObject reloadPrompt;

    private bool isReloading;
    private float cooldown; // for semi auto firing
    private bool isEmitting;

    private WeaponState state;

    [SerializeField] private DisplayAmmo displayAmmo;

    public WeaponState State
    {
        get { return state; }
    }

    void Start()
    {
        ammo = w.maxAmmo;
        spawner.WeaponInit(w);
        wAnim.SetSprites(w.wpnReady,w.wpnEmpty,w.wpnReload);
        
    }

    // Update is called once per frame
    void Update()
    {
        Controls();

        CooldownTick();
        AutoAmmoTick();
        StopAutoFireCheck();
    }

    void Controls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (w.isAutomatic) {
                AutoFire();
            }
            else {
                Fire();   
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopAutoFire();
        }
        
        if (Input.GetKeyDown(KeyCode.E)) {
            Reload();
        }
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
            SetCooldownValues();
            
            SoundManager.Instance.PlayShoot();
        }
    }

    void AutoAmmoTick() {
        if (!w.isAutomatic || !spawner.IsEmitting) return;
        
        ammo = w.maxAmmo - spawner.EmitAmount;
        ammo = ammo >= 0 ? ammo : 0;
        if (ammo == 0)
        {
            state = WeaponState.Empty;
            reloadPrompt.SetActive(true);
            wAnim.UpdateState(state);
        }
    }

    void StopAutoFireCheck() {
        if (ammo == 0) {
            StopAutoFire();
        }
    }

    void AutoFire() {
        if (isEmitting || ammo == 0 || cooldown > 0) return;

        isEmitting = true;
        InvokeRepeating("DoAutoEmit", 0, w.fireRate);
        
    }

    void SetCooldownValues()
    {
        state = WeaponState.Cooldown;
        --ammo;
        if (displayAmmo) displayAmmo.updateAmmo(ammo);
        cooldown = w.fireRate;
        wAnim.UpdateState(state);
        wAnim.PlayRecoil(w.fireRate * 0.5f, w.fireRate * 0.4f, ammo == 0);
    }

    void DoAutoEmit()
    {
        spawner.EmitOnce();
        SetCooldownValues();
        
        SoundManager.Instance.PlayShoot();
    }

    void StopAutoFire() {
        if (isEmitting) {
            CancelInvoke();
            isEmitting = false;
        }

        if (ammo == 0)
        {
            reloadPrompt.SetActive(true);
        }
    }

    void Reload() {
        if (ammo == w.maxAmmo || isReloading) return;

        StartCoroutine(IReload());
    }

    IEnumerator IReload()
    {
        isReloading = true;
        reloadPrompt.SetActive(false);
        yield return new WaitForSeconds(w.reloadDur);
        ammo = w.maxAmmo;
        displayAmmo.updateAmmo(ammo);
        isReloading = false;
        state = WeaponState.Ready;
        wAnim.UpdateState(state);
        reloadPrompt.SetActive(false);
    }
}
