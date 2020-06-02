using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAttacks : MonoBehaviour
{
    [Header("Component references")]
    public EnemyVision givVision;
    public EnemyAttackScript givAttackScript;
    public StunIndicator givStunIndicator;

    [Header("Visuals")]
    public GameObject preAttackSprite;
    public GameObject attackingSprite;
    [Header("Position references")]
    public Transform enemyTransform;
    [Header("Physics")]
    public Rigidbody2D rig;
    [Header("AI")]
    public NavMeshAgent aiAgent;

    [Header("Audio references")]

    public AudioClip preAttackSnd;
    public AudioClip attackSnd;
    public AudioClip stunnedSnd;
    public AudioSource sndSrc;

    [Header("Enemy settings")]

    public float activeAttackTime;
    public float preAttackSpd;
    public float preAttackDrag;
    public float attackSpd;
    public float attackDrag;
    public float stunForce;

    public float attackCooldown;
    public float stunCooldown;
    public float stunBack;

    public float attackDelay;

    private IEnumerator coroutineEnum;

    float nextAttack;
    Vector3 attackPos;

    private bool dontAttack = false;
    private Quaternion newRotation;
    private Vector3 dir;
    private float angle = 0;
    private float originalDrag;

    void preAttackStarted()
    {
        originalDrag = rig.drag;
        rig.drag = preAttackDrag;

        if (coroutineEnum != null)
        {
            StopCoroutine(coroutineEnum);
        }
        sndSrc.enabled = true;
        sndSrc.PlayOneShot(preAttackSnd);

        attackPos = givVision.detectedTransform.position;
        //
        dir = givVision.detectedTransform.position - givVision.npcTransform.position;
        angle = Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg - 90;
        newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        givVision.npcTransform.rotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);
        //

        aiAgent.isStopped = true;
        preAttackSprite.SetActive(true);
        if ((givVision.detectedTransform != null) && !dontAttack)
        {
            rig.velocity = transform.up * preAttackSpd;
        }
    }

    void attackStarted()
    {
        rig.drag = attackDrag;
        givVision.lookAtTarget = false;

        sndSrc.PlayOneShot(attackSnd);

        StartCoroutine(givAttackScript.attack());
        attackingSprite.SetActive(true);
        preAttackSprite.SetActive(false);
        if (givVision.detectedTransform != null)
        {
            /*
            Vector2 atkVector = new Vector2(transform.position.x - attackPos.x, transform.position.y - attackPos.y).normalized;
            rig.velocity = new Vector2(atkVector.x, atkVector.y) * attackSpd;
            */
            rig.velocity = transform.up * -attackSpd;
        }
    }

    void attackDone()
    {
        rig.drag = originalDrag;
        givVision.lookAtTarget = true;
        attackingSprite.SetActive(false);
        aiAgent.isStopped = false;
    }

    IEnumerator preAttack()
    {
        preAttackStarted();
        yield return new WaitForSeconds(attackDelay);
        attackStarted();
        yield return new WaitForSeconds(activeAttackTime);
        attackDone();

        if (coroutineEnum != null)
        {
            StopCoroutine(coroutineEnum);
        }
        coroutineEnum = DisableTimer();
        StartCoroutine(coroutineEnum);

        rig.velocity = rig.velocity/3;
    }

    public IEnumerator stunned(Vector3 parryPos)
    {
        if (coroutineEnum != null)
        {
            StopCoroutine(coroutineEnum);
        }
        sndSrc.enabled = true;
        sndSrc.PlayOneShot(stunnedSnd);
        coroutineEnum = DisableTimer();
        StartCoroutine(coroutineEnum);

        givVision.enabled = false;
        givAttackScript.attackBox.enabled = false;
        givAttackScript.enabled = false;
        dontAttack = true;
        rig.velocity = new Vector2(enemyTransform.position.x - PMove.playerTransform.position.x, enemyTransform.position.y - PMove.playerTransform.position.y).normalized * stunForce;
        StartCoroutine(givStunIndicator.stunTimer(stunCooldown));
        yield return new WaitForSeconds(stunCooldown);
        givAttackScript.parried = false;
        dontAttack = false;
        givAttackScript.enabled = true;
        givAttackScript.attackBox.enabled = false;
        givVision.enabled = true;
    }

    IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(3f);
        sndSrc.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Time.time > nextAttack)
        {
            if (!dontAttack)
            {
                nextAttack = Time.time + attackCooldown;
                StartCoroutine("preAttack");
            }
        }
    }

}
