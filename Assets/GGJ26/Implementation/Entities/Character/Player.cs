using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance { get; private set; }

    PlayerControl m_control;
    public PlayerData playerProgress;
    public PlayerInput pi;

    protected override void Awake()
    {
        base.Awake();
        if (!Instance) { Instance = this; }
    }

    protected override void Start()
    {
        base.Start();
        m_control = GetComponent<PlayerControl>();
        m_health.OnDeath += Die;
        m_health.OnHealthChanged += ;
    }

    void Hurt(int ammount, bool bDamage)
    {
        if (bDamage)
        {
            m_anim.Play("Hurt");
        }
    }

    void Die()
    {
        m_anim.Play("Death");
        float deathAnimLength = m_anim.GetCurrentAnimatorStateInfo(0).length;
        Object.Destroy(transform.gameObject, deathAnimLength);
    }

    void Update()
    {
        pi = m_control.ReadInput();

        AnimatorStateInfo state = m_anim.GetCurrentAnimatorStateInfo(0);
        if ((state.IsName("Hurt") || state.IsName("Death"))&& state.normalizedTime < 1f) { return; }

        m_anim.Play("Walk");
        m_movement.Move(pi.move);
        if (pi.swapMaskPressed && playerProgress.maskUnlocked.Count > 0f)
        {
            playerProgress.currentMask = (playerProgress.currentMask + 1) % playerProgress.maskUnlocked.Count;
        }

        if (pi.attackPressed)
        {
            if (m_hitbox.bOverHeated) { return; }
            if (playerProgress.maskUnlocked.Count > 0f)
            {
                var mask = playerProgress.maskList[playerProgress.maskUnlocked[playerProgress.currentMask]];
                m_damage.AttackDmg = mask.attackDamage;
                m_hitbox.Range = mask.attackRange;
                m_hitbox.Cooldown = mask.attackCooldown;
                m_hitbox.Type = mask.attackType;
            } 
            else
            {
                m_hitbox.Type = Data.attackType;
                m_hitbox.Range = Data.attackRange;
                m_hitbox.Cooldown = Data.attackCooldown;
                m_damage.AttackDmg = Data.attackDamage;
            }
            m_damage.DealAttackDamage(m_hitbox.getColliders(transform.position, pi.look.normalized, "Enemy"));
        }
    }
}
