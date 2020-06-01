using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
    public Rigidbody pRig;

    [Header("Movement vars")]
    public float speed;
    public float dashSpd;
    public bool isRolling;
    public float rollSpeed;
    public int rollLayer;

    public static float movHorizontal;
    public static float movVertical;

    public void PlayerMove()
    {
        if(isRolling)
        {
            movHorizontal = Input.GetAxis("Horizontal") * -rollSpeed;
            movVertical = Input.GetAxis("Vertical") * -rollSpeed;
            movHorizontal *= Time.deltaTime;
            movVertical *= Time.deltaTime;
            transform.Translate(movHorizontal, pRig.velocity.y * Time.deltaTime, movVertical);
        }
        {
            movHorizontal = Input.GetAxis("Horizontal") * -speed;
            movVertical = Input.GetAxis("Vertical") * -speed;
            movHorizontal *= Time.deltaTime;
            movVertical *= Time.deltaTime;
            transform.Translate(movHorizontal, pRig.velocity.y * Time.deltaTime, movVertical);
        }
    }

    public void InitDash()
    {
        pRig.AddForce(new Vector3(movHorizontal * dashSpd, 0, movVertical * dashSpd));
    }

    public void InitRoll()
    {

    }

    public void StartDefend()
    {

    }
}
