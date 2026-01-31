using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="CharacterData", menuName ="CharacterData/Default")]
public class CharacterData : ScriptableObject
{
    public int maxHealth = 1;
    public EAttackType attackType = EAttackType.None;
    public float attackRange = 1.0f;
    public float attackCooldown = 0.5f;
    public int attackDamage = 1;
    public int collisionDamage = 1;
    public EAttackType[] typesWeakness;
    public float moveSpeed = 1.5f;
}
