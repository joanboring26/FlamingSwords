using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSetter : MonoBehaviour
{
    public Transform navigator;
    void Update()
    {
        //transform.position = new Vector2(navigator.position.x, navigator.position.y);
        //transform.rotation = Quaternion.Euler( 0, 0, navigator.rotation.eulerAngles.x + 90);
    }
}
