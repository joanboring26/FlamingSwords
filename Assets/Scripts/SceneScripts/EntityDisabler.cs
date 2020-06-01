using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDisabler : MonoBehaviour
{
    public bool destroyTodisable;
    public bool disableOnTrigger;
    public GameObject[] toEnable;
    public GameObject[] toDisable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!destroyTodisable)
            {
                for (int j = 0; j < toDisable.Length; j++)
                {
                    toDisable[j].SetActive(false);
                }
            }
            else
            {
                for (int k = 0; k < toDisable.Length; k++)
                {
                    Destroy(toDisable[k]);
                }
            }

            for (int i = 0; i < toEnable.Length; i++)
            {
                toEnable[i].SetActive(true);
            }
            
            if(disableOnTrigger)
            {
                enabled = false;
                gameObject.SetActive(false);
            }

        }
    }
}
