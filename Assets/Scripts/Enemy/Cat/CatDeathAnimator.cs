using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDeathAnimator : MonoBehaviour {
    [SerializeField] private GameObject deathFX;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D coll;

    [SerializeField] private GameObject ui;

    public void Play()
    {
        deathFX.transform.position = sr.transform.position;
        deathFX.SetActive(true);
        ui.SetActive(false);
        
        SoundManager.Instance.PlayCatButterfly();

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Color color = sr.color;
        color.a = 1;
        float duration = 5;
        float rate = Time.deltaTime / 5;

        while (duration > 0)
        {
            sr.color = color;
            color.a -= rate;
            yield return new WaitForEndOfFrame();
            duration -= Time.deltaTime;
        }

        color.a = 0;
        sr.color = color;
    }
}
