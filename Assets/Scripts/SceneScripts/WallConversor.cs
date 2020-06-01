using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallConversor : MonoBehaviour
{
    public Transform parentHolder;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        for(int i = 0; i < walls.Length; i++)
        {
            SpriteRenderer coll = walls[i].transform.GetComponentInParent<SpriteRenderer>();
            walls[i].transform.SetParent(parentHolder);

            walls[i].GetComponent<BoxCollider>().size = new Vector3(coll.size.x, coll.size.y, 1);
        }
    }
}
