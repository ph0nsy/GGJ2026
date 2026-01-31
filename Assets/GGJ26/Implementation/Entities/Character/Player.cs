using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] PlayerData playerProgress;
    PlayerControl m_control;

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
        PlayerInput pi = m_control.ReadInput();

        // Handle Input
        
    }
}
