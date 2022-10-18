using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CameraPanToHouse : MonoBehaviour {
    public CameraTarget camTarget;
    public float moveSpeed = 1;
    public float fovSpeed = 2;
    public float fov = 12;


    private float dist;
    private float fovDiff;
    private float defaultFOV;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        print("enter");
        if (col.gameObject.tag == "Player")
        {
            print("player enter");
            StopAllCoroutines();
            StartCoroutine(EnterTransition());
        }
    }

    IEnumerator EnterTransition()
    {
        camTarget.isEnabled = false;
        dist = ((Vector2)camTarget.transform.position - (Vector2)transform.position).magnitude;
        fovDiff = Mathf.Abs(Camera.main.orthographicSize - fov);
        defaultFOV = Camera.main.orthographicSize;
        while (dist > 0.01f && fovDiff > 0.01f)
        {
            camTarget.transform.position =
                Vector3.Lerp(camTarget.transform.position, transform.position, Time.deltaTime * moveSpeed);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,fov,Time.deltaTime * fovSpeed);
            yield return new WaitForEndOfFrame();
            
            dist = ((Vector2)camTarget.transform.position - (Vector2)transform.position).magnitude;
            fovDiff = Mathf.Abs(Camera.main.orthographicSize - fov);
        }
    }
    
    IEnumerator ExitTransition()
    {
        camTarget.isEnabled = true;
        fovDiff = Mathf.Abs(Camera.main.orthographicSize - defaultFOV);
        while (fovDiff > 0.01f)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize,defaultFOV,Time.deltaTime * fovSpeed);
            yield return new WaitForEndOfFrame();
            fovDiff = Mathf.Abs(Camera.main.orthographicSize - defaultFOV);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(ExitTransition());
        }
    }
}
