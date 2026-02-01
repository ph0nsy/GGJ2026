using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public EEntityType Type;
    public EAttackType Atype;
    public AltarData altarProgress;
    // Start is called before the first frame update
    void Start()
    {
        PairTypeToAttack();
    }

    // Update is called once per frame
    void Update()
    {
        if(altarProgress.souls >= altarProgress.threshold && !Player.Instance.masksUnlocked.Contains(Atype))
        {
            Player.Instance.playerProgress.maskUnlocked.Insert(Atype);
        }
    }

    public EAttackType PairTypeToAttack()
    {
         switch (Type)
        {
            case EEntityType.lion:
                Atype = EAttackType.Half;
                break;
            case EEntityType.bot:
                Atype = EAttackType.Line;
                break;
            case EEntityType.fiddle:
                Atype = EAttackType.Quarter;
                break;
        }
        return Atype;
    }

}
