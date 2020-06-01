using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDamage : MonoBehaviour
{
    public float totalTime = 0;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player") { 
        totalTime += Time.deltaTime;
        if (totalTime > 1)
        {
            other.gameObject.SendMessage("ModHealth", -5);
            totalTime = 0;
        }
    }
    }
}
