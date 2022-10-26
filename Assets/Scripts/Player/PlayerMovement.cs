using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public static PlayerMovement Instance;
    Vector2 movement;

    void Awake()
    {
        // This only allows one instance of PlayerHealth to exist in any scene
        // This is to avoid the need for GetComponent Calls. Use PlayerMovement.Instance instead.
        if (Instance == null) {
            Instance = this;
        }else {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
