using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInput
{
    public Vector2 move;
    public Vector2 look;
    public bool swapMaskPressed;
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
            look = new Vector2(
                Input.GetJoystickNames().Length > 0 ? Input.GetAxisRaw("RightStickX") : Input.GetAxisRaw("Mouse X"),
                Input.GetJoystickNames().Length > 0 ? Input.GetAxisRaw("RightStickY") : Input.GetAxisRaw("Mouse Y")
            ),
            swapMaskPressed = Input.GetButtonDown("Fire2"),
            attackPressed = Input.GetButtonDown("Fire1"),
        };
    }
}

