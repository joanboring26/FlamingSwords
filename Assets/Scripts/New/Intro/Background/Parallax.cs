using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform holder;
    public Transform mainCamRef;
    //Transform startPoint;
    public float parallaxScale;

    //Vector3 parallaxDir;
    //Vector3 parallaxOffset;

    Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //parallaxDir = Vector3.Normalize((mainCamRef.position - startPoint.position));
        holder.position = transform.position + ((mainCamRef.position - startingPos) / parallaxScale);
    }
}
