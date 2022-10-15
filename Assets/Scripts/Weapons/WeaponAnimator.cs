using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour {
    public GunController gc;
    public SpriteRenderer sr;

    private void FixedUpdate()
    {
        switch (gc.State)
        {
            case WeaponState.Ready:
                sr.sprite = gc.w.wpnReady;
                break;
            case WeaponState.Cooldown:
            case WeaponState.Empty:
                sr.sprite = gc.w.wpnEmpty;
                break;
        }
    }
}
