using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        // Load Hub Scene
        SceneManager.LoadScene("Room #1 Player");
    }
}
