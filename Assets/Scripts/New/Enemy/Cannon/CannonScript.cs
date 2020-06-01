using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public AudioSource hitSrc;
    public AudioClip[] hitSnds;
    public AudioSource sndSrc;
    public AudioClip[] fireSnds;
    

    public float fireDelay;
    public GameObject projectile;
    public Transform muzzleFire;
    public Collider2D thisColl;
    public ParticleSystem partSys;

    public float initDelay;
    float prevTime;

    private void Start()
    {
        prevTime = initDelay + Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if( Time.time > prevTime + fireDelay )
        {
            prevTime = Time.time + fireDelay;
            CannonProj tmp = Instantiate(projectile, muzzleFire.position, muzzleFire.rotation).GetComponent<CannonProj>();
            tmp.alliedColl = thisColl;
            sndSrc.PlayOneShot(fireSnds[Random.Range(0, fireSnds.Length)]);
            partSys.Play();
        }
    }

    public void hitNPC(float val)
    {
        sndSrc.PlayOneShot(hitSnds[Random.Range(0, hitSnds.Length)]);
    }
}
