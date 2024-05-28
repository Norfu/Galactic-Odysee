using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    private float speed = 4f;
    private bool canSwitch = true;
    private Rigidbody rb;
    private GravityDirection currentGravityDirection = GravityDirection.Down;
    private Transform cameraTransform;
    private Vector3 initialCameraOffset;
    private SpriteRenderer spriteRenderer;
    private GameObject currentGravitySwap; // Reference to the current GravitySwap object

    private enum GravityDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Bloquer la rotation
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mouvement horizontal (pas de mouvement vertical vu qu'on a la gravité)
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.zero;

        switch (currentGravityDirection)
        {
            case GravityDirection.Up:
                movement = new Vector3(-horizontalInput, 0, 0); // Invert X-axis movement
                break;
            case GravityDirection.Down:
                movement = new Vector3(horizontalInput, 0, 0);
                break;
            case GravityDirection.Left:
                movement = new Vector3(0, -horizontalInput, 0); // Move along Y-axis
                break;
            case GravityDirection.Right:
                movement = new Vector3(0, horizontalInput, 0); // Invert Y-axis movement
                break;
        }

        // Calculate new position
        Vector3 newPosition = rb.position + movement * speed * Time.deltaTime;

        // Lock the Z position
        newPosition.z = 0;

        // Apply the new position using Rigidbody
        rb.MovePosition(newPosition);

        if (canSwitch)
        {
            Gravity();
        };
    }

    void Gravity()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Gravity up (towards the ceiling)
            SwitchGravity(new Vector3(0, 9.81f, 0), Quaternion.Euler(180, 0, 0), GravityDirection.Up);
            AdjustSpriteOrientation(GravityDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Gravity right (towards the right wall)
            SwitchGravity(new Vector3(9.81f, 0, 0), Quaternion.Euler(0, 0, -90), GravityDirection.Right);
            AdjustSpriteOrientation(GravityDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Gravity left (towards the left wall)
            SwitchGravity(new Vector3(-9.81f, 0, 0), Quaternion.Euler(0, 0, 90), GravityDirection.Left);
            AdjustSpriteOrientation(GravityDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Gravity down (normal)
            SwitchGravity(new Vector3(0, -9.81f, 0), Quaternion.Euler(0, 0, 0), GravityDirection.Down);
            AdjustSpriteOrientation(GravityDirection.Down);
        }
    }

    void SwitchGravity(Vector3 newGravity, Quaternion newRotation, GravityDirection newGravityDirection)
    {
        // Apply new gravity and rotation
        Physics.gravity = newGravity;
        transform.rotation = newRotation;
        currentGravityDirection = newGravityDirection;
    }

    void AdjustSpriteOrientation(GravityDirection newGravityDirection)
    {
        switch (newGravityDirection)
        {
            case GravityDirection.Up:
            case GravityDirection.Down:
                spriteRenderer.flipY = false;
                break;
            case GravityDirection.Left:
            case GravityDirection.Right:
                spriteRenderer.flipY = true;
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blackhole"))
        {
            SceneManager.LoadScene("Menu");
        }
        if (other.CompareTag("GravitySwap"))
        {
            SwitchGravity(new Vector3(0, 9.81f, 0), Quaternion.Euler(180, 0, 0), GravityDirection.Up);
            AdjustSpriteOrientation(GravityDirection.Up);
            canSwitch = false;
            currentGravitySwap = other.gameObject; // Store the reference to the GravitySwap object
        }
        if(other.CompareTag("Switch"))
        {
            Destroy(other.gameObject);
            if (currentGravitySwap != null)
            {
                Destroy(currentGravitySwap); // Destroy the GravitySwap object
                currentGravitySwap = null; // Clear the reference
                canSwitch = true; // Allow gravity switching again
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GravitySwap"))
        {
            canSwitch = true; // Unlock gravity switching
        }
    }
}

