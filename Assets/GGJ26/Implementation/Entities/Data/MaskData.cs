using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaskData", menuName = "CharacterData/Mask")]
public class MaskData : ScriptableObject
{
    public EAttackType attackType = EAttackType.None;
    public float attackRange = 1.0f;
    public float attackCooldown = 0.5f;
    public int attackDamage = 1;
}
