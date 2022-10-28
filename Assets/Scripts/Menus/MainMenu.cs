using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private int HubSceneIndex = 1;
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        // Load Hub Scene
        SceneManager.LoadScene(HubSceneIndex);
    }
}
