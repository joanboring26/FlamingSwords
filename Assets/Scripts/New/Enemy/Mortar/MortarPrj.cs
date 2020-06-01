using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarPrj : MonoBehaviour
{
    public float explDel;
    public float rotAdd;
    public GameObject explsRef;
    public AudioSource approachSnd;

    private float prevRot = 0;

    private void Start()
    {
        StartCoroutine(projExpl());
    }

    private void FixedUpdate()
    {
        prevRot = (prevRot + rotAdd);
        transform.rotation = Quaternion.Euler(0,0, transform.rotation.eulerAngles.z + prevRot);
    }

    IEnumerator projExpl()
    {
        yield return new WaitForSeconds(explDel);
        Instantiate(explsRef, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
