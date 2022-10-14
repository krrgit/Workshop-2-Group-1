using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour {
    public BulletHellSpawner spawner;
    public int ammo;
    public int maxAmmo;
    public float reloadDur;
    public bool isAutomatic;
    public float firerate;

    private bool isReloading;
    private float cooldown; // for semi auto firing

    // Update is called once per frame
    void Update()
    {
        DebugControlTest();

        CooldownTick();
        AutoAmmoTick();
    }

    void DebugControlTest() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (isAutomatic) {
                AutoFire();
            }
            else {
                Fire();   
            }
        }

        if (Input.GetKeyUp(KeyCode.E)) {
            StopFire();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            Reload();
        }

        StopFireAutoCheck();
    }

    void CooldownTick() {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void AutoAmmoTick() {
        if (!isAutomatic || !spawner.IsEmitting) return;
        
        ammo = maxAmmo - spawner.EmitAmount;
        ammo = ammo >= 0 ? ammo : 0;
    }

    void StopFireAutoCheck() {
        if (ammo == 0) {
            StopFire();
        }
    }

    void Fire() {
        if (ammo == 0) return;

        if (cooldown <= 0) {
            spawner.EmitOnce();
            cooldown = firerate;
            --ammo;
        }
    }

    void AutoFire() {
        if (spawner.IsEmitting || ammo == 0) return;
        
        spawner.ToggleEmit(true);
    }

    void StopFire() {
        if (spawner.IsEmitting) {
            spawner.ToggleEmit(false);
        }
    }

    void Reload() {
        if (ammo == maxAmmo || isReloading) return;

        StartCoroutine(IReload());
    }

    IEnumerator IReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadDur);
        ammo = maxAmmo;
        isReloading = false;
    }
}
