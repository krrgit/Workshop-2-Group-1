using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour {
    public SpriteRenderer sr;
    private Sprite sprReady;
    private Sprite sprEmpty;
    
    public void SetSprites(Sprite ready, Sprite empty)
    {
        sprReady = ready;
        sprEmpty = empty;
    }

    public void UpdateState(WeaponState state)
    {
        switch (state)
        {
            case WeaponState.Ready:
                sr.sprite = sprReady;
                break;
            case WeaponState.Cooldown:
            case WeaponState.Empty:
                sr.sprite = sprEmpty;
                break;
        }
    }

    public void PlayRecoil(float duration)
    {
        StartCoroutine(Recoil(duration));
    }

    IEnumerator Recoil(float duration)
    {
        float returnSpeed = 22.5f / duration;
        transform.localRotation = Quaternion.Euler(0,0,45);
        float angle = 45f;
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
