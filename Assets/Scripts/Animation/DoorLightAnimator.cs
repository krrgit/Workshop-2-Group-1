using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightAnimator : MonoBehaviour {
    [SerializeField] private float duration;
    [SerializeField] private float width;

    private Vector3 tempSize;
    private void OnEnable()
    {
        StartCoroutine(IClose());
    }

    public void PlayOpenAnim()
    {
        StopAllCoroutines();
        StartCoroutine(IOpen());
    }

    IEnumerator IClose()
    {
        yield return new WaitForSeconds(2);
        
        tempSize = transform.localScale;
        while (transform.localScale.x > 0.01f)
        {
            tempSize.x = Mathf.Lerp(tempSize.x, 0, Time.deltaTime);
            transform.localScale = tempSize;
            yield return new WaitForEndOfFrame();
        }

        tempSize.x = 0;
        transform.localScale = tempSize;
    }

    IEnumerator IOpen()
    {
        yield return new WaitForSeconds(5);
        tempSize = transform.localScale;
        while (tempSize.x+0.01f < width)
        {
            tempSize.x = Mathf.Lerp(tempSize.x, width, Time.deltaTime / duration);
            transform.localScale = tempSize;
            yield return new WaitForEndOfFrame();
        }
        tempSize.x = width;
        transform.localScale = tempSize;
    }
    


}
