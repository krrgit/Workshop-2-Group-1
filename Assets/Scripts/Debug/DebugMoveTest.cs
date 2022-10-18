using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMoveTest : MonoBehaviour {

    public Transform targetsR;
    public Transform targetsL;
    
    
    public float speed;

    private int dirx;
    private int diry;

    private Vector3 dir;
    
    

    // Update is called once per frame
    void Update()
    {
        dirx = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
        diry = (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0);
        dir = new Vector3(dirx, diry, 0);
        dir = Vector3.ClampMagnitude(dir, 1);
        
        transform.position += dir * Time.deltaTime * speed;
        
        
        if (dirx > 0)
        {
            targetsL.localPosition = dir;
            dir.x = 0;
            targetsR.localPosition = dir;
        } else if (dirx < 0)
        {
            targetsR.localPosition = dir;
            dir.x = 0;
            targetsL.localPosition = dir;
        }
        else
        {
            dir.x = 0;
            targetsR.localPosition = dir;
            targetsL.localPosition = dir;
        }


    }
}
