using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkFade : MonoBehaviour
{
    public Material mat;
    public float fadeSpeed;
    //private Material mat;
    private Color tempCol;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.
        //mat = GetComponent<Renderer>().material;
        //mat = gameObject.;
        StartCoroutine(destroyTime(1.45f));
    }

    private void FixedUpdate()
    {
        tempCol = mat.color;
        tempCol.a -= fadeSpeed * Time.deltaTime;
        mat.color = tempCol;
    }

    IEnumerator destroyTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
