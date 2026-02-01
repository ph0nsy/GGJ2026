using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData Data;

    protected Animator m_anim;
    protected BoxCollider2D m_collider;
    protected Rigidbody2D m_rb;

    protected HealthComponent m_health;
    protected HitComponent m_hitbox;
    protected DamageComponent m_damage;
    protected MoveComponent m_movement;

    protected virtual void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_collider = GetComponent<BoxCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();

        m_health = GetComponent<HealthComponent>();
        m_hitbox = GetComponent<HitComponent>();
        m_damage = GetComponent<DamageComponent>();
        m_movement = GetComponent<MoveComponent>();
    }

    protected virtual void Start()
    {
        m_health.MaxHP = Data.maxHealth;
        m_health.CurrentHP = Data.maxHealth;
        m_hitbox.Type = Data.attackType;
        m_hitbox.Range = Data.attackRange;
        m_hitbox.Cooldown = Data.attackCooldown;
        m_damage.AttackDmg = Data.attackDamage;
        m_damage.CollisionDmg = Data.collisionDamage;
        m_movement.MoveSpeed = Data.moveSpeed;
    }
}
