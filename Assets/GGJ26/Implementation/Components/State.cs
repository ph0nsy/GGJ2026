using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{      
    public Enemy m_character { get; set; }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void PhysicsUpdate();
    public abstract void GameplayUpdate();
    public abstract void EvaluateChange();
}
