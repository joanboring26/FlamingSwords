using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject UICanvas;
    public GameObject optionsMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
                Debug.Log("Resumed");
            }
            else
            {
                Pause();
                Debug.Log("Paused");
            }
        }
    }

    public void Resume() {

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        UICanvas.SetActive(true);
        gamePaused = false;
        Time.timeScale = 1f;
        
    }

    void Pause() {

        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        UICanvas.SetActive(false);
        gamePaused = true;
        Time.timeScale = 0f;
       
        

    }

    public void toMainMenu() {

        gamePaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        UICanvas.SetActive(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NewMainMenu");
      
    }

    public void ToggleOptions() {

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
      
    }

    public void Quit()
    {
        Application.Quit();
    }
}
