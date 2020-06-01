using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float m_bounceTime = 1.0f;
    private Vector2 m_initialPos;
    public float m_jumpHeight;
    private Vector2 m_finalPos;
    private float m_currentBounceTime = 0.0f;
    private bool m_bouncingUp = false;

    [SerializeField] CameraShake m_cameraShake;

    private void Start()
    {
        m_finalPos = this.transform.position;
        m_initialPos = m_finalPos + Vector2.up * m_jumpHeight;
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_bouncingUp) {
            if (m_currentBounceTime < m_bounceTime)
            {
                m_currentBounceTime += Time.deltaTime;
                if (m_currentBounceTime >= m_bounceTime)
                {
                    m_currentBounceTime = m_bounceTime;
                    m_bouncingUp = true;
                    //Trigger camera shake
                    //m_cameraShake.AddShake(Vector2.down);
                }
            }
        }
        else
        {
            if (m_currentBounceTime > 0f)
            {
                m_currentBounceTime -= Time.deltaTime;
                if(m_currentBounceTime <= 0.0f)
                {
                    m_currentBounceTime = 0.0f;
                    m_bouncingUp = false;
                }
            }
        }
        this.transform.position = Vector3.Lerp(m_initialPos, m_finalPos, Mathf.Pow(1f - Mathf.Cos(m_currentBounceTime / m_bounceTime * Mathf.PI * 0.5f), 1.5f));
    }

    private void OnDisable()
    {
        this.transform.position = m_initialPos;
        this.m_bouncingUp = false;
        this.m_currentBounceTime = 0;
    }
}
