using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInupts : MonoBehaviour
{
    public PickupBase baseRef;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            baseRef.managerRef.m1m2Enabled = false;
            if (Input.GetMouseButtonDown(0))
            {
                baseRef.managerRef.setWeapon(1, baseRef.attackBaseRef);
                
            }
            else if (Input.GetMouseButtonDown(1))
            {
                baseRef.managerRef.setWeapon(2, baseRef.attackBaseRef);
            }
        }
        else
        {
            baseRef.managerRef.m1m2Enabled = true;
        }
    }
}
