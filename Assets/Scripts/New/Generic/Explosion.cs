using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public LayerMask explMask;

    public float radius;

    public float damage;
    public float damageFalloff;

    public float force;

    public float objDestroyTime;

    public bool blowUpOnAppear;
    public bool damageEnemies;

    public AudioClip[] explosionSnds;
    public AudioSource explosionSrc;

    // Start is called before the first frame update
    void Start()
    {
        if(blowUpOnAppear)
        {
            blowUp();
        }
    }

    public void blowUp()
    {
        StartCoroutine(explStart());
        explosionSrc.PlayOneShot(explosionSnds[Random.Range(0, explosionSnds.Length)]);
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radius, explMask);
        for(int i = 0; i < targets.Length; i++)
        {
            switch(targets[i].tag)
            {
                case "Player":
                    Rigidbody2D temp = targets[i].GetComponent<Rigidbody2D>();
                    Vector2 otherPos = targets[i].transform.position;
                    otherPos.x = (otherPos.x - transform.position.x);
                    otherPos.y = (otherPos.y - transform.position.y);
                    temp.AddForce(otherPos * force, ForceMode2D.Impulse);

                    //Le aplica caida de daño dependiendo de la distancia de la explosion
                    float tDamage1 = Mathf.Clamp(-damage - (otherPos.magnitude / damageFalloff), 0, -damage);
                    targets[i].GetComponent<EntityHealth>().ModHealth(-tDamage1);
                    break;

                case "NPC":
                    if (damageEnemies)
                    {
                        Rigidbody2D tmp = targets[i].GetComponent<EnemyHealth>().rig;
                        Vector2 othPos = targets[i].transform.position;
                        otherPos.x = (othPos.x - transform.position.x);
                        otherPos.y = (othPos.y - transform.position.y);

                        if (tmp != null)
                        {
                            tmp.AddForce(otherPos * force, ForceMode2D.Impulse);
                        }

                        //Le aplica caida de daño dependiendo de la distancia de la explosion
                        float tDamage2 = Mathf.Clamp(-damage * 2f - (otherPos.magnitude / damageFalloff), 0, -damage * 2f);
                        targets[i].GetComponent<EnemyHealth>().ModHealth(-tDamage2, transform);
                    }
                    break;

                default:

                    break;
            }
        }

    }


    public IEnumerator explStart()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
