using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
   
    public GameObject door1;
    public GameObject door2;


    public int counter = 0;

    void checkEnemies()
    {
        counter++;
        if (counter == 6)
        {
            Destroy(door2);
            Destroy(door1);
           
            
        }

    }
}
