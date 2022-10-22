using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitstunAnimation : MonoBehaviour {
    [SerializeField] SpriteRenderer sr;
    public bool wiggle = true;
    [SerializeField] float duration = 0.05f;
    [SerializeField] private float displacement = 0.05f;
    [SerializeField] private Material whiteMtl;
    
    private Material spriteMtl;
    private Color spriteColor;

    private Vector3 startPos;
    public float Duration
    {
        get { return duration; }
    }

    public void Wiggle(Vector3 position)
    {
        transform.position = position + new Vector3(Random.Range(-displacement, displacement), Random.Range(-displacement, displacement), 0);
    }

    public void SetToWhite()
    {
        spriteMtl = sr.material;
        spriteColor = sr.color;
        sr.material = whiteMtl;
        sr.color = Color.white;
    }

    public void RevertSprite()
    {
        sr.material = spriteMtl;
        sr.color = spriteColor;
    }

    public void PlayAnimation(Collider2D coll)
    {
        StartCoroutine(HitStunAnim(coll));
    }

    IEnumerator HitStunAnim(Collider2D coll)
    {
        startPos = transform.position;
        SetToWhite();
        coll.enabled = false;
        if (wiggle)
        {
            float timer = duration;
            while (timer > 0)
            {
                Wiggle(startPos);
                yield return new WaitForEndOfFrame();
                timer -= Time.deltaTime;
            }

            transform.position = startPos;
        } else
        {
            yield return new WaitForSeconds(duration);
        }
        coll.enabled = true;
        RevertSprite();
        RevertSprite();
    }
}
