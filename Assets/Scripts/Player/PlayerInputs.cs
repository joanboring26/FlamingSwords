using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInputs : MonoBehaviour
{
    public MovPlayer pFunc;
    public AttackSystem pAttack;
    public PlayerVisuals pVisuals;
    public ParrySystem pPSystem;
    
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI text;
    
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
        {
            pFunc.PlayerMove();
        }
        else
        {
            pFunc.walkSource.Pause();
        }

        if(Input.GetButtonDown("Dash"))
        {
            pFunc.InitDash();
        }

        if(Input.GetMouseButtonDown(0))
        {
            pAttack.initAttack();
            pVisuals.attackUpdate(pAttack.attackBox, pAttack);
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(pAttack.canAttack() && !pPSystem.parrying)
            {
                pPSystem.DoParry();
            }
        }

        /*
        if(Input.GetButtonDown("Roll"))
        {
            pFunc.InitRoll();
        }*/
    }

    IEnumerator WriteText()
    {
        while(true)
        {
            Debug.Log(text.text);
            yield return new WaitForSeconds(1f);
        }
    }
}
