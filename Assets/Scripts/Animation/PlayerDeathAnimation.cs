using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathAnimation : MonoBehaviour {
    [SerializeField] private PlayerArmAnimator arms;
    [SerializeField] private WeaponParent weaponParent;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private Sprite deadSprite;
    
    

    public void Play()
    {
        arms.gameObject.SetActive(false);
        weaponParent.enabled = false;
        renderer.sprite = deadSprite;
    }
}
