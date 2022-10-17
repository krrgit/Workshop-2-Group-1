using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponSO : ScriptableObject {
    //Weapon Params
    [Header("Weapon Parameters")]
    public int maxAmmo = 10;
    public float reloadDur = 1;
    public bool isAutomatic;
    public float fireRate = 0.5f;
    public Sprite wpnReady;
    public Sprite wpnEmpty;
    
    // Bullet Spawner Params
    [Header("Spawner Parameters")] 
    [Range(1,25)]
    public int columns = 1;
    [Range(0,180)]
    public int spread = 0;
    public float centerDist;
    
    [Header("Bullet Parameters")]
    public float speed = 1;
    public float lifetime = 5;
    public float size = 1;
    public Color color;
    public Sprite sprite;
    public Material mtl;
}
