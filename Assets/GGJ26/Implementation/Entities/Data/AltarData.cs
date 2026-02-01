using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="AltarData", menuName ="CharacterData/Altar")]
public class AltarData : ScriptableObject
{
    public int souls = 0;
    public int threshold = 15;
}
