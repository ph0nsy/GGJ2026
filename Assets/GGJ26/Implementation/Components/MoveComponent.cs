using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public float KnockbackAmount;
    [HideInInspector] public float MoveSpeed { get; set; }

    [HideInInspector] public Vector2 m_direction { get; set; }
    Vector3 moveVector = new Vector3(0, 0, 0);

    private void Start()
    {
        GetComponent<DamageComponent>().OnDamageKnockback += Knockback;
    }

    void Update()
    {
        moveVector.x = m_direction.x * MoveSpeed * Time.deltaTime;
        moveVector.y = m_direction.y * MoveSpeed * Time.deltaTime;
        transform.position += moveVector;
    }

    private void Knockback(float strength, Vector2 direction)
    {
        if (strength <= 0.0f) { return; }

        moveVector.x = direction.x * strength * Time.deltaTime;
        moveVector.y = direction.y * strength * Time.deltaTime;
        transform.position += moveVector; 
    }


}
