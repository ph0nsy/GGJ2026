using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public float KnockbackAmount;
    [HideInInspector] public float MoveSpeed { get; set; }
    Vector3 moveVector = new Vector3(0, 0, 0);
    Vector3 targetVector = new Vector3(0, 0, 0);

    private void Start()
    {
        GetComponent<DamageComponent>().OnDamageKnockback += Knockback;
    }
    public void Move(Vector2 direction)
    {
        moveVector.x = direction.x * MoveSpeed * Time.deltaTime;
        moveVector.y = direction.y * MoveSpeed * Time.deltaTime;
        transform.position += moveVector;
    }

    public void Move(Vector3 targetLocation, bool bOverride)
    {
        float dist = Vector3.Distance(targetVector, transform.position);
        if (dist < 1.0f || dist > 10.0f || bOverride)
        {
            targetVector = targetLocation;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetVector, MoveSpeed * Time.deltaTime);
    }

    private void Knockback(float strength, Vector2 direction)
    {
        if (strength <= 0.0f) { return; }

        moveVector.x = direction.x * strength * Time.deltaTime;
        moveVector.y = direction.y * strength * Time.deltaTime;
        transform.position += moveVector; 
    }


}
