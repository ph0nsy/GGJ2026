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
        Collider2D[] cols = Physics2D.OverlapBoxAll(
            transform.position,
            new Vector2(m_distance / 4.0f, m_distance),
            Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, 
            LayerMask.GetMask("Obstacle", "Player"));
        
        foreach(var col in cols)
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
            if(m_aware && m_distance > Range)
            {
                m_aware = false;
                OnPlayerDetected?.Invoke(m_aware);
            }
        }
    }
}