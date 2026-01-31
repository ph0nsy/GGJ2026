using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData Data;

    Animator m_anim;
    BoxCollider2D m_collider;

    HealthComponent m_health;
    // HitboxComponent m_hitbox;
    DamageComponent m_damage;
    MoveComponent m_movement;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_collider = GetComponent<BoxCollider2D>();

        m_health = GetComponent<HealthComponent>();
        // m_hitbox = GetComponent<HitboxComponent>();
        m_damage = GetComponent<DamageComponent>();
        m_movement = GetComponent<MoveComponent>();
    }

    void Start()
    {
        m_health.MaxHP = Data.maxHealth;
        m_health.CurrentHP = Data.maxHealth;
        // m_hitbox.Type = Data.attackType;
        // m_hitbox.Range = Data.attackRange;
        // m_hitbox.Cooldown = Data.attackCooldown;
        m_damage.AttackDmg = Data.attackDamage;
        m_damage.CollisionDmg = Data.collisionDamage;
        m_movement.MoveSpeed = Data.moveSpeed;
    }
}
