using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCam : MonoBehaviour
{
    public float MouseSensitivity;
    public float camHeight;
    public Transform mouseDir;
    static public Vector3 PMousePos;
    float camHorizontal;
    float camVertical;

    private Camera cam;

    private Vector3 worldCamPos;
    private Vector2 ScreenCamPos;
    private Vector3 dir;
    private float angle = 0;

    private Event currentEvent;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        cam = Camera.main;
    }

    private void OnGUI()
    {
        /*eulerRot = Quaternion.LookRotation(new Vector3(transform.position.x, transform.position.y), Vector3.up).eulerAngles;
        mouseDir.rotation = Quaternion.Euler(0, 0, eulerRot.z);*/
        dir = mouseDir.position - transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        mouseDir.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //mouseDir.LookAt(new Vector3(transform.position.x, transform.position.y), Vector3.left);
        currentEvent = Event.current;
        ScreenCamPos.x = currentEvent.mousePosition.x;
        ScreenCamPos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        worldCamPos = cam.ScreenToWorldPoint(new Vector3(ScreenCamPos.x, ScreenCamPos.y, cam.nearClipPlane));

        //worldCamPos = new Vector3(Mathf.Clamp(worldCamPos.x, PlayerMov.playerPos.x - mouseRange, PlayerMov.playerPos.x + mouseRange), 1.87f, Mathf.Clamp(worldCamPos.z, PlayerMov.playerPos.z - mouseRange, PlayerMov.playerPos.z + mouseRange));
        worldCamPos = new Vector3(worldCamPos.x, worldCamPos.y, camHeight);

        PMousePos = worldCamPos;
        transform.position = PMousePos;
    }
}
