using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBulletHellSpawnerDemo : MonoBehaviour {
    [SerializeField] private BulletHellSpawner spawner;
    
    // Start is called before the first frame update
    void Start()
    {
        spawner.Initialize();
        spawner.ToggleEmit(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
