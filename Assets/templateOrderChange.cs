using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class templateOrderChange : MonoBehaviour
{
 private void OnEnable()
    {
        Canvas canvas = GetComponent<Canvas>();
        if(canvas)
        {
            Debug.Log("Found");
            canvas.sortingOrder = 0;
 
        }
    }
}
