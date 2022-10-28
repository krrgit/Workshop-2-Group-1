using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour {
    public SpriteRenderer sr;
    private Sprite sprReady;
    private Sprite sprEmpty;
    private Sprite sprReload;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Camera Shake Values")] 
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakePower;
    [SerializeField] GameObject reloadPrompt;

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
                muzzleFlash.Emit(25);
                break;
            case WeaponState.Empty:
                sr.sprite = sprEmpty;
                break;
        }
    }

    public void PlayRecoil(float duration,float delay, bool isEmpty)
    {
        if (!isEmpty) reloadDel(duration, delay);
        CameraShake.Instance.Shake(shakeDuration, shakePower);
        StartCoroutine(Recoil(duration, isEmpty));
        
    }

    IEnumerator Recoil(float duration, bool isEmpty)
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

        if (!isEmpty) UpdateState(WeaponState.Ready);
        transform.localRotation = Quaternion.identity;
    }
}
