using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    //Camera shake ref
    [SerializeField] CameraShake m_cameraShake;

    //Ball color and spawning
    [SerializeField] Color[] m_ballColors;
    [SerializeField] GameObject m_ballPrefab;
    public float m_ballRadius;
    private DragBall m_currentBall;

    //Ball physics variables
    [Range(0.0f, 10.0f)]
    [SerializeField] float m_ballGravity = 0.0f;
    [Range(1.0f, 10.0f)]
    [SerializeField] float m_ballVelocityMultiplyer = 5.0f;
    //LineRendering
    [SerializeField] LineRenderer m_lineRenderer;

    //Dragging
    private Vector2 m_initialDragPos;
    private bool m_isDragging;

    //AABB
    public Transform m_topLeft;
    public Transform m_bottomRight;
    // Start is called before the first frame update
    void Start()
    {
        m_ballRadius = m_ballPrefab.transform.localScale.x + 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !m_isDragging)
        {
            m_initialDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!CheckMouseValidPos(m_initialDragPos))
                return;
            m_currentBall = GameObject.Instantiate(m_ballPrefab, (Vector3)GetValidMousePosition(m_initialDragPos) + Vector3.back * 2f, Quaternion.identity, this.transform).GetComponent<DragBall>();

            Color ballColor = m_ballColors[Mathf.Min(Random.Range(0, m_ballColors.Length), m_ballColors.Length - 1)];
            m_currentBall.SetData(m_cameraShake, ballColor, this);

            m_isDragging = true;

            m_lineRenderer.enabled = true;
            m_lineRenderer.material.color = ballColor;
            m_lineRenderer.SetPosition(0, (Vector3)m_initialDragPos + Vector3.back);
            m_lineRenderer.SetPosition(1, (Vector3)m_initialDragPos + Vector3.back);
        }
        if (m_isDragging)
        {
            Vector2 validPos = GetValidMousePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector2 PosDiff = m_initialDragPos - validPos;
            m_currentBall.transform.position = (Vector3)validPos + Vector3.back * 2f;
            m_lineRenderer.SetPosition(1, (Vector3)validPos + Vector3.back);

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                m_isDragging = false;
                m_lineRenderer.enabled = false;
                if (PosDiff.magnitude > 0.3f)
                {
                    Rigidbody2D rb = m_currentBall.GetComponent<Rigidbody2D>();
                    m_currentBall.LaunchBall((PosDiff) * m_ballVelocityMultiplyer, m_ballGravity);
                } else
                {
                    m_currentBall.StartCoroutine("DestroyBall");
                }
            }
        }
    }

    private void OnDisable()
    {
        m_isDragging = false;
        m_currentBall = null;
        m_lineRenderer.enabled = false;
    }

    private Vector2 GetValidMousePosition(Vector2 mousePos)
    {
        return new Vector2(Mathf.Min(Mathf.Max(mousePos.x, m_topLeft.position.x + m_ballRadius), m_bottomRight.position.x - m_ballRadius),
            Mathf.Max(Mathf.Min(mousePos.y, m_topLeft.position.y - m_ballRadius), m_bottomRight.position.y + m_ballRadius));
    }

    private bool CheckMouseValidPos(Vector2 mousePos)
    {
        bool valid = false;
        valid = mousePos.x > (m_topLeft.position.x + m_ballRadius) && mousePos.x < (m_bottomRight.position.x - m_ballRadius);
        return valid && (mousePos.y < (m_topLeft.position.y - m_ballRadius) && mousePos.y > (m_bottomRight.position.y + m_ballRadius));
    }
}
