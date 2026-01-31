using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CharacterData/Player")]
public class PlayerData : ScriptableObject
{
    public EAttackType currentMask = EAttackType.None;
    public Dictionary<EAttackType, MaskData> maskList;
    public EAttackType[] maskUnlocked;
}
