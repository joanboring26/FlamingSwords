using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberLDetect : MonoBehaviour
{
    public GameObject explosion;
    public GameObject base2d;
    public GameObject baseAI;

    string deathMessage; 
    private void Start()
    {
        deathMessage = base2d.GetComponent<EnemyHealth>().deathMessage;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.tag != "Attack" && collision.tag != "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(baseAI);
        }
    }
}
