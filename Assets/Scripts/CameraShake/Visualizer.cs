using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    private LineRenderer m_lineRenderer;
    private Vector3[] m_positions;
    private Vector3 m_initialPos;
    [SerializeField] CameraShake m_cameraShake;
    [SerializeField] bool m_horizontal = true;
    // Start is called before the first frame update
    void Start()
    {
        //Initialization
        m_lineRenderer = this.GetComponent<LineRenderer>();
        m_positions = new Vector3[m_lineRenderer.positionCount];

        m_initialPos = this.transform.position;

        for (int i = 0; i < m_lineRenderer.positionCount; i++)
        {
            m_positions[i] = m_initialPos - (m_horizontal ? Vector3.right : Vector3.up) * i * 2.75f / m_lineRenderer.positionCount;
        }
        m_lineRenderer.SetPositions(m_positions);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_horizontal)
        {
            for (int i = m_lineRenderer.positionCount - 1; i > 0; i--)
                m_positions[i] = new Vector3(m_positions[i].x, m_positions[i - 1].y, m_positions[i].z);

            m_positions[0] = new Vector3(m_positions[0].x, m_initialPos.y - m_cameraShake.PositionDiff.y , m_positions[0].z);
            this.transform.position = m_initialPos + Vector3.up * -m_cameraShake.PositionDiff.y;
        }
        else
        {
            for (int i = m_lineRenderer.positionCount - 1; i > 0; i--)
                m_positions[i] = new Vector3(m_positions[i - 1].x, m_positions[i].y, m_positions[i].z);

            m_positions[0] = new Vector3(m_initialPos.x - m_cameraShake.PositionDiff.x, m_positions[0].y, m_positions[0].z);
            this.transform.position = m_initialPos + Vector3.right * -m_cameraShake.PositionDiff.x;
        }

        m_lineRenderer.SetPositions(m_positions);
    }
}
