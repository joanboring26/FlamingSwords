using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayLvl1()
    {
        SceneManager.LoadScene("level_1");
    }
    public void PlayLvl2()
    {
        SceneManager.LoadScene("level_2");
    }
    public void PlayLvl3()
    {
        SceneManager.LoadScene("level_3");
    }

    public void PlayLvl4()
    {
        SceneManager.LoadScene("level_4");
    }
    public void PlayLvl5()
    {
        SceneManager.LoadScene("level_5");
    }
    public void PlayLvl6()
    {
        SceneManager.LoadScene("level_6");
    }
    public void PlayCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void PlayTuto()
    {
        SceneManager.LoadScene("2d Tutorial");
    }
    public void PlayMainMenu()
    {
        SceneManager.LoadScene("NewMainMenu");
    }

    public void QuitGame()
    {
        
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("IntroBackground");
        }
    }

}
