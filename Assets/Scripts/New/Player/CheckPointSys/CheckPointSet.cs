using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSet : MonoBehaviour
{
    public GameObject visuals;
    public Transform checkPointPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPointSys.setSpawnPos(checkPointPos.position);
        visuals.SetActive(true);
    }
}
