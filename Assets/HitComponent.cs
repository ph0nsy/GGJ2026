using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitComponent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Collider2D[] getCollidersInBeam(Vector3 source, float range, float width, Vector3 direction, string mask){
        
        float angle = getPolarCoordinates(direction).y;
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            source + (range/2) * direction.normalized,
            new Vector2(range, width),
            angle,
            LayerMask.GetMask(mask)
        );
        return hits;
    }

    public Collider2D[] getCollidersInArch(Vector3 source, float range, float angle, Vector3 direction, string mask)
    {
        List<Collider2D> hits = new List<Collider2D>();
        Collider2D[] aux = Physics2D.OverlapCircleAll(
            source,
            range,
            LayerMask.GetMask(mask)
        );

        foreach (Collider2D target in aux)
        {
            if (checkArchHitBox(target.transform.position, source, direction, range, angle)){
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
