﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamFollower : MonoBehaviour
{
    public float followVal;
    public Transform followPos;
    public Transform currPos;

    public PostProcessVolume pp;
    private Grain grainInt;

    // Start is called before the first frame update
    void Start()
    {
        currPos.parent = null;
        pp.profile.TryGetSettings<Grain>(out grainInt);
        grainInt.intensity.Override(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currPos != null)
        {
            currPos.position = new Vector3(
                Mathf.SmoothStep(currPos.position.x, followPos.position.x, followVal * Time.deltaTime),
                Mathf.SmoothStep(currPos.position.y, followPos.position.y, followVal * Time.deltaTime),
                Mathf.SmoothStep(currPos.position.z, followPos.position.z, followVal * Time.deltaTime));
        }
    }
}
