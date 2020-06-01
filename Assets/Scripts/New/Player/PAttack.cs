using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttack : MonoBehaviour
{
    public EntityHealth playerStats;
    public GameObject attackBase;
    public BoxCollider2D attackBox;

    public AudioSource swingSrc;

    public float activeAttackColliderTime;
    public float attackDmg;
    public float hitPushForce;

    private static int maxHits = 5;

    private int[] hitEnts = new int[maxHits];
    private int cHits = 0;
    private bool dontCheck = false;



    private void Start()
    {
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
            //other.gameObject.GetComponent<EnemyHealth>();
            other.GetComponent<EnemyHealth>().rig.AddForce(Vector3.Normalize(new Vector3(transform.position.x - other.transform.position.x, transform.position.y - other.transform.position.y)) * hitPushForce, ForceMode2D.Impulse);
            other.GetComponent<EnemyHealth>().ModHealth(attackDmg, playerStats.playerRot);
            if (attackBase != null)
            {
                playerStats.RecoverPrevHealth((int)attackDmg);
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
        swingSrc.Play();
        attackBox.enabled = true;
        yield return new WaitForSeconds(activeAttackColliderTime);
        attackBox.enabled = false;
        for (int i = 0; i < maxHits; i++)
        {
            hitEnts[i] = 0;
        }
    }
}
