using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MoveTutorial : MonoBehaviour
{
    public Timescale time;

    public GameObject panel;

    public PostProcessVolume postProcProf;
    public PostProcessProfile profile;
  
    void Start()
    {
        time.Stop();
        panel.SetActive(true);
        StartCoroutine(resTime());
        LensDistortion lens;
        profile = postProcProf.sharedProfile;
        profile.TryGetSettings<LensDistortion>(out lens);
        lens.intensity.Override(50f);
        
        //var bloom = postProcProf.;
        //bloom.bloom.intensity = Mathf.Lerp(data[i].Strength, data[i + 1].Strength, data[i].TimeToReachNext);
        //postProcProf.bloom.settings = bloom;
    }


    void Update()
    {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            time.Resume();
            panel.SetActive(false);
            LensDistortion lens;
            profile = postProcProf.sharedProfile;
            profile.TryGetSettings<LensDistortion>(out lens);
            lens.intensity.Override(22f);
            Destroy(gameObject);
        }
       
    }

    IEnumerator resTime()
    {
        yield return new WaitForSecondsRealtime(2f);
        time.Resume();
    }
}
