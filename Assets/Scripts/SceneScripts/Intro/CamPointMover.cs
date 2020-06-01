using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class point
{
    public Transform pointPos;
    public float speed;
    public float holdTime;
    public GameObject endTarget;
    public string message;
}

public class CamPointMover : MonoBehaviour
{
    int currPoint = 0;
    bool holding = false;
    public bool move;
    public point[] camPoints;
    public GameObject[] endTargets;
    public string endMessage;
    public Vector2 dirVec;
    public Vector2 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move && (currPoint < camPoints.Length))
        {
            dirVec = camPoints[currPoint].pointPos.position - transform.position;
            Debug.Log(Vector2.SqrMagnitude(dirVec));
            if (Vector2.SqrMagnitude(dirVec) > (0.0005))
            {
                dirVec = dirVec.normalized;
                transform.Translate(dirVec * camPoints[currPoint].speed * Time.deltaTime);
            }
            else if (!holding)
            {
                if (currPoint == camPoints.Length)
                {
                    move = false;
                    for(int i = 0; i < endTargets.Length; i++)
                    {
                        endTargets[i].SendMessage(endMessage);
                    }
                }
                else
                {
                    holding = true;
                    StartCoroutine(holdPoint(camPoints[currPoint].holdTime));
                }
            }
        }
    }

    IEnumerator holdPoint(float holdSeconds)
    {
        transform.position = camPoints[currPoint].pointPos.position;
        yield return new WaitForSeconds(holdSeconds);
        if (camPoints[currPoint].endTarget != null)
        {
            camPoints[currPoint].endTarget.SendMessage(camPoints[currPoint].message);
        }
        currPoint++;
        holding = false;
    }

    void StartMoving()
    {
        move = true;
    }
}
