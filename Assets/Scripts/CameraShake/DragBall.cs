using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBall : MonoBehaviour
{
    private CameraShake m_cameraShake;
    private Rigidbody2D m_rb;
    private Drag m_dragScript;
    [SerializeField] ParticleSystem m_particleSystem;

    private bool m_isDestroying = false;

    private void Awake()
    {
        m_rb = this.GetComponent<Rigidbody2D>();
    }

    public void SetData(CameraShake cameraShake, Color color, Drag dragScript)
    {
        this.GetComponent<SpriteRenderer>().color = color;
        ParticleSystem.MainModule partMain = m_particleSystem.main;
        partMain.startColor = color;

        m_cameraShake = cameraShake;
        m_dragScript = dragScript;
    }

    public void LaunchBall(Vector2 vel, float grav)
    {
        this.transform.right = vel.normalized;
        m_particleSystem.Play();
        m_rb.velocity = vel;
        m_rb.gravityScale = grav;
    }

    // Update is called once per frame
    void Update()
    {
        bool outOfBoundsX = false, outOfBoundsY = false;
        outOfBoundsX = this.transform.position.x < (m_dragScript.m_topLeft.position.x + m_dragScript.m_ballRadius) || this.transform.position.x > (m_dragScript.m_bottomRight.position.x - m_dragScript.m_ballRadius);
        outOfBoundsY = this.transform.position.y > (m_dragScript.m_topLeft.position.y - m_dragScript.m_ballRadius) || this.transform.position.y < (m_dragScript.m_bottomRight.position.y + m_dragScript.m_ballRadius);

        if (outOfBoundsX || outOfBoundsY)
        {

            StartCoroutine("DestroyBall");

            //m_cameraShake.AddShake(m_rb.velocity * 0.5f);
            if (outOfBoundsX)
            {
                m_rb.velocity = new Vector2(-m_rb.velocity.x, m_rb.velocity.y);
                this.transform.position = new Vector3(Mathf.Min(Mathf.Max(m_dragScript.m_topLeft.position.x + m_dragScript.m_ballRadius, this.transform.position.x), m_dragScript.m_bottomRight.position.x - m_dragScript.m_ballRadius),
                    this.transform.position.y, this.transform.position.z);
            }
            if (outOfBoundsY)
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, -m_rb.velocity.y);
                this.transform.position = new Vector3(this.transform.position.x,
                    Mathf.Max(Mathf.Min(m_dragScript.m_topLeft.position.y - m_dragScript.m_ballRadius, this.transform.position.y), m_dragScript.m_bottomRight.position.y + m_dragScript.m_ballRadius),
                    this.transform.position.z);
            }
        }
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator DestroyBall()
    {
        if (m_isDestroying)
            yield break;

        m_isDestroying = true;
        while (m_rb.velocity.magnitude > 0.02f || this.transform.localScale.x > 0.02f)
        {
            m_rb.velocity = m_rb.velocity * 0.9f;
            this.transform.localScale = this.transform.localScale * 0.9f;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
}
