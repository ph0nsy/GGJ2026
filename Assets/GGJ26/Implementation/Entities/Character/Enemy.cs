using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] StateMachine m_fsm;
    PerceptionComponent m_perception;

    void Awake()
    {    
        m_perception = GetComponent<PerceptionComponent>();
    }
    
    void Start()
    {
        m_perception.Range = ((EnemyData)Data).awarenessRange;
    }

    void Update()
    {
        // Handle FSM
    }
}
