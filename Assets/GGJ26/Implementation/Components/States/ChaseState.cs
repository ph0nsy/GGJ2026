using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : IState
{      
    [HideInInspector] public Enemy m_character { get; set; }

    PerceptionComponent m_perception;

    HitComponent m_hitBox;

    MoveComponent m_movement;

    bool playerDetected = false;

    Collider2D col;
    void playerDetection(bool bPlayer) { playerDetected = bPlayer; }

    public void Enter() 
    {
        m_perception = m_character.transform.GetComponent<PerceptionComponent>();
        m_movement = m_character.transform.GetComponent<MoveComponent>();
        m_hitBox = m_character.transform.GetComponent<HitComponent>();

        m_perception.OnPlayerDetected += playerDetection;
    }
    public void Exit()
    {
        m_perception.OnPlayerDetected -= playerDetection;
    }
    public void PhysicsUpdate()
    {     
    }

    public void GameplayUpdate() 
    {
        m_movement.targetLoc = Player.Instance.transform.position;
    }
    public void EvaluateChange() 
    {
        if (!playerDetected && m_character.m_fsm.OwnsState(EStateId.Roam))
        {
            m_character.m_fsm.ChangeState(EStateId.Roam);
        }
        else if(playerDetected && IsTargetInRangeOfAttack() && m_character.m_fsm.OwnsState(EStateId.Attack))
        {
            m_character.m_fsm.ChangeState(EStateId.Attack);
        }

    }

    bool IsTargetInRangeOfAttack()
    {
       return (m_movement.targetLoc-m_character.transform.position).magnitude <= m_hitBox.Range;
    }
}
