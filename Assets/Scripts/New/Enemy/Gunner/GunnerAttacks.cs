using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerAttacks : MonoBehaviour
{
    public LayerMask enemyVisionLayers;

    public GameObject projectile;
    public GameObject attackAnims;

    public Transform muzzleFire;
    public AudioSource fireSnd;
    public AudioSource chargeSrc;
    public AudioClip chargeSnd;
    public AudioClip rdySnd;

    public float fireRate;
    public int bulletsPerBurst;
    public float attackCooldown;
    public bool dontAttack;

    private float nextAttack = 0;
    private Color initCol;

    public GameObject chargeSprite;
    private ParticleSystem partSys;
    private SpriteRenderer sprRender;
    public Color targetCol;
    //

    private void Start()
    {
        sprRender = chargeSprite.GetComponent<SpriteRenderer>();
        partSys = chargeSprite.GetComponent<ParticleSystem>();
        initCol = sprRender.color;
        chargeSprite.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(CheckRay(other.transform))
        {
            if (Time.time > nextAttack)
            {
                if (!dontAttack)
                {
                    nextAttack = Time.time + attackCooldown;
                    chargeSprite.SetActive(true);
                    StartCoroutine(ChargeAn(attackCooldown));
                    StartCoroutine(Burst());
                }
            }
        }
    }

    IEnumerator Burst()
    {
        partSys.Play();
        chargeSrc.PlayOneShot(rdySnd);
        yield return new WaitForSeconds(0.1f);
        sprRender.color = initCol;
        attackAnims.SetActive(true);
        for(int i = 0; i < bulletsPerBurst ; i++)
        {
            fireSnd.PlayOneShot(fireSnd.clip);
            Instantiate(projectile, muzzleFire.position, muzzleFire.rotation);
            yield return new WaitForSeconds(fireRate);
        }
        attackAnims.SetActive(false);
    }


    public IEnumerator ChargeAn(float fadeTime)
    {
        chargeSrc.PlayOneShot(chargeSnd);
        sprRender.color = new Color(initCol.r, initCol.g, initCol.b, 0);
        float fadeDurationInSeconds = fadeTime - 0.8f;
        float timeout = 0.01f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = 0; f <= 1; f += fadeAmount)
        {
            Color c = sprRender.color;
            c.a = f;
            sprRender.color = c;
            yield return new WaitForSeconds(timeout);
        }
        sprRender.color = new Color(initCol.r, initCol.g, initCol.b, 255);
    }

    public bool CheckRay(Transform detectedTransform)
    {
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(transform.position, detectedTransform.position - transform.position, 30f, enemyVisionLayers);
        if (hitInfo.collider.tag == "Player")
        {
            return true;
        }
        return false;
    }

}
