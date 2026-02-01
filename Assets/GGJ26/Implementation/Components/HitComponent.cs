using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitComponent : MonoBehaviour
{

    [HideInInspector] public EAttackType Type { get; set; }
    [HideInInspector] public float Range { get; set; }
    [HideInInspector] public float Cooldown { get; set; }

    public GameObject attackSprite;

    public bool bOverHeated = false;

    float m_cooldownTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (!bOverHeated) { return; }

        m_cooldownTimer -= Time.deltaTime;
        if (m_cooldownTimer <= 0) 
        {
            bOverHeated = false;
        }
    }

    public Collider2D[] getColliders(Vector3 source, Vector3 direction, string mask)
    {
        switch (Type)
        {
            case EAttackType.Half:
            case EAttackType.Quarter:
            case EAttackType.Eigth:
            case EAttackType.None:
                return getCollidersInArch(source, direction, mask);
            case EAttackType.Line:
            default:
                return getCollidersInBeam(source, direction, mask);
        }
    }

    public Collider2D[] getCollidersInBeam(Vector3 source, Vector3 direction, string mask){
        float angle = getPolarCoordinates(direction).y;
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            source + Range/2 * direction.normalized,
            new Vector2(Range, getWidth()),
            angle,
            LayerMask.GetMask(mask)
        );
        m_cooldownTimer = Cooldown;
        return hits;
    }

    public Collider2D[] getCollidersInArch(Vector3 source, Vector3 direction, string mask)
    {
        List<Collider2D> hits = new List<Collider2D>();
        Collider2D[] aux = Physics2D.OverlapCircleAll(
            source,
            Range,
            LayerMask.GetMask(mask)
        );

        foreach (Collider2D target in aux)
        {
            if (checkArchHitBox(target.transform.position, source, direction, Range, getWidth())){
                hits.Add(target);
            }
        }

        return hits.ToArray();
    }


    private bool checkArchHitBox(Vector3 otherPosition, Vector3 center, Vector3 direction, float radius, float angle)
    {   
        // NO USAR OFFSET O ME CORTO LA POLLA
        Vector3 relativeOtherPos = otherPosition - center;
        Vector2 polarRelOther = getPolarCoordinates(relativeOtherPos);
    
        Vector2 polarDirection = getPolarCoordinates(direction.normalized);
        Vector2 Aa = new Vector2(radius, polarDirection.y+(angle/2));
        Vector2 Bb = new Vector2(0, polarDirection.y-(angle/2));

        return checkInsideBox(Aa, Bb, polarRelOther);
    }

    float getWidth()
    {
        Animator childAnimator = attackSprite.GetComponent<Animator>();

        switch (Type)
        {
            case EAttackType.Half:
                childAnimator.Play("Scratch");
                return Mathf.PI;
            case EAttackType.Quarter:
                childAnimator.Play("Fireblast");
                return Mathf.PI/2;
            case EAttackType.Eigth:
            case EAttackType.None:
                childAnimator.Play("BasicAttack");
                return Mathf.PI/4;
            case EAttackType.Line:
                childAnimator.Play("Blast");
                return Range/5;
            default:
                childAnimator.Play("BasicAttack");
                return 0;
        }
    }

    public Vector2 getPolarCoordinates(Vector3 euclidCoord)
    {
        float x = euclidCoord.x;
        float y = euclidCoord.y;
        float angle = Mathf.PI/2;
        Vector3 aux = new Vector3(x,y,0);
        float r = aux.magnitude;
        if (x==0 ){
            if (y<0){
                angle = -angle;
            }
        }
        else {
            angle = Mathf.Atan(y/x);
        }
        return new Vector2(r, angle); 

    }

    private bool checkInsideBox(Vector2 Aa, Vector2 Bb, Vector2 position){
        return (position.x < Aa.x && position.x > Bb.x) && (position.y < Aa.y && position.y > Bb.y);
    }



    public Vector3 getDirectionWithPositions(Vector3 targetPosition, Vector3 originPosition){
                // NO USAR OFFSET O ME CORTO LA POLLA

        return (targetPosition - originPosition).normalized;
    }

}
