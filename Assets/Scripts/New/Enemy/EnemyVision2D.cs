using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision2D : MonoBehaviour
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

    private void Update()
    {
        if(detectedTarget && lookAtTarget)
        {
            var newRotation = Quaternion.LookRotation( detectedTransform.position - npcTransform.position).eulerAngles;
            newRotation.x = 0;
            newRotation.z = 0;
            npcTransform.rotation = Quaternion.Slerp(npcTransform.rotation, Quaternion.Euler(newRotation), Time.deltaTime * rotationSpeed);
            alertPropulsor.SetActive(true);
            idlePropulsor.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        detectedTransform = other.transform;
        if(Time.time > nextCheck)
        {

            nextCheck = Time.time + checkDelay;
            if(CheckRay())
            {
                if (movScript.nav.velocity == Vector3.zero)
                {
                    sndSrc.PlayOneShot(alert[Random.Range(0, alert.Length)]);
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
        sndSrc.PlayOneShot(impactSound[Random.Range(0, impactSound.Length)]);
    }

    public bool CheckRay()
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(transform.position, detectedTransform.position - transform.position/*, out hitInfo, Mathf.Infinity, enemyVisionLayers*/);
        //Debug.DrawRay(transform.position, detectedTransform.position - transform.position, Color.red, LayerMask.GetMask("Player"));
        if (hitInfo.collider.tag == "Player")
        {
            return true;
        }
        return false;
    }



}