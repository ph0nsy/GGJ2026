using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance { get; private set; }

    PlayerControl m_control;
    [SerializeField] PlayerData playerProgress;
    public PlayerInput pi;

    void Awake()
    {
        if(!Instance) { Instance = this; }
    }

    void Start()
    {
        m_control.GetComponent<PlayerControl>();
    }

    void Update()
    {
        pi = m_control.ReadInput();

        m_movement.targetLoc = transform.position + new Vector3(pi.move.x, pi.move.y, 0f);

        if (pi.attackPressed)
        {
            Debug.Log("Attack");
        }

        if (pi.swapMaskPressed)
        {
            Debug.Log("Swap");
        }
    }
}
