using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PInputs : MonoBehaviour
{

    public EntityHealth pStats;
    public PMove pFunc;
    public PAttSyst pAttack;
    public PVisuals pVisuals;
    public PParry pPSystem;

    [Header("Stamina requirements")]
    public float parryStaminaUse;
    public float dashStaminaUse;
    public float attackStaminaUse;

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
            if(pStats.stamina > dashStaminaUse)
            {
                pStats.ModStamina(-dashStaminaUse);
                pFunc.InitDash();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(pAttack.canAttack() && (pStats.stamina > attackStaminaUse))
            {
                pAttack.initAttack();
                pStats.ModStamina(-attackStaminaUse);
                pVisuals.attackUpdate(pAttack.attackBox, pAttack);
            }
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            if (pAttack.canAttack() && !pPSystem.parrying && pPSystem.CanParry())
            {
                if (pStats.stamina > parryStaminaUse)
                {
                    pPSystem.DoParry();
                    pStats.ModStamina(-parryStaminaUse);
                }
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
