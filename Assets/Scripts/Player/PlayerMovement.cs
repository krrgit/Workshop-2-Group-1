using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float baseSpeed;

    public Rigidbody2D rb;
    // public Camera cam;
    public Animator animator;

    public float dashPower;
    public float dashTime;

    bool isDashing = false;

    public static PlayerMovement Instance;
    Vector2 movement;
    private Vector3 direction;

    Vector3 pointerPos;

    // Vector2 mousePos;

    private Vector3 predictedPosition;

    public Vector3 PredictedPosition(float secondsInAdvance)
    {
        return transform.position + (Vector3)(movement * moveSpeed * secondsInAdvance);
    }
    
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

    void Start()
    {
        moveSpeed = baseSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        GetPointerInput();
        
        direction = pointerPos - transform.position;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //following mouse position
        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if(movement.magnitude >= 0)
        {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isDashing)
            {
                StartCoroutine(Dash());
            }
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        //following mouse position
        // Vector2 lookDir = mousePos - rb.position;
        // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        // rb.rotation = angle;
    }

    void GetPointerInput()
    {
        pointerPos = Input.mousePosition;
        pointerPos.z = Camera.main.nearClipPlane;
        pointerPos = Camera.main.ScreenToWorldPoint(pointerPos);
    } 

    IEnumerator Dash()
    {
        isDashing = true;
        moveSpeed *= dashPower;

        yield return new WaitForSeconds(dashTime);
        moveSpeed = baseSpeed;
        isDashing = false;
    }
}
