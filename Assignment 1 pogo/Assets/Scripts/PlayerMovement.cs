using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Attack")]
    public float attackRange = 1f;
    public float pogoForce = 8f;  // How much you bounce up after hitting an enemy
    public LayerMask enemyMask;   // What counts as an enemy
    public Transform attackPoint; // Position where the attack happens (below player)

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        float moveX = Input.GetAxis("Horizontal");

        Vector2 newVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = newVelocity;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Downward Attack (only when in air)
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded)
        {
            PerformDownwardAttack();
        }
    }

    void PerformDownwardAttack()
    {
        // Add downward force to make attack feel more impactful
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -jumpForce * 0.8f);
        
        // Check for enemies below
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            // Damage the enemy (you'll implement this later)
            Debug.Log("Hit enemy: " + enemy.name);
            
            // Pogo bounce effect
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, pogoForce);
            
            break; // Only hit one enemy per attack
        }
    }

    // Visualize attack range in Scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
        
        if (attackPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}