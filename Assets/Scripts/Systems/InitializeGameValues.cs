using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGameValues : MonoBehaviour {
    [SerializeField] private HubSpawnPointSO so;

    void Start()
    {
        so.spawn = HubSpawnPoint.Home;
        so.bossesDefeated = 0;
    }

}
