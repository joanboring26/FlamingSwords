using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProj : Projectile
{
    public GameObject visual;
    public GameObject partSys;
    public Collider2D alliedColl;
    public BoxCollider2D thisColl;


    public override bool findParryTarget()
    {
        destroy = false;
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position, parryTargetSearchRadius, parryTargets);
        if (hitColliders != null)
        {
            Vector2 dir = hitColliders.transform.position - transform.position;
            Quaternion newRotation = Quaternion.AngleAxis(Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg - 90, Vector3.forward);
            newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);

            transform.rotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);
            rig.velocity = transform.up * -velocity;
            alliedColl = null;
            return true;
        }
        return false;
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other != alliedColl)
        {
            switch (other.tag)
            {
                case "Player":
                    other.GetComponent<EntityHealth>().ModHealth(damage, rig.velocity.normalized * 3);
                    impact();
                    break;
                case "Parry":
                    gameObject.layer = 12;
                    velocity *= 1.6f;
                    damage *= 1.5f;
                    if (!findParryTarget())
                    {
                        Vector2 dir = other.transform.position - transform.position;
                        Quaternion newRotation = Quaternion.AngleAxis(Mathf.Atan2(-dir.y, dir.x) * Mathf.Rad2Deg + 90, Vector3.forward);
                        newRotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);

                        transform.rotation = Quaternion.Euler(0, 180, newRotation.eulerAngles.z);
                        rig.velocity = transform.up * -velocity;
                    }
                    break;
                case "NPC":
                    other.GetComponent<EnemyHealth>().ModHealth(damage, transform);
                    impact();
                    break;
                default:
                    impact();
                    break;
            }
        }
    }

    public void impact()
    {
        Destroy(visual);
        thisColl.enabled = false;
        rig.velocity = Vector3.zero;
        partSys.GetComponent<ParticleSystem>().Play();
    }
}
