using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [HideInInspector]
    public int MaxHP { get; set; }
    [HideInInspector]
    public int CurrentHP { get; set; }
    [HideInInspector]
    public bool bInvulnerable { get; private set; }
    [SerializeField]
    float InvulnerabilityCount = 0.5f;

    float m_invulnTimer = 0.0f;

    public event Action<int, bool> OnHealthChanged;
    public event Action OnDeath;
    public event Action OnLostInvulnerable;

    public void Init(int _maxHP, int _currentHP)
    {
        MaxHP = _maxHP;
        CurrentHP = _currentHP;
    }

    void Update()
    {
        if (!bInvulnerable) { return; }

        m_invulnTimer -= Time.deltaTime;
        if (m_invulnTimer <= 0) 
        {
            bInvulnerable = false;
            OnLostInvulnerable?.Invoke();
        }
    }

    public void Damage(int _amount)
    {
        if (bInvulnerable || _amount < 1) { return; }

        CurrentHP -= _amount;
        CurrentHP = Mathf.Max(0, CurrentHP);

        OnHealthChanged?.Invoke(CurrentHP, false);

        bInvulnerable = true;
        m_invulnTimer = InvulnerabilityCount;

        if (CurrentHP == 0) { OnDeath?.Invoke(); }
    }

    public void Heal(int amount)
    {
        if (CurrentHP <= 0) return;

        CurrentHP = Mathf.Min(CurrentHP + amount, MaxHP);
        OnHealthChanged?.Invoke(CurrentHP, true);
    }

}
