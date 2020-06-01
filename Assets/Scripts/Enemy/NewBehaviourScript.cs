using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform objective;
    // Start is called before the first frame update
    void Start()
    {
        agent.destination = objective.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
