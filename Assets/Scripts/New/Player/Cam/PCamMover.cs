using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCamMover : MonoBehaviour
{
    //Recoil y cLerp determinan la posicion actual de la cámara entre el Mouse y el jugador, 0.5 = 50% de posicion, osea que está en medio de la posicion
    /*
    private float CAM_POS = 0.5f;
    float recoil = 0.5f;
    */
    public Transform cameraShakeHolder;
    public Transform player;


    float cLerp = 0.5f;

    float shakeAmt;
    Vector3 hitDir = new Vector3();
    Vector3 firstDir = new Vector3();

    public float recoveryRate;
    public float maxRecoil;
    public float recoilDuration;

    //public float xRecoilTest;
    //public float yRecoilTest;
    //public float zRecoilTest;

    public float mouseRange;

    private bool camMove = true;

    private void Update()
    {
        
    }

    public void CamStatus(bool var)
    {
        camMove = var;
    }

    public void camShake(float recoil)
    {
        shakeAmt = recoil;
        //StartCoroutine(cameraHit(givHitDir));
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", recoilDuration);
    }

    public void DoShake()
    {
        if (shakeAmt > 0)
        {
            Vector3 camPos = cameraShakeHolder.position;

            float offsetX = Random.Range(-shakeAmt, shakeAmt) * -1.2f;
            float offsetY = Random.Range(-shakeAmt, shakeAmt) * -1.2f;

            camPos.x += offsetX;
            camPos.z += offsetY;

            cameraShakeHolder.transform.position = camPos;
        }
    }

    public void StopShake()
    {
        CancelInvoke("DoShake");
        cameraShakeHolder.localPosition = Vector3.zero;
    }
}
