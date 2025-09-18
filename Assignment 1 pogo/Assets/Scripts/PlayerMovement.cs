using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody2D rb;  // Changed to Rigidbody2D
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Changed to Rigidbody2D
    }

    void Update()
    {
        // Changed to 2D physics check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        float moveX = Input.GetAxis("Horizontal");
        // Removed moveZ since 2D games typically only move left/right

        Vector2 newVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = newVelocity;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);  // Changed to Vector2 and ForceMode2D
        }
    }
}