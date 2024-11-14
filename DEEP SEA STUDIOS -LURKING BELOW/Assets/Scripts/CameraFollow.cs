using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform hook; // Reference to the hook object
    public float smoothSpeed = 0.125f; // Speed of camera movement
    public Vector3 offset; // Offset to keep a fixed distance from the hook

    void LateUpdate()
    {
        if (hook != null)
        {
            // Define target position based on hook position and offset
            Vector3 targetPosition = hook.position + offset;
            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
