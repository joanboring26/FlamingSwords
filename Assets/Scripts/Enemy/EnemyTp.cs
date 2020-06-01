using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTp : MonoBehaviour
{
    Vector3 playerVector;
    Vector3 enemyVector;
    Vector3 finalVector;
    //public Transform playerTransform;
    public Transform enemy;
    public Transform tpPlayer;

    public float totalTime = 0;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            totalTime += Time.deltaTime;
            if (totalTime < 1) { 
            
                tpPlayer = collider.GetComponent<Transform>();
                playerVector = tpPlayer.position;

                finalVector = tpPlayer.position - enemy.position;
                finalVector = finalVector.normalized * -1;


                enemy.position = finalVector + enemy.position;
            }
            if (totalTime == 4) totalTime = 0;
        }
    }

    private Vector3 Vector3(float v1, float v2, float y)
    {
        throw new NotImplementedException();
    }
}
