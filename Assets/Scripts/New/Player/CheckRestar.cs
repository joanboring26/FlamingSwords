using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckRestar : MonoBehaviour
{
    public MusicScene musicHolder;

    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            musicHolder.setTimeStamp();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
