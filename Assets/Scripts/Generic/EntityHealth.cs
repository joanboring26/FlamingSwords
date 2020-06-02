using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class EntityHealth : MonoBehaviour
{
    [Header("Health variables")]
    public float hp;
    float maxHp;
    public Image healthbar;


    [Header("Stamina variables")]
    public float maxStamina;
    public float stamina;
    public float staminaRechargeRate;
    public Slider staminabar;

    [Header("Previous health variables")]
    public float delayPrevHealth;
    public float prevHealthEmptyRate;
    public int prevHealthRecovery; //Se usa para calcular cuanta vida recupera un ataque a un enemigo
    private float prevHealth = 0;
    private bool drainPrevHealth = false;
    public Image prevHealthBar;


    [Header("OnDeath variables")]
    //Script will broadcast this message to the thing it is attached to when it dies
    public string deathMessage;
    public GameObject deadSprite;
    public GameObject deadHudMsg;

    [Header("OnHit variables")]
    public string hitMessage;
    public float nextDamageDelay;
    public GameObject mainCam;
    public GameObject explosionRef;

    [Header("Damaged visuals")]
    public SpriteRenderer body;
    public Sprite[] damagedSprites;
    int totalStates;
    int currState;

    [Header("Audio")]
    public AudioSource painSrc;
    public AudioClip[] hitSnds;

    float nextDamage = 0;
    public float totalTime = 1;

    [Header("Other")]
    public Transform playerRot;
    public CameraShake camShaker;
    public PCamMover camMover;
    public PCam camHolder;
    public MusicScene musicHolder;
    public CrossTimer crossFiller;

    private float scaleVal = 0;
    private float maxHPBarVal = 0.9f;
    private float minHPBarVal = 0.1f;
    private float newRange = 0;

    public PostProcessVolume postP;
    private Grain grainInt;

    //Escala la vida actual a la barra de vida que tiene el jugador pegada
    public float scaleToHP( float OldValue)
    {
        scaleVal = (((OldValue - 0) * newRange) / maxHp) + minHPBarVal;
        return (scaleVal);
    }

    private void Start()
    {
        newRange = (maxHPBarVal - minHPBarVal);

        prevHealth = hp;
        prevHealthBar.fillAmount = maxHPBarVal;
        maxHp = hp;
        healthbar.fillAmount = scaleToHP(hp);

        maxStamina = stamina;
        staminabar.maxValue = maxStamina;
        staminabar.value = maxStamina;

        totalStates = damagedSprites.Length;
        currState = totalStates;
                                                            //Setting initial grain intensity
        prevHealthBar.fillAmount = scaleToHP(prevHealth);
        postP.sharedProfile.TryGetSettings(out grainInt);
        grainInt.intensity.Override(0);
        


    }

    private void FixedUpdate()
    {
        stamina = Mathf.Clamp(stamina + staminaRechargeRate, 0, maxStamina);
        staminabar.value = stamina;
        if (drainPrevHealth)
        {
            prevHealth = Mathf.Clamp(prevHealth - prevHealthEmptyRate, hp, maxHp);
            prevHealthBar.fillAmount = scaleToHP(prevHealth);
        }
    }

    public void ModHealth(float givVal)
    {
        if(Time.time > nextDamage)
        {
            if (givVal < 0)
            {
                Instantiate(explosionRef, transform.position, transform.rotation);
                camShaker.AddCustomShake(-transform.up * 3, CameraShake.ShakeType.PLAYERDAM);
                StartCoroutine(PrevHealthStart(givVal));
            }
            nextDamage = Time.time + nextDamageDelay;
            hp += givVal;

            //Visuals
            currState = Mathf.RoundToInt((hp / maxHp) * totalStates);
            body.sprite = damagedSprites[currState];

            if (mainCam != null)
            {
                painSrc.PlayOneShot(hitSnds[Random.Range(0, hitSnds.Length)]);
                mainCam.SendMessage(hitMessage, 0.3f);
            }

            if (hp <= 0)
            {
                deadHudMsg.SetActive(true);
                Destroy(camShaker);
                Destroy(camMover);
                Destroy(camHolder);
                mainCam.transform.parent = null;
                mainCam.GetComponent<CheckRestar>().enabled = true;
                Instantiate(deadSprite, transform.position, transform.rotation);
                //StartCoroutine(checkrestart());
                gameObject.SendMessage(deathMessage, hp);
                Destroy(gameObject);
            }
            else if(hp > 100)
            {
                hp = 100;
            }
        }

        healthbar.fillAmount = scaleToHP(hp);
       // GrainChange();
        
    }

    public void ModHealth(float givVal, Vector2 dir)
    {
        if (Time.time > nextDamage)
        {
            Instantiate(explosionRef, transform.position, transform.rotation);
            camShaker.AddCustomShake(-dir, CameraShake.ShakeType.PLAYERDAM);
            //StartCoroutine(PrevHealthStart(givVal));

            nextDamage = Time.time + nextDamageDelay;
            hp += givVal;

            //Visuals
            currState = Mathf.RoundToInt((hp / maxHp) * totalStates);
            body.sprite = damagedSprites[currState];

            painSrc.PlayOneShot(hitSnds[Random.Range(0, hitSnds.Length)]);

            if (hp <= 0)
            {
                deadHudMsg.SetActive(true);
                Destroy(camShaker);
                Destroy(camMover);
                Destroy(camHolder);
                mainCam.transform.parent = null;
                mainCam.GetComponent<CheckRestar>().enabled = true;
                Instantiate(deadSprite, transform.position, transform.rotation);
                //StartCoroutine(checkrestart());
                gameObject.SendMessage(deathMessage, hp);
                Destroy(gameObject);
            }
        }

        healthbar.fillAmount = scaleToHP(hp);
       // GrainChange();
    }

    public void RecoverPrevHealth(int dealtDamage)
    {
        if(prevHealth > hp)
        {
            //hp = Mathf.Clamp((dealtDamage / prevHealthRecovery) * dealtDamage + hp, hp, prevHealth);
            hp = Mathf.Clamp(prevHealthRecovery + hp, hp, prevHealth);
            healthbar.fillAmount = scaleToHP(hp);
        }

        //GrainChange();
        prevHealth=hp;
        prevHealthBar.fillAmount = scaleToHP(hp);
    }

    public void ModStamina(float givVal)
    {
        stamina += givVal;
    }
        //postP.sharedProfile.TryGetSettings<Grain>(out grainInt);
        //grainInt.intensity.Override(1-(hp/100));

    IEnumerator PrevHealthStart( float damage)
    {
        prevHealthBar.fillAmount = scaleToHP(hp);
        prevHealth = hp;
        drainPrevHealth = false;
        yield return new WaitForSeconds(delayPrevHealth);
        drainPrevHealth = true;
    }
}
