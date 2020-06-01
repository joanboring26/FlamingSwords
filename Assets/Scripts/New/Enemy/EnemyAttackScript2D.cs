using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript2D : MonoBehaviour
{
    public SoldierAttacks behaviourBase;
    public EnemyVision visionBase;
    public float activeAttackColliderTime;
    public float attackDmg;
    public float hitPushForce;
    public GameObject parryRef;
    public BoxCollider attackBox;

    public AudioClip[] playerImpactSnd;
    public AudioClip[] parryImpactSnd;
    public AudioSource sndSrc;

    private static int maxHits = 5;

    private int[] hitEnts = new int[maxHits];
    private int cHits = 0;
    private bool dontCheck = false;



    private void Start()
    {
        attackBox = GetComponent<BoxCollider>();
        attackBox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {


        hitEnts[cHits] = other.GetInstanceID();

        for (int i = cHits; i > 0; i--)
        {
            if (hitEnts[i] == hitEnts[cHits] && i != cHits)
            {
                dontCheck = true;
            }
        }
        if (!dontCheck)
        {
            if (other.GetComponent<Rigidbody2D>())
            {
                //other.GetComponent<Rigidbody2D>().AddForce(Vector2(new Vector2(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y).Normalize()) * hitPushForce, ForceMode.Impulse);
            }
            Debug.Log("HITSOMETHING");
            if (other.gameObject.tag == "Parry")
            {

                //sndSrc.PlayOneShot(parryImpactSnd[Random.Range(0, parryImpactSnd.Length)]);
                StartCoroutine(behaviourBase.stunned(other.transform.position));
                Instantiate(parryRef, transform.position, transform.rotation);
            }
            else
            {
                if (visionBase.CheckRay())
                {
                    sndSrc.PlayOneShot(playerImpactSnd[/*Random.Range(0,playerImpactSnd.Length)*/0]);
                    other.gameObject.SendMessage("ModHealth", attackDmg);
                }
            }
        }
        else
        {
            dontCheck = false;
        }
        cHits = (cHits + 1) % maxHits;
    }

    private float Vector2(object v)
    {
        throw new NotImplementedException();
    }

    public IEnumerator attack()
    {
        attackBox.enabled = true;
        yield return new WaitForSeconds(activeAttackColliderTime);
        attackBox.enabled = false;
        for (int i = 0; i < maxHits; i++)
        {
            hitEnts[i] = 0;
        }
    }
}
