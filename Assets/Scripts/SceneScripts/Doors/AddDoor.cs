using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDoor : MonoBehaviour
{
    public GameObject[] door;
    public void EnemyDead(float hp)
    {
        for(int i = 0; i < door.Length; i++)
        {
            door[i].SendMessage("eventTrig", 1);
        }
    }
}
