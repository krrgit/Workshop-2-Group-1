using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour {
    public Transform player;
    public float speed = 1;

    private Vector3 direction;
    Vector3 pointerPos;
    private Vector2 pointerPerc;

    private Vector3 targetPos;

    void Update()
    {
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
                    new Vector3(direction.x * Mathf.Abs(pointerPerc.x) * 3, direction.y * Mathf.Abs(pointerPerc.y) * 3, 0);
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
