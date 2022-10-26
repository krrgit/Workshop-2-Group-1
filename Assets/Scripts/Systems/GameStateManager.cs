using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour {
    [SerializeField] private HubSpawnPointSO hubSO;
    [SerializeField] private GameObject deathCanvas;
    [SerializeField] private PlayerDeathAnimation playerDeathAnim;
    [SerializeField] private EnemyHealth bossHealth;
    [SerializeField] private GameObject screenFlash;
    [SerializeField] private DoorLightAnimator doorLight;
    [SerializeField] private GameObject exit;
    public static GameStateManager Instance;

    private bool enableRestart = false;

    private void OnEnable()
    {
        bossHealth.enemyDeathDel += BossDeath;
    }
    
    private void OnDisable()
    {
        bossHealth.enemyDeathDel -= BossDeath;
    }

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

    public void PlayerDeath()
    {
        // Stop player movement
        // show death screen UI
        // press key to restart/reload scene
        deathCanvas.SetActive(true);
        Destroy(PlayerMovement.Instance);
        playerDeathAnim.Play();
        CameraTarget.Instance.isEnabled = false;

        enableRestart = true;
    }

    public void BossDeath()
    {
        screenFlash.SetActive(true);
        doorLight.PlayOpenAnim();
        hubSO.spawn = HubSpawnPoint.SpiderHouse;
        StartCoroutine(WaitForExitPopup());

    }

    IEnumerator WaitForExitPopup()
    {
        yield return new WaitForSeconds(7);
        exit.SetActive(true);
    }
}
