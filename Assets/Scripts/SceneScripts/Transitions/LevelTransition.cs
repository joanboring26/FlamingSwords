using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextLevel;
    public float fadeTime;
    public GameObject doorToClose;
    public Image render;
    public int LevelIndex;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(fadeTimer(fadeTime));
    }

    public IEnumerator fadeTimer(float fadeTime)
    {
        GetComponent<AudioSource>().Play();
        doorToClose.SetActive(true);
        render.color = new Color(render.color.r, render.color.g, render.color.b, 0);
        float fadeDurationInSeconds = fadeTime - 0.7f;
        float timeout = 0.01f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);
        
        for (float f = 0; f <= 1; f += fadeAmount)
        {
            Color c = render.color;
            c.a = f;
            render.color = c;
            yield return new WaitForSeconds(timeout);
        }

        render.color = new Color(render.color.r, render.color.g, render.color.b, 255);

        int CurrentLevel = PlayerPrefs.GetInt("Levels", 1);
        if ((CurrentLevel >> (LevelIndex + 1)) % 2 == 0)
        {

            CurrentLevel += (int)Mathf.Pow(2f, LevelIndex + 1);
            PlayerPrefs.SetInt("Levels", CurrentLevel);

        }

        SceneManager.LoadScene(nextLevel);
    }

}
