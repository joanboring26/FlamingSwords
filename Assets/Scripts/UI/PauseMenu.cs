using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject panel;
    public GameObject UICanvas;
    public GameObject optionsMenu;
    
    public GameObject backButton;
    public GameObject optionsButton;
    public GameObject resumeButton;

 

    private Vector3 backButt;
    private Vector3 optionsButt;
    private Vector3 resumeButt;
    

    // Update is called once per frame

    void Start(){

        backButt = backButton.transform.localScale;
        optionsButt = optionsButton.transform.localScale;
        resumeButt = resumeButton.transform.localScale;

    }

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


        optionsButton.transform.localScale = optionsButt;
        backButton.transform.localScale = backButt;
        resumeButton.transform.localScale = resumeButt;

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        panel.SetActive(false);
        UICanvas.SetActive(true);
        gamePaused = false;
        Time.timeScale = 1f;
        backButton.SetActive(false); 
     

        
    }

    public void Back(){

            Pause(); 
    }
    void Pause() {

        optionsButton.transform.localScale = optionsButt;
        backButton.transform.localScale = backButt;
        resumeButton.transform.localScale = resumeButt;


        backButton.SetActive(false);
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        panel.SetActive(true);
        UICanvas.SetActive(false);
        gamePaused = true;
        Time.timeScale = 0f;

        
        

    }

    public void toMainMenu() {

        optionsButton.transform.localScale = optionsButt;
        backButton.transform.localScale = backButt;
        resumeButton.transform.localScale = resumeButt;


        gamePaused = false;
        pauseMenu.SetActive(false);
        panel.SetActive(false);
        optionsMenu.SetActive(false);
        UICanvas.SetActive(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene("NewMainMenu");
        backButton.SetActive(false);
      
    }

    public void ToggleOptions() {

        optionsButton.transform.localScale = optionsButt;
        backButton.transform.localScale = backButt;
        resumeButton.transform.localScale = resumeButt;


        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        panel.SetActive(true);
        backButton.SetActive(true);
      
    }

    public void Quit()
    {
        Application.Quit();
    }
}
