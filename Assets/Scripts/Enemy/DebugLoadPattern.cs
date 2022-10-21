using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLoadPattern : MonoBehaviour {
    [SerializeField] private BulletHellSpawner spawner;

    [SerializeField] private BulletHellSpawnerSO pattern;
    // Start is called before the first frame update
    void Start()
    {
        spawner.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            spawner.LoadSO(pattern);
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            spawner.ToggleEmit(true);
        }
        
        if (Input.GetKeyUp(KeyCode.O))
        {
            spawner.ToggleEmit(false);
        }
    }
}
