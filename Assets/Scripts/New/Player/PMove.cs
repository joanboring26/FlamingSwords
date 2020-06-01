using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    public DashFade dashFade;
    public GameObject rayHolder;
    public GameObject MouseDirection;
    public GameObject dashTrail;

    public static Transform playerTransform;

    public Rigidbody2D pRig;


    [Header("Movement vars")]
    public float speed;
    public float dashSpd;

    public bool moveEnabled;
    public float dashTime;

    public static float movHorizontal;
    public static float movVertical;

    [Header("Audio Vars")]

    public AudioSource walkSource;
    public AudioSource dashSource;

    public AudioClip[] dashSnds;

    private void Start()
    {
        playerTransform = transform;
        walkSource = GetComponent<AudioSource>();
    }
    public void PlayerMove()
    {
        if (moveEnabled)
        {
            movHorizontal = Input.GetAxis("Horizontal") * speed;
            movHorizontal *= Time.deltaTime;
            movVertical = Input.GetAxis("Vertical") * speed;
            movVertical *= Time.deltaTime;
            pRig.velocity = new Vector2(movHorizontal, movVertical);
        }
        else
        {
            movHorizontal = Input.GetAxis("Horizontal");
            movVertical = Input.GetAxis("Vertical");
        }
    }

    public void InitDash()
    {
        StartCoroutine(dashMove());
    }

    public void deadPlayer()
    {
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<PMove>());
        Destroy(GetComponent<PInputs>());
        Destroy(MouseDirection);
        Destroy(rayHolder);
    }

    IEnumerator dashMove()
    {
        //Start the dash fade effect
        StartCoroutine(dashFade.fadeTimer(dashTime + 0.1f));
        //

        dashTrail.SetActive(true);
        dashSource.PlayOneShot(dashSnds[Random.Range(0, dashSnds.Length)]);
        moveEnabled = false;
        Vector2 tempVec;
        tempVec.x = 0;
        tempVec.y = 0;
        float nextRoll = Time.time + dashTime;
        while (nextRoll > Time.time)
        {
            tempVec.x = movHorizontal * Time.deltaTime;
            tempVec.y = movVertical * Time.deltaTime;
            tempVec.Normalize();
            tempVec *= dashSpd;
            pRig.velocity = new Vector2(tempVec.x * dashSpd, tempVec.y * dashSpd);
            yield return new WaitForEndOfFrame();
        }
        dashTrail.SetActive(false);
        moveEnabled = true;

    }

}