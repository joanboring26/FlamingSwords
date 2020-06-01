using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossTimer : MonoBehaviour
{
    public Image crossFiller;

    public IEnumerator fadeTimer(float fadeTime)
    {
        crossFiller.fillAmount = 0;
        float fadeDurationInSeconds = fadeTime;
        float timeout = 0.01f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 0; f <= 1; f += fadeAmount)
        {
            crossFiller.fillAmount = f;
            yield return new WaitForSeconds(timeout);
        }

        crossFiller.fillAmount = 1;
    }
}
