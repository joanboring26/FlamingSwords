using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunIndicator : MonoBehaviour
{
    public SpriteRenderer render;

    private Sprite spr;
    Color finalColor;
    public int fadeStepChange;

  

    private void Start()
    {
        spr = render.sprite;
        finalColor = new Color( render.color.r, render.color.g, render.color.b, 0);
        render.color = finalColor;

    }
    public IEnumerator stunTimer(float stunTime)
    {
        render.color = new Color(render.color.r, render.color.g, render.color.b, 255);
        float fadeDurationInSeconds = stunTime;
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
