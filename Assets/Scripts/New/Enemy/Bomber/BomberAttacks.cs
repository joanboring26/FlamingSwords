using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAttacks : MonoBehaviour
{

    [Header("Other variables")]
    public GameObject explosion;
    public GameObject objectBase;
    public bool canDetonate = true;
    public EnemyVision visScript;
    public IEnumerator explIEnum;

    public MeshRenderer meshRend;
    Material mat;
    public Color initCol;

    private float colChangeAm = 0.1f;

    [Header("Time variables")]
    public float preReadyTime;
    public float detonationTime;

    [Header("Audio variables")]
    public AudioSource soundSource;
    public AudioClip preReadySound;
    public AudioClip detonationSound;

    private void Start()
    {
        explIEnum = startExpl();
        mat = meshRend.material;
        mat.color = initCol;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canDetonate && (collision.tag == "Player"))
        {
            StartCoroutine(explIEnum);
        }
    }

    public IEnumerator startExpl()
    {
        StartCoroutine(starColChange());
        canDetonate = false;
        visScript.movScript.nav.isStopped = true;
        visScript.movScript.nav.enabled = false;
        visScript.enabled = false;

        soundSource.enabled = true;
        soundSource.PlayOneShot(preReadySound);
        yield return new WaitForSeconds(preReadyTime);

        soundSource.PlayOneShot(detonationSound);
        yield return new WaitForSeconds(detonationTime);
        if (!canDetonate)
        {
            Debug.Log("AAAA!!");
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(objectBase);
        }
    }

    public IEnumerator starColChange()
    {
        mat.color = initCol;
        while (mat.color.g > 0)
        {
            mat.color = new Color( mat.color.r, Mathf.SmoothStep(mat.color.g,0,colChangeAm), mat.color.b);
            yield return new WaitForEndOfFrame();
        }

    }

}
