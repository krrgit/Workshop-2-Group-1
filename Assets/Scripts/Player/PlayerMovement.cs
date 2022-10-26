using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;


    public Rigidbody2D rb;
    // public Camera cam;
    public Animator animator;


    public static PlayerMovement Instance;
    Vector2 movement;
    // Vector2 mousePos;
    
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

        //following mouse position
        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        //following mouse position
        // Vector2 lookDir = mousePos - rb.position;
        // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // rb.rotation = angle;
    }
}
