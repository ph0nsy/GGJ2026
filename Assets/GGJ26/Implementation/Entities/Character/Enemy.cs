using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    [HideInInspector] public StateMachine m_fsm;
    [SerializeField] List<EStateId> States;
    
    PerceptionComponent m_perception;

    protected override void Awake()
    {
        base.Awake();
        m_fsm = new();
        m_perception = transform.GetComponent<PerceptionComponent>();
    }

    protected override void Start()
    {
        base.Start();
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

        altar?.altarProgress.souls += 1;
        
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
