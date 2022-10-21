using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    void Awake()
    {
        // This only allows one instance of PlayerHealth to exist in any scene
        // This is to avoid the need for GetComponent Calls. Use PlayerHealth.Instance instead.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void PlayerDeath()
    {
        // Stop player movement
        // show death screen UI
        // press key to restart/reload scene
        
    }
}
