using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmAnimator : MonoBehaviour {
    [SerializeField] private SpriteRenderer rightHand;
    [SerializeField] private SpriteRenderer leftHand;
    public Transform weapon;

    private bool usingRight;
    private bool isActive = true;

    private Vector2 defaultHandPosR;
    private Vector2 defaultHandPosL;

    void Start()
    {
        defaultHandPosL = leftHand.transform.localPosition;
        defaultHandPosR = rightHand.transform.localPosition;
    }

    public void SetActive(bool state)
    {
        isActive = state;
    }
    public void PointArm()
    {
        if (!isActive) return;
        if (usingRight)
        {
            rightHand.transform.position = weapon.position;
        }
        else
        {
            leftHand.transform.position = weapon.position;
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
            rightHand.transform.localPosition = defaultHandPosR;
        }
        else
        {
            leftHand.transform.localPosition = defaultHandPosL;
        }
    }

    public void SetSpriteSortingOrder(int newLayer)
    {
        rightHand.sortingOrder = newLayer;
        leftHand.sortingOrder = newLayer;
    }
}
