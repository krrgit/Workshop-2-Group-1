using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGameValues : MonoBehaviour {
    [SerializeField] private HubSpawnPointSO so;

    void Start()
    {
        so.bossesDefeated = 0;
    }

}
