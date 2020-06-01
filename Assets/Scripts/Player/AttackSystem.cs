using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public float damage;

    public float attackRate;

    public AttackScript attackBox;

    public bool camShakeEnabled;
    public float attackShake;
    public float giveHitShake;
    public float takeHitShake;
    public CameraMover cam;

    public float nextAttack = 0;

    public void Start()
    {
        attackBox.GetComponent<AttackScript>().attackDmg = damage;
    }

    public void targetHit()
    {
        cam.camShake(giveHitShake);
    }

    public void targetParry()
    {

    }

    public bool canAttack()
    {
        if (Time.time > nextAttack)
        {
            return true;
        }
        return false;
    }

    public void initAttack()
    {
        //Vector3 atkVector = Quaternion.Euler(0, Quaternion.LookRotation((transform.position - MousePointer.MousePos), transform.up).eulerAngles.y, 0) * new Vector3(5, 0, 0);
        if(Time.time > nextAttack)
        {
            //Debug.Log("Attacking!");
            nextAttack = Time.time + attackRate;
            if(camShakeEnabled)
            {
                cam.camShake(attackShake);
            }
            StartCoroutine(attackBox.attack());
        }
        else
        {
            //Debug.Log("Didnt attack");
        }
    }
}