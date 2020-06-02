using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public GameObject inspectHolder;
    public Image mouse1;
    public Image mouse2;
    public Image num1;
    public Image num1Fill;
    public Image itemImage;
    public TextMeshProUGUI name;
    public TextMeshProUGUI type;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI attackRate;
    public TextMeshProUGUI staminaUse;

    public void updateItem(Sprite gitemImage, string gname, string gtype, string gdamage, string gattackRate,string gstaminaUse)
    {
        inspectHolder.SetActive(true);
        if (name.text != gname)
        {
            itemImage.sprite = gitemImage;
            name.text = gname;
            type.text = gtype;
            damage.text = gdamage;
            attackRate.text = gdamage;
            staminaUse.text = gstaminaUse;
        }
    }

    public void hideItem()
    {
        inspectHolder.SetActive(false);
    }

    public void setItem(Sprite givSprite, int mouseNum)
    {
        if(mouseNum == 1)
        {
            mouse1.sprite = givSprite;
        }
        else if(mouseNum == 2)
        {
            mouse2.sprite = givSprite;
        }
    }


    public void setAbility()
    {
        //StartCoroutine(fadeTimer(4));
    }

    public IEnumerator fadeTimer1(float fadeTime)
    {
        num1Fill.fillAmount = 0;
        float fadeDurationInSeconds = fadeTime;
        float timeout = 0.01f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 0; f <= 1; f += fadeAmount)
        {
            num1Fill.fillAmount = f;
            yield return new WaitForSeconds(timeout);
        }

        num1Fill.fillAmount = 1;
    }

}
