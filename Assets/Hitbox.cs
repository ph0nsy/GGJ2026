using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    Vector3 mouseDirection;
    float range;
    float angle;
    float width;

    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        //if (collider is CircleCollider2D) {
        //    collider?.radius = RANGE;
        //}
        //if (collider is BoxCollider2D) {
        //    collider?.size.set(WIDTH, RANGE)
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GetComponent<Collider2D>() is CircleCollider2D) {
            checkArchHitBox(other.gameObject.getComponent.offset, mouseDirection, range, angle);
        }
        else if (GetComponent<Collider2D>() is SquareCollider2D){
            /// YOU ARE ALREADY INSIDE IDIOT
        }
    }


    bool checkArchHitBox(Vector3 otherPosition, Vector3 direction; float radius, float angle){
        Vector3 relativeOtherPos = otherPosition - GetComponent<Collider2D>().offset;

        Vector2 polarRelOther = getPolarCoordinates(relativeOtherPos);
    
        Vector2 polarDirection = getPolarCoordinates(direction.normalized);
        Vector2 Aa = new Vector2(radius, polarDirection+(angle/2));
        Vector2 Bb = new Vector2(0, polarDirection-(angle/2));

        return checkInsideBox(Aa, Bb, polarRelOther);
    }



    Vector2 getPolarCoordinates(Vector3 euclidCoord){
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

    bool checkInsideBox(Vector2 Aa, Vector2 Bb, Vector2 position){
        return (position.x < Aa.x && position.x > Bb.x) && 
        (position.y < Aa.y && position.y > Bb.y);
    }

    Vector3 getMouseDirection(){
        return mouseDirection;
    }

    void setMouseDirection(Vector3 direction){
        mouseDirection = direction.normalized;
    }

    void setMouseDirectionWithPositions(Vector3 mousePosition){
        mouseDirection = mousePosition - GetComponent<Collider2D>().offset;
        mouseDirection = mouseDirection.normalized; 
    }

}
