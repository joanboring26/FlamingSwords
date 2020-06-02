using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleStop : MonoBehaviour
{
    public GameObject robot;
    public GameObject point;
    public ParticleSystem particle;


    // Update is called once per frame
    void Update()
    {
        if (robot.transform.position == point.transform.position)
        particle.Stop();
    }
}
