using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjMarker : MonoBehaviour
{
    public UIPointerObj marker;
    public Transform newObj;

    public bool disableOnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        marker.point = newObj.position;
        if(disableOnTrigger)
        {
            gameObject.SetActive(false);
        }
    }
}
