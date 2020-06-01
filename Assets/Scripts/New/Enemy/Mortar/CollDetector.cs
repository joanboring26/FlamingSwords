using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollDetector : MonoBehaviour
{
    public float shotDelTime;
    public GameObject gameBase;
    public GameObject expl;
    public GameObject chargeAnim;
    public ParticleSystem cannonFirePart;
    public Transform cannonVis;
    public float rotationSpeed;

    bool detecting = false;

    float prevTime = 0;
    public AudioSource mortarSrc;
    public AudioClip[] fireSnds;
    public AudioClip[] approachSnds;
    public AudioSource impactSrc;
    public AudioClip[] impactSound;

    private Quaternion newRotation;
    private Vector3 dir;
    private float angle = 0;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            dir = collision.transform.position - transform.position;
            angle = Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg + 90;
            newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);

            cannonVis.rotation = Quaternion.Euler(0, 180, Quaternion.Slerp(cannonVis.rotation, newRotation, Time.deltaTime * rotationSpeed).eulerAngles.z);

            if (Time.time > prevTime)
            {
                chargeAnim.SetActive(false);
                chargeAnim.SetActive(true);
                prevTime = Time.time + shotDelTime;

                mortarSrc.PlayOneShot(fireSnds[Random.Range(0, fireSnds.Length)]);
                GameObject tmpRef = Instantiate(expl, collision.transform.position, collision.transform.rotation);
                tmpRef.GetComponent<MortarPrj>().approachSnd.clip = approachSnds[Random.Range(0,approachSnds.Length)];
                tmpRef.GetComponent<MortarPrj>().approachSnd.Play();
                cannonFirePart.Play();
            }
        }
    }

    private void hitNPC(float givFloat)
    {
        mortarSrc.PlayOneShot(impactSound[Random.Range(0, impactSound.Length)]);
    }

    public void EnemyDead(float hp)
    {
        Destroy(gameBase);
    }
}
