using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private float rotationSpeed = 400f; // Rotation speed in degrees per second

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation amount for this frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Apply the rotation around the Z axis
        transform.Rotate(0, 0, rotationAmount);
    }
}
