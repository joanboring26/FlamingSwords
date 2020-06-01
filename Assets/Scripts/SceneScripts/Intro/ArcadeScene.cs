using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArcadeScene : MonoBehaviour
{
    bool P1 = false;
    bool P2 = false;
    bool P3 = false;
    bool P4 = false;
    bool dialogue1Done = false;
    bool dialogue2Done = false;
    bool dialogue3Done = false;
    bool dialogue4Done = false;

    public AudioClip wheelsGravel;
    public AudioClip wheelsMoving;
    public AudioClip wheelsStopping;
    public AudioClip standUp;
    public AudioClip standDown;
    public AudioClip scan;
    public AudioClip thump;
    public AudioClip struggling;
    public AudioClip pickCabin;
    public AudioClip bootUp;

    public AudioSource audioSrc;
    public AudioSource boxSrc;
    public AudioSource bootSrc;

    public Image fadeImage;
    public GameObject dialogue1;
    public Animator animWheels;
    public Animator animTorso;

    public Animator animCab;
    public Animator animB0x;
    public Dialogue dialogue1Script;
    public CamPointMover bl4ckMover;
    public CamPointMover camMover;
    public GameObject struggleState;
    public GameObject normalState;

    public string nextLevel;

    private void Start()
    {
        StartCoroutine(Active());
    }

    void ArriveP1()
    {
        P1 = true;
    }
    void ArriveP2()
    {
        P2 = true;
    }
    void ArriveP3()
    {
        P3 = true;
    }

    void ArriveP4()
    {
        P4 = true;
    }

    void FinishDialogue1()
    {
        dialogue1Done = true;
    }
    void FinishDialogue2()
    {
        dialogue2Done = true;
    }
    void FinishDialogue3()
    {
        dialogue3Done = true;
    }
    void FinishDialogue4()
    {
        dialogue4Done = true;
    }
    IEnumerator Active()
    {
        audioSrc.PlayOneShot(wheelsMoving);
        audioSrc.PlayOneShot(wheelsGravel);

        animWheels.SetBool("Moving",true);
        bl4ckMover.move = true;
        while (!P1)
        {
            yield return new WaitForEndOfFrame();
        }

        audioSrc.Stop();
        audioSrc.PlayOneShot(wheelsStopping);

        animWheels.SetBool("Moving", false);
        bl4ckMover.move = false;

        yield return new WaitForSeconds(1);

        dialogue1.SetActive(true);
        dialogue1Script.talking = true;


        while (!dialogue1Done)
        {
            yield return new WaitForEndOfFrame();
        }

        dialogue1.SetActive(false);
        dialogue1Script.talking = false;

        audioSrc.PlayOneShot(wheelsMoving);
        audioSrc.PlayOneShot(wheelsGravel);
        animWheels.SetBool("Moving", true);

        bl4ckMover.move = true;
        camMover.move = true;


        while (!P2)
        {
            yield return new WaitForEndOfFrame();
        }
        camMover.move = false;

        while (!P3)
        {
            yield return new WaitForEndOfFrame();
        }

        audioSrc.Stop();
        audioSrc.PlayOneShot(wheelsStopping);
        audioSrc.PlayOneShot(standUp);
        bl4ckMover.move = false;
        animWheels.SetBool("Moving", false);
        animTorso.SetBool("Standing", true);
        yield return new WaitForSeconds(0.666f);
        audioSrc.PlayOneShot(scan);
        animTorso.SetBool("Scanning", true);
        yield return new WaitForSeconds(1.666f);
        animTorso.SetBool("Scanning", false);
        //animTorso.SetFloat("ScanSpeed", 0f);



        dialogue1.SetActive(true);
        dialogue1Script.talking = true;
        while (!dialogue2Done)
        {
            yield return new WaitForEndOfFrame();
        }
        dialogue1.SetActive(false);
        dialogue1Script.talking = false;
        boxSrc.PlayOneShot(thump);

        yield return new WaitForSeconds(1.5f);

        audioSrc.PlayOneShot(standDown);
        animTorso.SetBool("Standing", false);

        dialogue1.SetActive(true);
        dialogue1Script.talking = true;
        while (!dialogue3Done)
        {
            yield return new WaitForEndOfFrame();
        }
        dialogue1.SetActive(false);
        dialogue1Script.talking = false;
        bl4ckMover.move = true;
        camMover.move = true;
        audioSrc.PlayOneShot(wheelsMoving);
        audioSrc.PlayOneShot(wheelsGravel);
        animWheels.SetBool("Moving", true);

        P3 = false;
        while (!P3)
        {
            yield return new WaitForEndOfFrame();
        }
        animWheels.SetBool("Moving", false);

        while (!P4)
        {
            yield return new WaitForEndOfFrame();
        }
        P4 = false;

        animWheels.SetBool("Moving", false);
        dialogue1.SetActive(true);
        dialogue1Script.talking = true;

        while (!dialogue4Done)
        {
            yield return new WaitForEndOfFrame();
        }
        dialogue1.SetActive(false);
        dialogue1Script.talking = false;
        animCab.SetBool("Struggling", true);
        animB0x.SetBool("Struggling", true);
        boxSrc.PlayOneShot(struggling);
        yield return new WaitForSeconds(1f);
        animCab.SetBool("Struggling", false);
        animB0x.SetBool("Struggling", false);
        dialogue1.SetActive(true);
        dialogue1Script.talking = true;

        while (!P4)
        {
            yield return new WaitForEndOfFrame();
        }
        P4 = false;
        dialogue1.SetActive(false);
        dialogue1Script.talking = false;

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        float fadeDurationInSeconds = 2f - 0.7f;
        float timeout = 0.01f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 0; f <= 1; f += fadeAmount)
        {
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return new WaitForSeconds(timeout);
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 255);
        struggleState.SetActive(false);
        normalState.SetActive(true);
        boxSrc.PlayOneShot(pickCabin);
        yield return new WaitForSeconds(1f);
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 255);
        fadeDurationInSeconds = 1.2f - 0.7f;
        timeout = 0.01f;
        fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 1; f >= 0; f -= fadeAmount)
        {
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return new WaitForSeconds(timeout);
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);

        dialogue1.SetActive(true);
        dialogue1Script.talking = true;

        while (!P4)
        {
            yield return new WaitForEndOfFrame();
        }
        P4 = false;

        bootSrc.PlayOneShot(bootUp);

        while (!P4)
        {
            yield return new WaitForEndOfFrame();
        }
        P4 = false;

        dialogue1.SetActive(false);
        dialogue1Script.talking = false;

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        fadeDurationInSeconds = 3f - 0.7f;
        timeout = 0.01f;
        fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 0; f <= 1; f += fadeAmount)
        {
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return new WaitForSeconds(timeout);
        }

        SceneManager.LoadScene(nextLevel);


    }
}
