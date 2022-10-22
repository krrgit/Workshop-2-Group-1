using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletPattern", order = 1)]
public class BulletHellSpawnerSO : ScriptableObject
{
    [Header("Spawner")]
    [Range(1,25)]
    public int columns = 5;
    [Range(0,360)]
    public int spread = 360; // max angle between first and last column. 360 = max
    [Range(-180,180)]
    public float spinSpeed = 0;
    public float centerDist = 0;
    public float fireRate = 1;
    
    [Header("Bullet")]
    public float speed = 1;
    public float lifetime = 5;
    public float size = 1;
    public Color color;
    public Sprite sprite;
    public Material mtl;
}
