using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxObject
{
    public GameObject PObject;
    public float spdX;
    public float spdY;
}
public class Parallax_Mult : MonoBehaviour
{
    float difX;
    float difY;
    Vector3 lastScreenPos;
    public Camera cam;
    public ParallaxObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        lastScreenPos = cam.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        difX = cam.transform.position.x - lastScreenPos.x;
        difY = cam.transform.position.y - lastScreenPos.y;
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].PObject.transform.Translate(transform.right*objects[i].spdX*difX);
            objects[i].PObject.transform.Translate(transform.up*objects[i].spdY*difY);
        }
        lastScreenPos = cam.transform.position;
    }
}
