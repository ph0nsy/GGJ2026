using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [HideInInspector] public StateMachine m_fsm;
    PerceptionComponent m_perception;

    void Awake()
    {
        m_fsm = new();
        m_perception = GetComponent<PerceptionComponent>();
    }
    
    void Start()
    {
        m_perception.Range = ((EnemyData)Data).awarenessRange;
    }

    void FixedUpdate()
    {
        m_fsm.FixedUpdate();
    }

    void Update()
    {
        m_fsm.Update();
    }
}
