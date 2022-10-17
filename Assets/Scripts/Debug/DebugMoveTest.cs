using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMoveTest : MonoBehaviour {
    public float speed;

    private int dirx;
    private int diry;

    // Update is called once per frame
    void Update()
    {
        dirx = (Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0);
        diry = (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0);

        transform.position += new Vector3(dirx * Time.deltaTime * speed, diry * Time.deltaTime * speed, 0);
    }
}
