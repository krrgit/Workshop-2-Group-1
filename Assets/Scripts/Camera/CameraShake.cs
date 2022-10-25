using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public struct CameraShakeParams {
    public  float power;
    public float duration;
}

public class CameraShake : MonoBehaviour {


    public static CameraShake Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }

    public void Shake(float duration, float power)
    {
        StartCoroutine(ShakeAnim(duration, power));
    }

    IEnumerator ShakeAnim(float duration, float power)
    { 
        float powerFadeRate = power/duration;

        float xAmount = 0;
        float yAmount = 0;
        
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            xAmount = Random.Range(-1f, 1f) * power;
            yAmount = Random.Range(-1f, 1f) * power;

            transform.position += new Vector3(xAmount, yAmount, 0);
            power = Mathf.MoveTowards(power, 0, powerFadeRate * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = Vector3.back * 10;
    }
}
