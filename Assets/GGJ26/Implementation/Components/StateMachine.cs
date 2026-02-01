using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState Current { get; set; }
    Dictionary<EStateId, IState> m_states = new Dictionary<EStateId, IState>();
    Transform m_parent;

    public void Init(List<EStateId> states, Transform parent)
    {
        m_parent = parent;
        if(states.Count < 0) { return; }

        foreach (var key in states)
        {
            Debug.Log(key);
            IState temp = IdToState(key);
            temp.m_character = parent.GetComponent<Enemy>();
            if(m_states.Count <= 0) 
            { 
                m_states.Add(key, temp);
                ChangeState(key);
                continue;
            }
        }
        
        if (OwnsState(EStateId.Hurt))
        {
            parent.GetComponent<HealthComponent>().OnHealthChanged += HurtHijack;
        }
    }

    public void HurtHijack(int DamageDealt, bool bNotDamaged)
    {
        if(bNotDamaged) { return; }
        ChangeState(EStateId.Hurt);
    }

    public void ChangeState(EStateId _nextState)
    {
        Debug.Log("Helllooooooo");
        Current?.Exit();
        Current = m_states[_nextState];
        Current.Enter();
    }

    public bool OwnsState(EStateId _nextState)
    {
        return m_states.ContainsKey(_nextState);
    }

    public void FixedUpdate() => Current?.PhysicsUpdate();
    public void Update() 
    {
        Current?.GameplayUpdate();
        Current?.EvaluateChange();
    }

    public void OnDestroy()
    {
        m_parent.GetComponent<HealthComponent>().OnHealthChanged -= HurtHijack;
    }

    IState IdToState(EStateId id)
    {
        switch (id)
        {
            case EStateId.Attack: return new Attack();
            case EStateId.Chase: return new Chase();
            case EStateId.Hurt: return new Hurt();
            /*
            case EStateId.Spawn: return new Spawn();
            case EStateId.Stunned: return new Stunned();
            */
            default: return new Roam();
        }
    }


}
