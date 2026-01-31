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
        m_distance = (transform.position - Player.Instance.transform.position).magnitude;
        if(!m_aware && m_distance <= Range)
        {
            m_aware = true;
            OnPlayerDetected?.Invoke(m_aware);
        }
        if(m_aware && m_distance > Range)
        {
            m_aware = false;
            OnPlayerDetected?.Invoke(m_aware);
        }
    }
}