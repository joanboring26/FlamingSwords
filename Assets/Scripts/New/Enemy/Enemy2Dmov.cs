using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2DMov : MonoBehaviour
{
    Vector3 currentDest;
    Vector3 lastSeenEnemyPos;

    public NavMeshAgent nav;
    public GameObject explosionRef;


    public float speed = 4;

    public bool retreating;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void MoveToDestination(Vector3 givDest)
    {
        if (nav.stoppingDistance < (transform.position - givDest).magnitude)
        {
            retreating = false;
        }
        else
        {
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

    public void EnemyDead()
    {
        Instantiate(explosionRef, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
