using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroSound : MonoBehaviour
{
    public AudioSource lineSource;

    public float initialDelay;
    public float finalDelay;

    public GameObject textLine1;
    public float displayLine1;
    public AudioClip line1;

    public GameObject textLine2;
    public float displayLine2;
    public AudioClip line2;

    public GameObject textLine3;
    public float displayLine3;
    public AudioClip line3;

    public GameObject textLine4;
    public float displayLine4;
    public AudioClip line4;

    private void Start()
    {
        StartCoroutine(intro());
    }

    // Start is called before the first frame update
    IEnumerator intro()
    {
        yield return new WaitForSeconds(initialDelay);

        lineSource.PlayOneShot(line1);
        textLine1.SetActive(true);
        yield return new WaitForSeconds(displayLine1);
        textLine1.SetActive(false);

        lineSource.PlayOneShot(line2);
        textLine2.SetActive(true);
        yield return new WaitForSeconds(displayLine2);
        textLine2.SetActive(false);

        lineSource.PlayOneShot(line3);
        textLine3.SetActive(true);
        yield return new WaitForSeconds(displayLine3);
        textLine3.SetActive(false);

        lineSource.PlayOneShot(line4);
        textLine4.SetActive(true);
        yield return new WaitForSeconds(displayLine4);
        textLine4.SetActive(false);

        yield return new WaitForSeconds(finalDelay);

        SceneManager.LoadScene(1);

    }
}
