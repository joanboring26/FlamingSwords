using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    public float followVal;
    public Transform followPos;
    public Transform currPos;

    // Start is called before the first frame update
    void Start()
    {
        currPos.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currPos != null)
        {
            currPos.position = new Vector3(
                Mathf.SmoothStep(currPos.position.x, followPos.position.x, followVal * Time.deltaTime),
                Mathf.SmoothStep(currPos.position.y, followPos.position.y, followVal * Time.deltaTime),
                Mathf.SmoothStep(currPos.position.z, followPos.position.z, followVal * Time.deltaTime));
        }
    }
}
