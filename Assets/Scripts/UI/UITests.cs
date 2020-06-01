using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITests : MonoBehaviour
{

    public Image img;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.SaveData(this);
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            DataManagerTest data = SaveSystem.LoadData();
            if(data == null)
            {
                return;
            }
            img.color = new Color(data.colorData[0], data.colorData[1], data.colorData[2]);
            img.GetComponent<RectTransform>().anchoredPosition = new Vector2(data.positionData[0], data.positionData[1]);
            img.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, data.zRotationData);
        }

    }

    // Update is called once per frame
    public void RandomizeOnPress()
    {
        img.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        img.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-50f, 50f), 40f);
        img.gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Random.Range(-30f, 30f));
    }
}
