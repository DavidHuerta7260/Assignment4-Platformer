using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    // Player movement speed
    public float moveSpeed = 5f;
    public float jumpForce = 10f; // Make jumpForce public or use SerializeField

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;

    public AudioClip jumpSound;

    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAudio = GetComponent<AudioSource>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck not assigned to the player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Update isGrounded first before checking jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if (playerAudio != null && jumpSound != null)
            {
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}

