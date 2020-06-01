using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{
    [Header("Base vars")]
    //Used to spawn the weapon or item in this relative location to the player's item holder base
    public string pickupPrefab;
    public Transform objTransform;
    public Vector3 RelativeSpawnCoordinates;
    //Used for hud stuff
    [Header("HUD shown vars")]
    public Sprite hudIcon;
    public string AtkType;
    public string AtkRate;
    public string AtkDamage;
    public string WepName;
    public string StamUse;

    public bool mUp;

    public EntityHealth playerStats;

    public virtual void attack()
    {

    }

    public virtual void mouseHeld()
    {

    }

    public virtual void mouseReleased()
    {

    }
    
    //Used whenever an enemy hits a collider in the weapon, used for parrying in general
    public virtual void hitByEnemy()
    {

    }

    public virtual void enableWeapon()
    {

    }
}
