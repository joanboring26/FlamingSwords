using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTIme : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyTime(time));
    }

    IEnumerator DestroyTime(float tim)
    {
        yield return new WaitForSeconds(tim);
        Destroy(gameObject);
    }
}
