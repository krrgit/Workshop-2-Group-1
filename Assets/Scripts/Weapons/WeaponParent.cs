using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour {

    public SpriteRenderer charRenderer, wpnRenderer; // character renderer is needed in order to know which layer is on top or behind
    public PlayerArmAnimator playerArms;
    private Vector3 pointerPos; // position of the pointer in world space

    private Vector2 direction; // direction from the player to the pointer
    private Vector3 scale; // Scale of the gun. Used to flip the sprite horizontally
    private Vector3 localPos;

    void Update()
    {
        GetPointerInput();
        PointAtPointer();

        HorzFlip();
        RenderOrderSwitch();
        playerArms.PointArm();
    }

    void HorzFlip()
    {
        scale = transform.localScale;
        localPos = transform.localPosition;
        if (direction.x < 0)
        {
            scale.y = -Mathf.Abs(scale.y);
            playerArms.SwitchArms(false);
        } else if (direction.x > 0)
        {
            scale.y = Mathf.Abs(scale.y);
            playerArms.SwitchArms(true);
        }

        transform.localScale = scale;
        transform.localPosition = localPos;
    }
    
    // This function sets the weapon above or below the player
    void RenderOrderSwitch()
    {
        if (direction.y < 0)
        {
            wpnRenderer.sortingOrder = charRenderer.sortingOrder + 1;
            playerArms.SetSpriteSortingOrder(charRenderer.sortingOrder+2);
        } else if (direction.y >= 0)
        {
            wpnRenderer.sortingOrder = charRenderer.sortingOrder - 2;
            playerArms.SetSpriteSortingOrder(charRenderer.sortingOrder - 1);
        }
    }
    
    void GetPointerInput()
    {
        pointerPos = Input.mousePosition;
        pointerPos.z = Camera.main.nearClipPlane;
        pointerPos = Camera.main.ScreenToWorldPoint(pointerPos);
    } 
    
    // Makes the weapon point at the pointer
    void PointAtPointer()
    {
        direction = ((Vector2)pointerPos - (Vector2)transform.position).normalized;
        transform.right = (Vector3)direction;
    }
}
