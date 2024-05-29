using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{
    private float speed = 4f;
    private bool canSwitch = true;
    private Rigidbody rb;
    private GravityDirection currentGravityDirection = GravityDirection.Down;
    private SpriteRenderer spriteRenderer;
    private GameObject currentGravitySwap; // Reference to the current GravitySwap object
    private Animator animator; // Reference to the Animator component
    private bool isGrounded = true; // Track if the player is grounded
    public string SceneToLoad;
    public AudioSource walkSound;
    public AudioSource gravitySound;
    public AudioSource landSound;
    public AudioSource teleportSound;
    public AudioSource endTeleportSound;
    public AudioSource forcefieldSound;
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

        // Block rotation
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        if (canSwitch)
        {
            HandleGravitySwitching();
        }
        UpdateAnimationStates();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.zero;

        switch (currentGravityDirection)
        {
            case GravityDirection.Up:
                movement = new Vector3(-horizontalInput, 0, 0); // Invert X-axis movement
                walkSound.Play();
                break;
            case GravityDirection.Down:
                movement = new Vector3(horizontalInput, 0, 0);
                walkSound.Play();
                break;
            case GravityDirection.Left:
                movement = new Vector3(0, -horizontalInput, 0); // Move along Y-axis
                walkSound.Play();
                break;
            case GravityDirection.Right:
                movement = new Vector3(0, horizontalInput, 0); // Invert Y-axis movement
                walkSound.Play();
                break;
        }

        Vector3 newPosition = rb.position + movement * speed * Time.deltaTime;
        newPosition.z = 0;
        rb.MovePosition(newPosition);

        // Flip the sprite based on the direction of movement and gravity direction
        AdjustSpriteFlip(horizontalInput);

        animator.SetBool("Walking", horizontalInput != 0);
    }

    void HandleGravitySwitching()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SwitchGravity(new Vector3(0, 9.81f, 0), Quaternion.Euler(180, 0, 0), GravityDirection.Up);
            AdjustSpriteOrientation(GravityDirection.Up);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchGravity(new Vector3(9.81f, 0, 0), Quaternion.Euler(0, 0, -90), GravityDirection.Right);
            AdjustSpriteOrientation(GravityDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchGravity(new Vector3(-9.81f, 0, 0), Quaternion.Euler(0, 0, 90), GravityDirection.Left);
            AdjustSpriteOrientation(GravityDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwitchGravity(new Vector3(0, -9.81f, 0), Quaternion.Euler(0, 0, 0), GravityDirection.Down);
            AdjustSpriteOrientation(GravityDirection.Down);
        }
    }

    void SwitchGravity(Vector3 newGravity, Quaternion newRotation, GravityDirection newGravityDirection)
    {
        Physics.gravity = newGravity;
        transform.rotation = newRotation;
        currentGravityDirection = newGravityDirection;
        gravitySound.Play();
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
            endTeleportSound.Play();
            SceneManager.LoadScene(SceneToLoad);
        }
        if (other.CompareTag("GravitySwap"))
        {
            SwitchGravity(new Vector3(0, 9.81f, 0), Quaternion.Euler(180, 0, 0), GravityDirection.Up);
            AdjustSpriteOrientation(GravityDirection.Up);
            canSwitch = false;
            currentGravitySwap = other.gameObject;
            forcefieldSound.Play();
        }
        if (other.CompareTag("Switch"))
        {
            teleportSound.Play();
            Destroy(other.gameObject);
            if (currentGravitySwap != null)
            {
                Destroy(currentGravitySwap);
                currentGravitySwap = null;
                canSwitch = true;
            }
        }
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            landSound.Play();
            animator.ResetTrigger("Landing");
            animator.SetTrigger("Landing");
        }
    }

    void AdjustSpriteFlip(float horizontalInput)
    {
        switch (currentGravityDirection)
        {
            case GravityDirection.Up:
                if (horizontalInput > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (horizontalInput < 0)
                {
                    spriteRenderer.flipX = false;
                }
                break;
            case GravityDirection.Down:
                if (horizontalInput > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (horizontalInput < 0)
                {
                    spriteRenderer.flipX = true;
                }
                break;
            case GravityDirection.Left:
                if (horizontalInput > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (horizontalInput < 0)
                {
                    spriteRenderer.flipX = false;
                }
                break;
            case GravityDirection.Right:
                if (horizontalInput > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (horizontalInput < 0)
                {
                    spriteRenderer.flipX = false;
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GravitySwap"))
        {
            canSwitch = true;
        }
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void UpdateAnimationStates()
    {
        if (!isGrounded && rb.velocity.y < 0)
        {
            animator.ResetTrigger("Falling");
            animator.SetTrigger("Falling");
        }
    }
}
