using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject DeathCanvas;
    [SerializeField] private PlayerDeathAnimation playerDeathAnim;
    public static GameStateManager Instance;

    private bool enableRestart = false;
    

    void Awake()
    {
        // This only allows one instance of GameStateManager to exist in any scene
        // This is to avoid the need for GetComponent Calls. Use GameStateManager.Instance instead.
        if (Instance == null) {
            Instance = this;
        }else {
            Destroy(this);
        }
    }

    private void LateUpdate()
    {
        RestartCheck();
    }

    void RestartCheck()
    {
        if (!enableRestart) return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }

    public void Death()
    {
        // Stop player movement
        // show death screen UI
        // press key to restart/reload scene
        DeathCanvas.SetActive(true);
        Destroy(PlayerMovement.Instance);
        playerDeathAnim.Play();
        CameraTarget.Instance.isEnabled = false;
        
        
        enableRestart = true;
    }
}
