using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : AttackBase
{
    [Header("Parry vars")]

    public bool parrying = false;

    public float activeParryColliderTime;
    public float parryRate;
    public int staminaUse;

    public int staminaRestore;

    public GameObject visualParry;

    float nextParry = 0;

    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void hitByEnemy()
    {
        playerStats.ModStamina(staminaRestore);
    }

    public override void attack()
    {
        if (nextParry + parryRate < Time.time)
        {
            if (playerStats.stamina > staminaUse && !visualParry.activeInHierarchy)
            {
                playerStats.ModStamina(-staminaUse);
                nextParry = Time.deltaTime + parryRate;
                StartCoroutine(doParry());
            }
        }
    }

    public IEnumerator doParry()
    {
        StartCoroutine(playerStats.crossFiller.fadeTimer(parryRate - 0.4f));
        parrying = true;
        visualParry.SetActive(true);
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(activeParryColliderTime);
        GetComponent<BoxCollider2D>().enabled = false;
        visualParry.SetActive(false);
        parrying = false;
    }
}
