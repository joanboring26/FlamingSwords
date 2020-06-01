using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DataManagerTest
{
    
    public float[] colorData { get; private set; }
    public float[] positionData { get; private set; }
    public float zRotationData { get; private set; }

    public DataManagerTest(UITests data)
    {
        colorData = new float[3];
        colorData[0] = data.img.color.r;
        colorData[1] = data.img.color.b;
        colorData[2] = data.img.color.g;

        positionData = new float[2];
        positionData[0] = data.img.gameObject.GetComponent<RectTransform>().anchoredPosition.x;
        positionData[1] = data.img.gameObject.GetComponent<RectTransform>().anchoredPosition.y;

        zRotationData = data.img.GetComponent<RectTransform>().rotation.eulerAngles.z;

    }

}
