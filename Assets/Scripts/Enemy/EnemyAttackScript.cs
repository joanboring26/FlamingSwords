using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public SoldierAttacks behaviourBase;
    public EnemyVision visionBase;
    public float activeAttackColliderTime;
    public float attackDmg;
    public float hitPushForce;
    public GameObject parryRef;
    public BoxCollider2D attackBox;

    public AudioClip[] playerImpactSnd;
    public AudioClip[] parryImpactSnd;
    public AudioSource sndSrc;

    public bool parried = false;

    private static int maxHits = 5;

    private int[] hitEnts = new int[maxHits];
    private int cHits = 0;
    private bool dontCheck = false;

   

    private void Start()
    {
        attackBox = GetComponent<BoxCollider2D>();
        attackBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
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
            if (other.GetComponent<Rigidbody>())
            {
                other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(new Vector3(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y, 0)) * hitPushForce, ForceMode.Impulse);
            }
            if (other.gameObject.tag == "Parry")
            {

                sndSrc.PlayOneShot(parryImpactSnd[Random.Range(0, parryImpactSnd.Length)]);
                StartCoroutine(behaviourBase.stunned(other.transform.position));
                other.GetComponent<AttackBase>().hitByEnemy();
                attackBox.enabled = false;
                Instantiate(parryRef, transform.position, transform.rotation);
            }
            else
            {
                if (visionBase.CheckRay())
                {
                    sndSrc.PlayOneShot(playerImpactSnd[Random.Range(0,playerImpactSnd.Length)]);
                    attackBox.enabled = false;
                    other.gameObject.GetComponent<EntityHealth>().ModHealth(attackDmg, transform.up* 2);
                }
            }
        }
        else
        {
            dontCheck = false;
        }
        cHits = (cHits + 1) % maxHits;
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
