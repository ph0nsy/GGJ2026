using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [HideInInspector] public int AttackDmg { get; set; }
    [HideInInspector] public int CollisionDmg { get; set; }

    public event Action<float, Vector2> OnDamageKnockback;

    Collider2D m_collider;

    void Start()
    {
        m_collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            player.GetComponent<HealthComponent>().Damage(CollisionDmg);
            OnDamageKnockback?.Invoke(0.25f * AttackDmg, (player.transform.position - transform.position).normalized);
        }
    }

    private void DealAttackDamage(Collider2D[] targets)
    {
        foreach (Collider2D tgt in targets)
        {
            if (tgt.transform.TryGetComponent(out HealthComponent Hp))
            {
                Hp.Damage(CollisionDmg);
                OnDamageKnockback?.Invoke(0.5f * AttackDmg, (tgt.transform.position - transform.position).normalized);
            }
        }
    }
}
