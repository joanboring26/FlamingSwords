using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public enum ShakeType {PLAYERDAM, ENEMYDAM, SWORDSWING, DASH };

    public struct type
    {
        public float m_springVal;
        public float m_damperVal;
        public float m_shakeVal;
        public type(float g_springVal, float g_damperVal, float g_shakeVal)
        {
            m_springVal = g_springVal;
            m_damperVal = g_damperVal;
            m_shakeVal = g_shakeVal;
        }
    }

    public Dictionary< ShakeType,type> typeArr;
    public Transform ogPos;

    private Vector3 posOffsets;

    private float m_spring = 0.0f;
    private float m_damper = 0.0f;
    private float m_shake = 0.0f;

    public Vector2 PositionDiff
    {
        get { return m_initialPos - (Vector2)this.transform.position; }
    }
    private Vector2 m_velocity;
    private Vector2 m_initialPos;
    // Start is called before the first frame update
    void Start()
    {
        //We initialize different types of cameraShake on the typeArr struct

        //We initialize dictionary
        typeArr = new Dictionary<ShakeType, type>();

        //Used when player is hit
        typeArr.Add(ShakeType.PLAYERDAM, new type(0.8366044f, 0.6334246f, 0.65f));

        //Used when the player swings his sword
        typeArr.Add(ShakeType.SWORDSWING, new type(0.4510828f, 0.383356f, 0.3f));

        //Used when the player damages an enemy
        typeArr.Add(ShakeType.ENEMYDAM, new type(0.03430229f, 0.08640016f, 0.45f));

        //

        m_initialPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        m_initialPos = ogPos.position;
        m_velocity += (m_initialPos - (Vector2)this.transform.position) * m_spring;
        m_velocity -= m_velocity * m_damper;
        transform.localPosition += new Vector3(m_velocity.x, m_velocity.y, 0);
    }

    public void AddCustomShake(Vector2 input, ShakeType givShake)
    {
        m_velocity += input * typeArr[givShake].m_shakeVal;
        m_spring = typeArr[givShake].m_springVal;
        m_damper = typeArr[givShake].m_damperVal;
        m_shake = typeArr[givShake].m_shakeVal;
        //m_velocity += new Vector2(Mathf.Clamp(m_velocity.x, minX, maxX), Mathf.Clamp(m_velocity, minY, maxY);)
    }
}
