using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float baseSpeed;
    
    bool isDashing = false;
    public float dashPower;
    public float dashTime;
    public float cooldownTime;
    private float nextDashTime = 0;
    
    public Rigidbody2D rb;
    // public Camera cam;
    public Animator animator;

    

    public static PlayerMovement Instance;
    Vector2 movement;
    private Vector3 direction;

    Vector3 pointerPos;

    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    
    bool isInvincible;

    public SpriteRenderer mySprite;

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

        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if(movement.magnitude >= 0)
        {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        }

        //Left shift to dash
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {            
            if (!isDashing)
            {
                if (Time.time > nextDashTime)
                {
                StartCoroutine(Dash());
                nextDashTime = Time.time + cooldownTime;
                }
            }
        } 

            
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void GetPointerInput()
    {
        pointerPos = Input.mousePosition;
        pointerPos.z = Camera.main.nearClipPlane;
        pointerPos = Camera.main.ScreenToWorldPoint(pointerPos);
    } 

    IEnumerator Dash()
    {
        triggerCollider.enabled = false;

        isDashing = true;
        moveSpeed *= dashPower;

        yield return new WaitForSeconds(dashTime);
        moveSpeed = baseSpeed;
        isDashing = false;

        if(!isInvincible)
        {
            triggerCollider.enabled = true;
        }
    }

    private IEnumerator FlashCo()
    {
        isInvincible = true;

        int temp = 0;
        triggerCollider.enabled = false;

        while(temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        mySprite.color = regularColor;

        triggerCollider.enabled = true;

        isInvincible = false;
    }
    
    public void startInvincibility()
    {
        if(isInvincible) return;
        
        StartCoroutine(FlashCo());
    }
}
