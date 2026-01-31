using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player Instance { get; private set; }

    PlayerControl m_control;
    [SerializeField] PlayerData playerProgress;
    public PlayerInput pi;

    protected override void Awake()
    {
        base.Awake();
        if (!Instance) { Instance = this; }
    }

    protected override void Start()
    {
        base.Start();
        m_control = GetComponent<PlayerControl>();
        Debug.Log("PlayerClass.Start()");
    }

    void Update()
    {
        pi = m_control.ReadInput();

        m_movement.targetLoc = transform.position + new Vector3(pi.move.x, pi.move.y, 0f);
        Debug.Log("Semen:" + m_movement.targetLoc);

        if (pi.attackPressed)
        {

        }

        if (pi.swapMaskPressed)
        {

        }
    }
}
