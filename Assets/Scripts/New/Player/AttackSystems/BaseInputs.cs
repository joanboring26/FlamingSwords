using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseInputs : MonoBehaviour
{
    public EntityHealth pStats;
    public PMove pFunc;

    [Header("Stamina requirements")]
    public float dashStaminaUse;

    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI text;

    // Update is called once per frame

    private void FixedUpdate()
    {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            pFunc.PlayerMove();
            pFunc.walkSource.UnPause();
        }
        else
        {
            pFunc.walkSource.Pause();
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            if (pStats.stamina > dashStaminaUse)
            {
                pStats.ModStamina(-dashStaminaUse);
                pFunc.InitDash();
            }
        }
    }

    IEnumerator WriteText()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
        }
    }
}
