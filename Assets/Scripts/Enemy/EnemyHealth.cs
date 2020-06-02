using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp;
    float maxHp;
    public GameObject deadSprite;

    protected int counter = 0;
    public float nextDamageDelay;

    //Script will broadcast this message to the thing it is attached to when it dies
    public string deathMessage;
    public GameObject hitMessageTarget;
    public string hitMessage;
    public GameObject explosionRef;
    public Rigidbody2D rig;

    [Header("Damaged visuals")]
    public MeshRenderer meshRenderer;
    public Material[] damagedSprites;
    public GameObject damageChunks;
    public GameObject deathChunks;
    int totalStates;
    int currState;

    public float nextDamage = 0;

    private void Start()
    {
        maxHp = hp;
        totalStates = damagedSprites.Length;
        currState = totalStates;
        totalStates--;
    }

    public virtual void ModHealth(float givVal, Transform attackDir)
    {
        if (Time.time > nextDamage)
        {
            nextDamage = Time.time + nextDamageDelay;
            hp += givVal;
            currState = Mathf.RoundToInt((hp / maxHp) * totalStates);

            Instantiate(explosionRef, transform.position, attackDir.rotation * Quaternion.Euler(0,0,-120 + Random.Range(-20,20)) );
            if (hitMessageTarget != null)
            {
                hitMessageTarget.SendMessage(hitMessage, 0.3f);
            }

            if (hp <= 0)
            {
                //if (deadSprite != null)
                //{
                //    Vector3 dir = attackDir.position - transform.position;
                //    Quaternion newRotation = Quaternion.AngleAxis(Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg, Vector3.forward);
                //    newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);
                //}

                Instantiate(deadSprite, transform.position, transform.rotation);
                Instantiate(deathChunks, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                gameObject.SendMessage(deathMessage, hp);
                
            }
            else
            {
                Vector3 dir = attackDir.position - transform.position;
                Quaternion newRotation = Quaternion.AngleAxis(Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg, Vector3.forward);
                newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);
                meshRenderer.material = damagedSprites[currState];
                Instantiate(damageChunks, new Vector3(transform.position.x, transform.position.y, transform.position.z), newRotation);
            }

        }
    }


    public void EnemyDead(float hp)
    {
        Destroy(gameObject);
    }
}
