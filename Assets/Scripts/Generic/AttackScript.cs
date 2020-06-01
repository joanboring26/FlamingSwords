using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject attackBase;
    public BoxCollider attackBox;

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
                other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(new Vector3(transform.position.x - other.transform.position.x, 0, transform.position.z - other.transform.position.z)) * hitPushForce, ForceMode.Impulse);
                Debug.Log("HITSOMETHING");
                other.gameObject.SendMessage("ModHealth", attackDmg);
                if (attackBase != null)
                {
                    attackBase.BroadcastMessage("targetHit");
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
        for(int i = 0; i < maxHits; i++)
        {
            hitEnts[i] = 0;
        }
    }
}
