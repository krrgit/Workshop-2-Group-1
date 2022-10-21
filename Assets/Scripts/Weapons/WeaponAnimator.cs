using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour {
    public SpriteRenderer sr;
    private Sprite sprReady;
    private Sprite sprEmpty;
    private Sprite sprReload;

    public delegate void ReloadDelegate(float duration, float delay);
    public ReloadDelegate reloadDel;

    private float returnSpeed;
    private float angle;
    
    
    public void SetSprites(Sprite ready, Sprite empty, Sprite reload)
    {
        sprReady = ready;
        sprEmpty = empty;
        sprReload = reload;
    }

    public void UpdateState(WeaponState state)
    {
        switch (state)
        {
            case WeaponState.Ready:
                sr.sprite = sprReady;
                break;
            case WeaponState.Cooldown:
                sr.sprite = sprReload;
                break;
            case WeaponState.Empty:
                sr.sprite = sprEmpty;
                break;
        }
    }

    public void PlayRecoil(float duration,float delay, bool isEmpty)
    {
        if (!isEmpty) reloadDel(duration, delay);
        StartCoroutine(Recoil(duration));
        
    }

    IEnumerator Recoil(float duration)
    {
        returnSpeed = 22.5f / duration;
        transform.localRotation = Quaternion.Euler(0,0,22.5f);
        angle = 45f;
        while (duration > 0)
        {
            transform.localRotation = Quaternion.Euler(0,0,angle);
            duration -= Time.deltaTime;
            angle -= returnSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        transform.localRotation = Quaternion.identity;
    }
}
