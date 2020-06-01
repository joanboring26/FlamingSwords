using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointSys : MonoBehaviour
{
    public static string prevScene = "none";
    public static Vector3 spawnPos;
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (prevScene != SceneManager.GetActiveScene().name)
        {
            prevScene = SceneManager.GetActiveScene().name;
            setSpawnPos(playerTransform.position);
        }
        setPlayer();
    }

    public void setPlayer()
    {
        playerTransform.position = spawnPos;
    }

    public static void setSpawnPos(Vector3 gSpawnPos)
    {
        spawnPos = gSpawnPos;
    }
}
