using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParry : MonoBehaviour
{
    public float damage;

    public float attackRate;

    public AttackScript attackBox;

    float nextAttack = 0;

    public void Start()
    {
        attackBox.GetComponent<AttackScript>().attackDmg = damage;
    }

    public void targetParry()
    {

    }

    public void initAttack()
    {
        //Vector3 atkVector = Quaternion.Euler(0, Quaternion.LookRotation((transform.position - MousePointer.MousePos), transform.up).eulerAngles.y, 0) * new Vector3(5, 0, 0);
        if (Time.time > nextAttack)
        {
            //Debug.Log("Attacking!");
            nextAttack = Time.time + attackRate;
            StartCoroutine(attackBox.attack());
        }
        else
        {
            //Debug.Log("Didnt attack");
        }
    }
}
