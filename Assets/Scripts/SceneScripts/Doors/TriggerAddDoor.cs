using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAddDoor : MonoBehaviour
{
    public GameObject[] door;
    public float addAmount;
    public bool disableOnTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < door.Length; i++)
        {
            door[i].SendMessage("eventTrig", addAmount);

        }
        if(disableOnTrigger)
        {
            gameObject.SetActive(false);
        }
    }

}
