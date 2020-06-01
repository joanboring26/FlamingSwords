using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPoint : MonoBehaviour
{
    public NavMeshAgent agente;
    public Transform pointToGo;

    // Start is called before the first frame update
    void Start()
    {
        agente.SetDestination(pointToGo.position);
    }
}
