using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurCam : MonoBehaviour
{
    private Vector3 initialPosition;
    public Transform playerTransform; // Reference to the player's transform
    public float rotationSmoothTime = 0.3f; // Smooth time for rotation transition
    private Quaternion targetRotation;
    private Quaternion originalRotation;
    private float zRotationVelocity;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not assigned!");
        }

        // Store the initial rotation of the camera
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (playerTransform == null)
            return;

        // Calculate the target rotation by mirroring the player's Z rotation
        float playerZRotation = playerTransform.eulerAngles.z;
        float targetZRotation = -playerZRotation;

        // Smoothly interpolate between the current Z rotation and the target Z rotation
        float newZRotation = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetZRotation, ref zRotationVelocity, rotationSmoothTime);

        // Apply the new rotation to the camera, preserving the original X and Y rotations
        transform.rotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y, newZRotation);
    }
}