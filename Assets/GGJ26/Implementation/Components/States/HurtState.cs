using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : IState
{      
    [HideInInspector] public Enemy m_character { get; set; }

    PerceptionComponent m_perception;
    MoveComponent m_movement;

    bool playerDetected = false;

    Vector2 areaRange = new Vector2(2.0f, 5.0f);
    Collider2D[] cols;
    float area = 0.0f;
    void playerDetection(bool bPlayer) 
    {
        playerDetected = bPlayer;
        Debug.Log("Hurt state -> player" + (bPlayer ? "detected" : "lost"));
    }

    public void Enter() 
    {
        m_perception = m_character.transform.GetComponent<PerceptionComponent>();
        m_movement = m_character.transform.GetComponent<MoveComponent>();

        m_perception.OnPlayerDetected += playerDetection;
    }
    
    public void Exit()
    {
        m_perception.OnPlayerDetected -= playerDetection;
    }
    public void PhysicsUpdate() 
    {
        area = Random.Range(areaRange.x, areaRange.y);
        cols = Physics2D.OverlapCircleAll(
            m_character.transform.position,
            area,
            LayerMask.GetMask("Walls")
            );
    }

    public void GameplayUpdate() 
    {
        Vector3 sum = new Vector3(0, 0, 0);
        if (cols.Length > 0)
        {
            foreach (var col in cols)
            {
                sum.x += col.transform.position.x;
                sum.y += col.transform.position.x;
            }
            m_movement.Move(m_character.transform.position + (-sum.normalized * area));
            return;
        }
        sum.x = Random.Range(areaRange.x, areaRange.y);
        sum.y = Random.Range(areaRange.x, areaRange.y);
        m_movement.Move(m_character.transform.position + (sum.normalized * area));
    }
    public void EvaluateChange() 
    {
        if (playerDetected && m_character.m_fsm.OwnsState(EStateId.Chase))
        {
            m_character.m_fsm.ChangeState(EStateId.Chase);
        }
    } 
}
