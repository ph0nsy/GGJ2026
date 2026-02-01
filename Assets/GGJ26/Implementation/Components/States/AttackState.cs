using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IState
{      
    [HideInInspector] public Enemy m_character { get; set; }

    PerceptionComponent m_perception;

    HitComponent m_hitBox;

    DamageComponent m_damage;

    MoveComponent m_movement;

    List<Collider2D> emptyArray = new List<Collider2D>();

    Collider2D[] hits;

    public void Enter() 
    {
        m_perception = m_character.transform.GetComponent<PerceptionComponent>();
        m_movement = m_character.transform.GetComponent<MoveComponent>();
        m_hitBox = m_character.transform.GetComponent<HitComponent>();
        m_damage = m_character.transform.GetComponent<DamageComponent>();

        hits = emptyArray.ToArray();
    }

    public void Exit() { }

    public void PhysicsUpdate()
    {
        if (!m_hitBox.bOverHeated)
        {
            hits = m_hitBox.getCollidersInBeam(
                m_character.transform.position, 
                m_hitBox.getDirectionWithPositions(
                    Player.Instance.transform.position,
                    m_character.transform.position),
                "Player"
                );
        }
        
    }

    public void GameplayUpdate() 
    {
        m_damage.DealAttackDamage(hits);
        hits = emptyArray.ToArray();
    }
    public void EvaluateChange() 
    {
        if(!IsTargetInRangeOfAttack() && m_character.m_fsm.OwnsState(EStateId.Chase))
        {
            m_character.m_fsm.ChangeState(EStateId.Chase);
        }
    }

    bool IsTargetInRangeOfAttack()
    {
       return (Player.Instance.transform.position - m_character.transform.position).magnitude <= m_hitBox.Range;
    }
}
