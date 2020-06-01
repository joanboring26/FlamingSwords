using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDamage : MonoBehaviour
{
    public GameObject[] Chunks;
    public float destroyTime;
    public float maxChunkSpeed;
    public float minChunkSpeed;
    public int chunkAmount;

    public float disappearSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tempGObj;
        for(int i = 0; i < chunkAmount; i++)
        {
            tempGObj = Instantiate( Chunks[ i % Chunks.Length ], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(180,0,Random.Range(0,359)));
            tempGObj.transform.rotation = Quaternion.Euler(180, 0, Random.Range(0, 360));
            tempGObj.GetComponent<ChunkFade>().fadeSpeed = disappearSpeed;
            tempGObj.GetComponent<Rigidbody2D>().velocity = tempGObj.transform.up * -Random.Range(minChunkSpeed,maxChunkSpeed);
            tempGObj.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-10, 10);
        }
        StartCoroutine(deleteTimer());
    }

    IEnumerator deleteTimer()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
