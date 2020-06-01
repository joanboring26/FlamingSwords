using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCounter : MonoBehaviour
{
    public bool destroyDoor;
    public int numOfEnemies;
    public GameObject door;

    // Start is called before the first frame update
    public void eventTrig(int val)
    {
        numOfEnemies -= val;
        if(numOfEnemies <= 0)
        {
            if (destroyDoor)
            {
                Destroy(door);
                Destroy(gameObject);
            }
            else
            {
                door.SetActive(false);
                Destroy(this);
            }
        }
    }
}
