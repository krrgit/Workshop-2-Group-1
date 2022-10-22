using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmAnimator : MonoBehaviour {
    [SerializeField] private SpriteRenderer rightArm;
    [SerializeField] private SpriteRenderer leftArm;
    public Transform weapon;

    private bool usingRight;
    private bool isActive = true;

    public void SetActive(bool state)
    {
        isActive = state;
    }
    public void PointArm()
    {
        if (!isActive) return;
        if (usingRight)
        {
            rightArm.transform.parent.up = rightArm.transform.parent.position - weapon.position;
        }
        else
        {
            leftArm.transform.parent.up = leftArm.transform.parent.position - weapon.position;
        }
    }

    public void SwitchArms(bool isRight)
    {
        if (!isActive) return;
        usingRight = isRight;
        ResetArm(!isRight);
    }

    void ResetArm(bool isRight)
    {
        if (!isActive) return;
        if (isRight)
        {
            rightArm.transform.parent.up = Vector3.up;
        }
        else
        {
            leftArm.transform.parent.up = Vector3.up;
        }
    }

    public void SetSpriteSortingOrder(int newLayer)
    {
        rightArm.sortingOrder = newLayer;
        leftArm.sortingOrder = newLayer;
    }
}
