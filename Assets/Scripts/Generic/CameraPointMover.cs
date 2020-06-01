using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointMover : MonoBehaviour
{
    public GameObject camRef;

    public float camSpeed;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        startCameraMoving(camRef);
    }

    void startCameraMoving(GameObject cameraHolder)
    {
        camRef.BroadcastMessage("CamStatus", false);
        camRef = cameraHolder;
        transform.position = new Vector3(camRef.transform.position.x, 0, camRef.transform.position.z);
        StartCoroutine(moveCamera());
    }

    IEnumerator moveCamera()
    {
        yield return new WaitForSeconds(1);
        while(Vector3.Distance(transform.position, camRef.transform.position) > 1)
        {
            camRef.transform.position = Vector3.SmoothDamp(camRef.transform.position, transform.position, ref velocity, camSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

}
