using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionComponent : MonoBehaviour
{
    [HideInInspector] public float Range { get; set; }
    float m_distance = 0.0f;
    bool m_aware = false;

    public event Action<bool> OnPlayerDetected;

    void Update()
    {
        Vector3 dir = (transform.position - Player.Instance.transform.position);
        m_distance = dir.magnitude;
        if (m_aware && m_distance > Range)
        {
            m_aware = false;
            OnPlayerDetected?.Invoke(m_aware);
            return;
        }

        RaycastHit2D[] cols = Physics2D.RaycastAll(
            transform.position,
            -dir.normalized,
            m_distance, 
            LayerMask.GetMask("Obstacle", "Player"));

        foreach (var col in cols)
        {
            if(col.transform.TryGetComponent(out Player player))
            {
                if (!m_aware && m_distance <= Range)
                {
                    m_aware = true;
                    OnPlayerDetected?.Invoke(m_aware);
                    return;
                }
            }
            else if (m_aware)
            {
                m_aware = false;
                OnPlayerDetected?.Invoke(m_aware);
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Player.Instance == null)
            return;

        Vector3 dir = transform.position - Player.Instance.transform.position;
        float distance = dir.magnitude;

        Gizmos.color = Color.cyan;

        Matrix4x4 oldMatrix = Gizmos.matrix;

        Gizmos.matrix =
            Matrix4x4.TRS(
                transform.position,
                Quaternion.Euler(0, 0, 0),
                Vector3.one
            );

        Gizmos.DrawLine(Vector3.zero, -dir.normalized * dir.magnitude);

        Gizmos.matrix = oldMatrix;

        Gizmos.color = m_aware ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}