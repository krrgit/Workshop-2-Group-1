using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour {

    public SpriteRenderer charRenderer, wpnRenderer;
    private Vector3 pointerPos;

    private Vector2 direction;
    private Vector3 scale;
    
    void Update()
    {
        GetPointerInput();
        PointAtPointer();

        HorzFlip();
        RenderOrderSwitch();
    }

    void HorzFlip()
    {
        scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -Mathf.Abs(scale.y);
        } else if (direction.x > 0)
        {
            scale.y = Mathf.Abs(scale.y);
        }

        transform.localScale = scale;
    }

    void RenderOrderSwitch()
    {
        if (direction.y < 0)
        {
            wpnRenderer.sortingOrder = charRenderer.sortingOrder + 1;
        } else if (direction.y >= 0)
        {
            wpnRenderer.sortingOrder = charRenderer.sortingOrder - 1;
        }
    }

    void GetPointerInput()
    {
        pointerPos = Input.mousePosition;
        pointerPos.z = Camera.main.nearClipPlane;
        pointerPos = Camera.main.ScreenToWorldPoint(pointerPos);
    } 

    void PointAtPointer()
    {
        direction = ((Vector2)pointerPos - (Vector2)transform.position).normalized;
        transform.right = (Vector3)direction;
    }
}
