using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{

    public GameObject attackSprite;

    private float nextAttack = 0;

    public void attackUpdate(AttackScript givAtkScript, AttackSystem givAttackSystem)
    {
        if (Time.time > nextAttack)
        {
            attackSprite.transform.localScale = new Vector3(-attackSprite.transform.localScale.x, attackSprite.transform.localScale.y, attackSprite.transform.localScale.z);
            nextAttack = Time.time + givAttackSystem.attackRate;
            attackSprite.SetActive(true);
            StartCoroutine(attackUpdateVisuals(givAtkScript));
        }
    }

    IEnumerator attackUpdateVisuals(AttackScript givAtkScript)
    {
        yield return new WaitForSeconds(givAtkScript.activeAttackColliderTime);
        attackSprite.SetActive(false);
    }


}
