using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CharacterData/Player")]
public class PlayerData : ScriptableObject
{
    public int currentMask = 0;
    public Dictionary<EAttackType, MaskData> maskList;
    public List<EAttackType> maskUnlocked;
}
