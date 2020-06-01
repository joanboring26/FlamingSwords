using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioFader
{
    public static IEnumerator FadeOutAudio(AudioSource src, float fadeTime)
    {
        
        float fadeDurationInSeconds = fadeTime - 0.7f;
        float timeout = 0;
        float volumeStart = src.volume;

        for (float f = volumeStart; f >= timeout; f -= volumeStart * Time.deltaTime / fadeTime)
        {
            src.volume = f;
            yield return new WaitForSeconds(timeout);
        }
    }
    public static IEnumerator FadeInAudio(AudioSource src, float fadeTime, float fadeTo)
    {
        src.Play();
        float fadeDurationInSeconds = fadeTime - 0.7f;
        float timeout = 0.01f;
        float volumeStart = src.volume;

        for (float f = volumeStart; f < fadeTo; f += fadeTo * Time.deltaTime / fadeTime)
        {
            src.volume = f;
            yield return new WaitForSeconds(timeout);
        }
    }
}
