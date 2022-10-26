using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashAnimation : MonoBehaviour {
    [SerializeField] private Image img;
    [SerializeField] private float duration = 3;

    void OnEnable()
    {
        StartCoroutine(IFade());
    }
    

    IEnumerator IFade()
    {
        Color color = Color.white;
        color.a = 1;
        img.color = color;

        float delta = Time.deltaTime / duration;
        while (color.a > 0)
        {
            color.a = Mathf.Lerp(color.a,0,delta);
            img.color = color;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
    }
}
