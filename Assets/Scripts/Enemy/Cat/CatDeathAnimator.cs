using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDeathAnimator : MonoBehaviour {
    [SerializeField] private GameObject deathFX;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D coll;
    public void Play()
    {
        deathFX.transform.position = sr.transform.position;
        deathFX.SetActive(true);

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
        }

        coll.enabled = false;

    }
}
