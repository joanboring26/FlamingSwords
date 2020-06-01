using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour
{
    Vector3 currentDest;
    Vector3 lastSeenEnemyPos;

    public NavMeshAgent nav;
    public GameObject explosionRef;

    public bool retreating;

    public void MoveToDestination( Vector3 givDest)
    {
        if (nav != null)
        {
            if (nav.stoppingDistance < (transform.position - givDest).magnitude)
            {
                retreating = false;
            }
            else
            {
                Debug.Log("STOPPING");
                retreating = true;
            }

            if (!retreating)
            {
                //Debug.Log("Not near player");
                currentDest = givDest;
                nav.SetDestination(givDest);
            }
            else
            {
                //Debug.Log("NEAR PLAYER");
                currentDest = (transform.position - givDest).normalized * (nav.stoppingDistance * 2) + transform.position;
                //Debug.DrawLine(transform.position, currentDest, Color.red, 20);

                nav.SetDestination(currentDest);
            }
        }
    }

    public void EnemyDead()
    {
        Instantiate(explosionRef, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
