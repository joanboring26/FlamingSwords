using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMov : MonoBehaviour
{
    public Transform mov;
    Interpolator inter;

    private void Start()
    {
        inter = new Interpolator(1f, Interpolator.Type.SMOOTHER);
    }

    private void Update() 
    {

        inter.Update(Time.deltaTime);

        if (inter.IsMaxPrecise)
            inter.ToMin();
        if (inter.IsMinPrecise)
            inter.ToMax();

        mov.position = new Vector3(mov.position.x, inter.GetValue() * 5, mov.position.z);
    }

}
