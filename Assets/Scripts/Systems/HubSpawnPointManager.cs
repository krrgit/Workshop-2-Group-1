using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubSpawnPointManager : MonoBehaviour {
    [SerializeField] private HubSpawnPointSO so;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camTarget;
    [SerializeField] private Transform homePoint;
    [SerializeField] private Transform spiderPoint;
    [SerializeField] private Transform catPoint;
    
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        switch (so.spawn)
        {
            case HubSpawnPoint.Home:
                player.position = homePoint.position;
                camTarget.position = homePoint.position;
                break;
            case HubSpawnPoint.SpiderHouse:
                player.position = spiderPoint.position;
                camTarget.position = spiderPoint.position;
                break;
            case HubSpawnPoint.CatHouse:
                player.position = catPoint.position;
                camTarget.position = catPoint.position;
                break;
            default:
                break;
        }
    }
}
