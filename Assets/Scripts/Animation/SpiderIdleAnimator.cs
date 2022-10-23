using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdleAnimator : MonoBehaviour {

    [SerializeField] private bool isEnabled = true;
     public float endDuration = 0.25f;
     public float midDuration = 0.05f;
     public float displaceY = 0.05f;

     private float displacement;
     private int dir = 1;

     public void SetEnabled(bool state)
     {
         isEnabled = state;
     }
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Animate()
    {
        while (isEnabled)
        {
            transform.localPosition = Vector3.up * displacement;
            yield return new WaitForSeconds(midDuration);
            FindDisplacement();
            transform.localPosition = Vector3.up * displacement;
            yield return new WaitForSeconds(endDuration);
            FindDisplacement();
            dir = -dir;
        }
    }

    void FindDisplacement()
    {
        if (dir == 1)
        {
            displacement = displacement == 0 ? displaceY : displacement;
            displacement = displacement == displaceY ? displaceY*2 : displaceY;

        }
        else
        {
            displacement = displacement == displaceY*2 ? displaceY : displacement;
            displacement = displacement == displaceY ? 0 : displaceY;
        }
    }
}
