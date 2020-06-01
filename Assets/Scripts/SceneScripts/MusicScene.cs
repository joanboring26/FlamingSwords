using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScene : MonoBehaviour
{
    public AudioSource musicSrc;

    public static string prevScene = "none";
    public static float timeStamp = 0;
    public float startTime = 0;

    private void Start()
    {
        if(prevScene != SceneManager.GetActiveScene().name)
        {
            prevScene = SceneManager.GetActiveScene().name;
            timeStamp = startTime;
        }
        loadMusic();
    }
    public void setTimeStamp()
    {
        timeStamp = musicSrc.time;
    }

    public void loadMusic()
    {
        musicSrc.time = timeStamp;
    }
}
