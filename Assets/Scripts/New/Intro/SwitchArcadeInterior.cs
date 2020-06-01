using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchArcadeInterior : MonoBehaviour
{
    public AudioSource musicSrc;
    public AudioSource windSrc;
    public AudioSource interiorSrc;
    public float fadeInTime;
    public float fadeOutTime;
    public Image render;

    public float delayBeforeScene2Audio;
    public float delayBeforeScene2;

    public GameObject scene1;
    public GameObject scene2;

    void Activate()
    {
        StartCoroutine(fadeTimer(fadeInTime));
    }

    public IEnumerator fadeTimer(float fadeTime)
    {
        StartCoroutine(AudioFader.FadeOutAudio(musicSrc, 7));

        render.color = new Color(render.color.r, render.color.g, render.color.b, 0);
        float fadeDurationInSeconds = 5 - 0.7f;
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

        scene1.SetActive(false);
        scene2.SetActive(true);
        
        yield return new WaitForSeconds(delayBeforeScene2Audio);

        AudioFader.FadeInAudio(interiorSrc, 3, 0.456f);
        AudioFader.FadeOutAudio(windSrc, 2);

        yield return new WaitForSeconds(delayBeforeScene2);

        fadeDurationInSeconds = fadeOutTime - 0.7f;
        fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 1; f > 0; f -= fadeAmount)
        {
            Color c = render.color;
            c.a = f;
            render.color = c;
            yield return new WaitForSeconds(timeout);
        }


    }
}
