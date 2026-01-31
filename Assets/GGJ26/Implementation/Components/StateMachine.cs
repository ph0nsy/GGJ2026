using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState Current { get; set; }
    Dictionary<EStateId, IState> m_states;

    public void Init(Dictionary<EStateId, IState> states, Transform parent)
    {
        foreach(var key in states.Keys) 
        {
            IState temp = states[key];
            temp.m_character = parent.GetComponent<Enemy>();
            m_states.Add(key, temp);
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
        // if(OwnsState(Hurt)) Delegates -= () => Current = Hurt;
    }
}
