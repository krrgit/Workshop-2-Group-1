using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPattern : MonoBehaviour
{
    [SerializeField] private BulletHellSpawner spawner1;
    [SerializeField] private BulletHellSpawnerSO pattern1;

    void Start()
    {
        spawner1.Initialize();
        spawner1.ToggleEmit(true);
        spawner1.LoadSO(pattern1);
        

    }
}
