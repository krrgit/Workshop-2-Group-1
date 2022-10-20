using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LollipopGunReloadAnim : MonoBehaviour {
    [SerializeField] private WeaponAnimator wAnim;
    [SerializeField] private GameObject spriteObject;
    private float rotSpeed;
    private float angle;

    private void OnEnable()
    {
        wAnim.reloadDel += PlayAnim;
    }

    private void OnDisable()
    {
        wAnim.reloadDel -= PlayAnim;
    }

    public void PlayAnim(float duration)
    {
        StartCoroutine(Play(duration));
    }

    IEnumerator Play(float duration)
    {
        spriteObject.SetActive(true);
        angle = 135;
        rotSpeed = angle / duration;
        transform.localRotation = Quaternion.Euler(0,0,angle);
        while (duration > 0)
        {
            transform.localRotation = Quaternion.Euler(0,0,angle);
            duration -= Time.deltaTime;
            angle -= rotSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        spriteObject.SetActive(false);
    }
}
