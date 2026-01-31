using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInput
{
    public Vector2 move;
    public bool jumpPressed;
    public bool crouchPressed;
    public bool interactPressed;
    public bool attackPressed;
}

public class PlayerControl : MonoBehaviour
{
    public PlayerInput ReadInput()
    {
        return new PlayerInput
        {
            move = new Vector2(
                Input.GetJoystickNames().Length > 0 ? Input.GetAxisRaw("LeftStickX") : Input.GetAxisRaw("Horizontal"),
                Input.GetJoystickNames().Length > 0 ? Input.GetAxisRaw("LeftStickY") : Input.GetAxisRaw("Vertical")
            ),
            jumpPressed = Input.GetJoystickNames().Length > 0 ? Input.GetButtonDown("A") : Input.GetButtonDown("Jump"),
            attackPressed = Input.GetJoystickNames().Length > 0 ? Input.GetButtonDown("X") : Input.GetButtonDown("Fire1"),
            crouchPressed = Input.GetJoystickNames().Length > 0 ? Input.GetButtonDown("B") : Input.GetButtonDown("Fire2"),
            interactPressed = Input.GetJoystickNames().Length > 0 ? Input.GetButtonDown("Y") : Input.GetButtonDown("Fire3")
        };
    }
}

