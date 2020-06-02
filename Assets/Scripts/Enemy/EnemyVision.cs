using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public LayerMask enemyVisionLayers;

    public EnemyMov movScript;

    public GameObject alertPropulsor;
    public GameObject idlePropulsor;

    public float rotationSpeed;
    public bool lookAtTarget;
    bool detectedTarget;
    
    public float checkDelay;
    float nextCheck = 0;

    public Transform detectedTransform;
    public Transform npcTransform;

    public AudioClip[] alert;
    public AudioClip[] impactSound;
    public AudioSource sndSrc;

    private Quaternion newRotation;
    private Vector3 dir;
    private float angle = 0;

    private IEnumerator coroutineEnum;

    private void Update()
    {
        if(detectedTarget && lookAtTarget && (detectedTransform != null))
        {
            dir = detectedTransform.position - transform.position;
            angle = Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg - 90;
            newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);

            npcTransform.rotation = Quaternion.Euler(0, 180, Quaternion.Slerp(npcTransform.rotation, newRotation, Time.deltaTime * rotationSpeed).eulerAngles.z);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        detectedTransform = other.transform;
        if (Time.time > nextCheck)
        {

            nextCheck = Time.time + checkDelay;
            if (CheckRay())
            {
                if (movScript.nav.velocity == Vector3.zero)
                {
                    if(coroutineEnum != null)
                    {
                        StopCoroutine(coroutineEnum);
                    }
                    sndSrc.enabled = true;
                    sndSrc.PlayOneShot(alert[Random.Range(0, alert.Length)]);
                    coroutineEnum = DisableTimer();
                    StartCoroutine(coroutineEnum);
                }
                movScript.MoveToDestination(detectedTransform.position);
                detectedTarget = true;
            }
            else
            {
                detectedTarget = false;
            }

        }
    }

    private void hitNPC(float givFloat)
    {
        if (coroutineEnum != null)
        {
            StopCoroutine(coroutineEnum);
        }
        sndSrc.enabled = true;
        sndSrc.PlayOneShot(impactSound[Random.Range(0, impactSound.Length)]);
        coroutineEnum = DisableTimer();
        StartCoroutine(coroutineEnum);
    }

    public bool CheckRay()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(transform.position, detectedTransform.position - transform.position, 30f, enemyVisionLayers);
        Debug.DrawLine(hitInfo.point, transform.position, Color.red, 0.5f);
        if (hitInfo.collider.tag == "Player")
        {
            return true;
        }
        return false;
    }

    IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(3f);
        sndSrc.enabled = false;
    }

}