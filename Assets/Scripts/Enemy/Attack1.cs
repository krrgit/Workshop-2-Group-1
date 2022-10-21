using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
   [SerializeField] private BulletHellSpawner spawner;
   [SerializeField]  BulletHellSpawnerSO pattern;
   
   
    // Start is called before the first frame update
    void Start()
    {
        spawner.Initialize();
        spawner.ToggleEmit(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            spawner.LoadSO(pattern);
            
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            spawner.ToggleEmit(false);
        }
    }
}
