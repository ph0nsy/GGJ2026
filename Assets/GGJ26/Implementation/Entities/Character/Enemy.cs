using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Dictionary<EStateId, IState> States;

    [HideInInspector] 
    public StateMachine m_fsm;
    
    PerceptionComponent m_perception;

    void Awake()
    {
        m_fsm = new();
        m_perception = GetComponent<PerceptionComponent>();
    }
    
    void Start()
    {
        m_fsm.Init(States, transform);
        m_perception.Range = ((EnemyData)Data).awarenessRange;
        m_health.OnDeath += updateAltarCount;
    }

    Altar getAltarOfSameType()
    {
        foreach (Altar altar in FindObjectsOfType<Altar>())
        {
            if (altar.Type == Type)
            {
                return altar;
            }
        }
        return null;
    }

    void updateAltarCount()
    {
        Altar altar= getAltarOfSameType();

        altar?.altarProgress.souls++;
        
        if(altar?.altarProgress.souls == altar?.altarProgress.threshold){
            foreach (Altar a in FindObjectsOfType<Altar>())
            {
                if (a.Type != Type && !Player.Instance.playerProgress.maskUnlocked.Contains(a.PairTypeToAttack()))
                {
                    a.altarProgress.threshold*=2;
                }
            }
        }

    }

    void FixedUpdate()
    {
        m_fsm.FixedUpdate();
    }

    void Update()
    {
        m_fsm.Update();
    }

    void OnDestroy()
    {
        m_fsm.OnDestroy();    
    }
}
