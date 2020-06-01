using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPointerObj : MonoBehaviour
{
    public Transform objPoint;
    public Transform playerPoint;
    public Vector3 point;

    RectTransform currTransform;

    private Quaternion newRotation;
    private Vector3 dir;
    private float angle = 0;
    private void Start()
    {
        point = objPoint.position;
        currTransform = GetComponent<RectTransform>();
    }

    //Please note that this script NEEDS an object to point to BEFORE it is enabled/Activated
    void Update()
    {
        if (objPoint != null)
        {
            dir = point - playerPoint.position;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
