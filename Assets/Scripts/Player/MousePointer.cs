using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    public float MouseSensitivity;
    public float camHeight;
    public Transform mouseDir;
    static public Vector3 MousePos;
    float camHorizontal;
    float camVertical;

    private Camera cam;

    private Vector3 worldCamPos;
    private Vector2 ScreenCamPos;

    private Event currentEvent;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        cam = Camera.main;
    }

    private void OnGUI()
    {
        mouseDir.LookAt( new Vector3(transform.position.x, mouseDir.position.y, transform.position.z), Vector3.up);
        currentEvent = Event.current;
        ScreenCamPos.x = currentEvent.mousePosition.x;
        ScreenCamPos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        worldCamPos = cam.ScreenToWorldPoint(new Vector3(ScreenCamPos.x, ScreenCamPos.y, cam.nearClipPlane));

        //worldCamPos = new Vector3(Mathf.Clamp(worldCamPos.x, PlayerMov.playerPos.x - mouseRange, PlayerMov.playerPos.x + mouseRange), 1.87f, Mathf.Clamp(worldCamPos.z, PlayerMov.playerPos.z - mouseRange, PlayerMov.playerPos.z + mouseRange));
        worldCamPos = new Vector3( worldCamPos.x, camHeight, worldCamPos.z);

        MousePos = worldCamPos;
        transform.position = MousePos;
    }
}
