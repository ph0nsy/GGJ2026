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
    }

    void Update()
    {
        pi = m_control.ReadInput();
        m_movement.Move(pi.move);

        if (pi.attackPressed)
        {

        }

        if (pi.swapMaskPressed)
        {

        }
    }
}
