using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AttackTuto : MonoBehaviour
{
    public bool dontCheck;
    public GameObject attack;
    public GameObject marker;
    public GameObject gateBlocker;
    public GameObject tip;
    public Timescale time;

    public PostProcessVolume postProcProf;
    public PostProcessProfile profile;

    private bool secondStage = false;

    GameObject test;
    private void Start()
    {
        test = marker;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dontCheck)
        {
            if (collision.tag == "Parry")
            {
                LensDistortion lens;
                test.SetActive(true);
                profile = postProcProf.sharedProfile;
                profile.TryGetSettings<LensDistortion>(out lens);
                lens.intensity.Override(22f);
                StartCoroutine(pauseDelay());
            }
        }
    }

    void Update()
    {
        if(secondStage)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                time.Resume();
                LensDistortion lens;
                profile = postProcProf.sharedProfile;
                profile.TryGetSettings<LensDistortion>(out lens);
                lens.intensity.Override(22f);
            }
        }
    }

    public void EnemyDead()
    {
        test.SetActive(true);
        tip.SetActive(true);
        Destroy(attack);
        Destroy(gateBlocker);
    }

    IEnumerator pauseDelay()
    {
        yield return new WaitForSeconds(1f);
        time.Stop();
        attack.SetActive(true);
        LensDistortion lens;
        profile = postProcProf.sharedProfile;
        profile.TryGetSettings<LensDistortion>(out lens);
        lens.intensity.Override(50f);
        secondStage = true;
    }
}
