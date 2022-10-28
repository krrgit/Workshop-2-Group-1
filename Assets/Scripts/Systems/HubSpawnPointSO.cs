using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HubSpawnPoint {
    Home,
    SpiderHouse,
    CatHouse
}
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HubSpawnPoint", order = 1)]
public class HubSpawnPointSO : ScriptableObject {
    public HubSpawnPoint spawn;
    public int bossesDefeated;
}
