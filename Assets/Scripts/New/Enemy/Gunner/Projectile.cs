using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rig;
    public GameObject expl;
    public LayerMask parryTargets;

    public float lifeTime;
    public float damage;
    public float velocity;
    public float parryVelocity;
    public float parryTargetSearchRadius;

    public bool parried;
    public bool destroy = true;


    private void Start()
    {
        //rig.AddRelativeForce(new Vector2(velocity, 0), ForceMode2D.Impulse);
        rig.velocity = transform.up * -velocity;
        StartCoroutine(destroyOverTime());
    }

    public virtual bool findParryTarget()
    {
        destroy = false;
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position, parryTargetSearchRadius, parryTargets);
        if(hitColliders != null)
        {

            Vector2 dir = hitColliders.transform.position - transform.position;
            Quaternion newRotation = Quaternion.AngleAxis( Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg - 90, Vector3.forward);
            newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);

            Debug.Log(Vector3.Angle(transform.up, -dir.normalized));

            if (Vector3.Angle(transform.up, -dir.normalized) < 45)
            {
                transform.rotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);
                rig.velocity = transform.up * -velocity;
                return true;
            }
        }
        return false;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Player":
                other.GetComponent<EntityHealth>().ModHealth(damage, rig.velocity.normalized * 3);
                Destroy(gameObject);
                break;
            case "Parry":
                gameObject.layer = 12;
                velocity *= 1.6f;

                Vector2 dir = other.transform.up;
                Quaternion newRotation = Quaternion.AngleAxis(Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg - 90, Vector3.forward);
                newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);
                transform.rotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);

                if (!findParryTarget())
                {
                    rig.velocity = transform.up * -velocity;
                }
                break;
            case "NPC":
                other.GetComponent<EnemyHealth>().ModHealth(damage * 3f, transform);
                Destroy(gameObject);
                break;
            default:
                Instantiate(expl, transform.position, Quaternion.Euler(Vector3.zero));
                Destroy(gameObject);
                break;
        }
    }


    IEnumerator destroyOverTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
