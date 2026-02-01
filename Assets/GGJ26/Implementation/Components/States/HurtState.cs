using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : IState
{      
    [HideInInspector] public Enemy m_character { get; set; }

    HealthComponent m_health;
    Animator m_animator;

    bool bDead = false;
    bool bAnimDone = false;

    void EnemyDeath() 
    {
        bDead = true;
    }

    public void Enter() 
    {
        m_health = m_character.transform.GetComponent<HealthComponent>();
        m_animator = m_character.transform.GetComponent<Animator>();

        m_animator.Play("Hurt"); 
        m_health.OnDeath += EnemyDeath;
    }
    
    public void Exit()
    {
        m_health.OnDeath -= EnemyDeath;
        if (bDead)
        { 
            m_animator.Play("Death");
            float deathAnimLength = m_animator.GetCurrentAnimatorStateInfo(0).length;
            Object.Destroy(m_character.gameObject, deathAnimLength);
        }
    }

    public void PhysicsUpdate() { }

    public void GameplayUpdate() 
    {
        AnimatorStateInfo state = m_animator.GetCurrentAnimatorStateInfo(0);
        if (state.IsName("Hurt") && state.normalizedTime >= 1f)
        {
            bAnimDone = true;
        }
    }

    public void EvaluateChange() 
    {
        if (bAnimDone && m_character.m_fsm.OwnsState(EStateId.Chase))
        {
            m_character.m_fsm.ChangeState(EStateId.Chase);
        }
    } 
}
