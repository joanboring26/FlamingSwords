using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFade : MonoBehaviour
{
    public Image render;
    public IEnumerator fadeTimer(float fadeTime)
    {
        render.color = new Color(render.color.r, render.color.g, render.color.b, 255);

        float fadeDurationInSeconds = fadeTime;
        float timeout = 0.05f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 1; f >= 0; f -= fadeAmount)
        {
            Color c = render.color;
            c.a = f;
            render.color = c;
            yield return new WaitForSeconds(timeout);
        }

        render.color = new Color(render.color.r, render.color.g, render.color.b, 0);
    }
}
