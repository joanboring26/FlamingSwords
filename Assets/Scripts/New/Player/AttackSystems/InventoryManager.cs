using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public AttackBase item1;
    public AttackBase item2;
    public AttackBase ability1;

    public GameObject wepHolder;
    public HudManager HudMan;
    public EntityHealth playerStats;

    public bool ability1OnStart = false;
    public bool m1m2Enabled = true;
    public bool abilityEnabled = true;

    private void Start()
    {
        HudMan.setAbility();
    }
    void Update()
    {
        if (m1m2Enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (item1 != null) item1.attack();

            }
            if (Input.GetMouseButtonDown(1))
            {
                if (item2 != null) item2.attack();
            }
        }

        if(abilityEnabled)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if (ability1 != null) ability1.attack();
            }
        }
    }

    //
    public void setWeapon(int wep, AttackBase wepAtkBase)
    {
        if(wep == 1)
        {
            if(item1 != null)
            {
                dropWeapon(item1);
            }
            HudMan.setItem(wepAtkBase.hudIcon, 1);
            item1 = wepAtkBase;
            item1.playerStats = playerStats;
            setPos(item1);
        }
        else if(wep == 2)
        {
            if(item2 != null)
            {
                dropWeapon(item2);
            }
            HudMan.setItem(wepAtkBase.hudIcon, 2);
            item2 = wepAtkBase;
            item2.playerStats = playerStats;
            setPos(item2);
        }
    }

    //Reseteamos el transform del arma, poniendolo en el weaponHolder del player y la activamos
    void setPos(AttackBase item)
    {
        GameObject tmp = item.transform.parent.gameObject;
        item.objTransform.parent = wepHolder.transform;
        item.objTransform.localPosition = item.RelativeSpawnCoordinates;
        item.objTransform.rotation = wepHolder.transform.rotation;
        item.objTransform.localScale = Vector3.one;
        item.enableWeapon();


        //Destruimos su parent(El pickup base)
        Destroy(tmp);
    }

    public void dropWeapon(AttackBase wepAtkBase)
    {
        //Instanciamos el prefab de el arma en el suelo y destruimos el arma que nos han pasado que deberia de ser soltada
        Instantiate(Resources.Load("Weapons/Pickup/" + wepAtkBase.pickupPrefab) as GameObject, transform.position, transform.rotation);
        Destroy(wepAtkBase.gameObject);
    }

    public void showItem(AttackBase givItem)
    {
        HudMan.updateItem( givItem.hudIcon, givItem.WepName, givItem.AtkType, givItem.AtkDamage, givItem.AtkRate, givItem.StamUse);
    }

    public void hideItem()
    {
        HudMan.hideItem();
    }
}
