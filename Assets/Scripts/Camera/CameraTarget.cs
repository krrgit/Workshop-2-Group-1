using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {
    public bool isEnabled = true;
    public Transform player;
    public float speed = 4; // Speed at which to lerp to the new position
    public float panDist = 3; // The max amount the camera pans toward the cursor
    private Vector3 direction;
    Vector3 pointerPos;
    private Vector2 pointerPerc; // Percentage the pointer is away from center. 100% = edges. 0% = center.

    private Vector3 targetPos; // The position this target lerps to.
    
    void Update()
    {
        if (!isEnabled) return;
        GetPointerInput();
        GetPointerPercentage();

        GetDirection();
        UpdatePosition();
    }

    void GetDirection()
    {
        direction = ((Vector2)pointerPos - (Vector2)player.position);
        direction = Vector3.ClampMagnitude(direction,1);
    }

    void UpdatePosition()
    {
        targetPos = player.position +
                    new Vector3(direction.x * Mathf.Abs(pointerPerc.x) * panDist, direction.y * Mathf.Abs(pointerPerc.y) * panDist, 0);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    void GetPointerPercentage()
    {
        pointerPerc = ((Vector2)Input.mousePosition - new Vector2(Camera.main.pixelWidth/2f,Camera.main.pixelHeight/2f)) / new Vector2(Camera.main.pixelWidth,Camera.main.pixelHeight);
    }

    void GetPointerInput()
    {
        pointerPos = Input.mousePosition;
        pointerPos.z = Camera.main.nearClipPlane;
        pointerPos = Camera.main.ScreenToWorldPoint(pointerPos);
    } 
}
