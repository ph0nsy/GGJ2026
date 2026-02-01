using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject targetPlayer;

    public Vector2 deadzoneSize = new Vector2(0.5f, 0.5f);
    public float smoothTime = 0.2f;

    private Vector3 velocity;
    private Vector4 deadzone;

    void LateUpdate()
    {
        Vector3 camPos = transform.position;
        Vector3 targetPos = targetPlayer.transform.position;

        // Deadzone
        deadzone.x = camPos.x - (deadzoneSize.x * 0.5f);
        deadzone.y = camPos.x + (deadzoneSize.x * 0.5f);
        deadzone.z = camPos.y - (deadzoneSize.y * 0.5f);
        deadzone.w = camPos.y + (deadzoneSize.y * 0.5f);

        Vector3 desiredPos = camPos;

        // Horizontal Scroll
        if (targetPos.x < deadzone.x)
        {
            desiredPos.x = targetPos.x + deadzoneSize.x * 0.5f;
        }
        else if (targetPos.x > deadzone.y)
        {
            desiredPos.x = targetPos.x - deadzoneSize.x * 0.5f;
        }

        // Vertical Scroll
        if (targetPos.y < deadzone.z)
        {
            desiredPos.y = targetPos.y + deadzoneSize.y * 0.5f;
        }
        else if (targetPos.y > deadzone.w)
        {
            desiredPos.y = targetPos.y - deadzoneSize.y * 0.5f;
        }

        // Adjust with lag (frame-rate independent)
        transform.position = Vector3.SmoothDamp(
            camPos,
            new Vector3(desiredPos.x, desiredPos.y, camPos.z),
            ref velocity,
            smoothTime
        );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector2 bounds = new Vector2(deadzoneSize.x, deadzoneSize.y);
        Gizmos.DrawWireCube(transform.position, bounds);
    }

}
