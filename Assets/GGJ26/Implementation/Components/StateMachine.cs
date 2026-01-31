using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    IState Current { get; set; }
    [SerializeField] Dictionary<EStateId, IState> m_states;

    public void Init()
    {
        // if(OwnsState(Hurt)) Delegates += () => Current = Hurt;
        // if(OwnsState(Stun)) Delegates += () => Current = Stun;
        // if(OwnsState(Spawn)) Delegates += () => Current = Spawn;
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
}
