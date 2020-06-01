using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : AttackBase
{
    [Header("Sword vars")]
    [Header("Object and component references")]
    public GameObject attackVisual;
    public BoxCollider2D attackBox;
    public AudioSource swingSrc;
    public AudioClip[] swingSnds;

    [Header("Attack values")]
    public float fireRate;
    public float activeAttackColliderTime;
    public float attackDmg;
    public int staminaUse;
    public float hitPushForce;

    private static int maxHits = 5;

    private int[] hitEnts = new int[maxHits];
    private int cHits = 0;
    private bool dontCheck = false;

    //Used for fireRate timer!
    private float prevTime = 0;
    private bool hitP = false;


    private void Start()
    {
        attackBox.enabled = false;
    }

    //Activated when the attack box collider touches something
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
            playerStats.RecoverPrevHealth((int)attackDmg);

        }
        else
        {
            dontCheck = false;
        }
        cHits = (cHits + 1) % maxHits;
    }
    
    public override void attack()
    {
        if (prevTime + fireRate < Time.time)
        {
            if (playerStats.stamina > staminaUse && !attackBox.enabled)
            {
                StartCoroutine(playerStats.crossFiller.fadeTimer(fireRate + 0.1f));
                playerStats.camShaker.AddCustomShake(transform.right * 3, CameraShake.ShakeType.SWORDSWING);
                hitP = false;
                attackVisual.transform.localScale = new Vector3(-attackVisual.transform.localScale.x, attackVisual.transform.localScale.y, 1);
                playerStats.ModStamina(-staminaUse);
                prevTime = Time.time + fireRate;
                StartCoroutine(initAttack());
            }
        }
    }

    public IEnumerator initAttack()
    {
        swingSrc.PlayOneShot(swingSnds[Random.Range(0, swingSnds.Length)]);
        attackVisual.SetActive(true);
        attackBox.enabled = true;
        yield return new WaitForSeconds(activeAttackColliderTime);
        attackVisual.SetActive(false);
        attackBox.enabled = false;
        for (int i = 0; i < maxHits; i++)
        {
            hitEnts[i] = 0;
        }
    }
}
