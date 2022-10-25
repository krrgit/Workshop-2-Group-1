using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor.UIElements;
using UnityEngine;

public class firstPattern : MonoBehaviour
{
    [SerializeField] private BulletHellSpawner spawner1;
    [SerializeField] private BulletHellSpawnerSO pattern1;
    [SerializeField] private BulletHellSpawnerSO pattern2;
    
    public float time = 3f;
    
    void Start()
    {
        spawner1.Initialize();
        spawner1.LoadSO(pattern1);
        StartCoroutine(StartPatternOne());
    }

    



    IEnumerator StartPatternOne()
    {
        for (int i = 0; i < 10; i++)
        {
            spawner1.LoadSO(pattern1);
            yield return new WaitForSeconds(time);
            spawner1.ToggleEmit(false);
            spawner1.LoadSO(pattern2);
            yield return new WaitForSeconds(time - 1);
            spawner1.ToggleEmit(true);
        }
    }
    

}
