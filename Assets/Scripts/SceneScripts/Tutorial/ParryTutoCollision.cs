using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ParryTutoCollision : MonoBehaviour
{
    public Timescale time;

    public GameObject tutorial;

    public PostProcessVolume postProcProf;
    public PostProcessProfile profile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        time.Stop();
        tutorial.SetActive(true);
        LensDistortion lens;
        profile = postProcProf.sharedProfile;
        profile.TryGetSettings<LensDistortion>(out lens);
        lens.intensity.Override(50f);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && tutorial.activeSelf)
        {
            time.Resume();
            tutorial.SetActive(false);
            LensDistortion lens;
            profile = postProcProf.sharedProfile;
            profile.TryGetSettings<LensDistortion>(out lens);
            lens.intensity.Override(22f);
            Destroy(gameObject);
        }

    }

}
