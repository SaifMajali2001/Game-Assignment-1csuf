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
    public float attackCooldown = 0.5f; // Cooldown time between attacks
    private float lastAttackTime = 0f;

    [Header("Animation")]
    public Animator animator;
    public GameObject attackEffectPrefab;

    [Header("better jump")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private bool isGrounded;

    bool canAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }
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

       
        BetterJump();

        // Downward Attack (only when in air)
        if (Input.GetKeyDown(KeyCode.S) && !isGrounded && canAttack())
        {
            PerformDownwardAttack();
        }
        
    }
    void BetterJump()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void PerformDownwardAttack()
    {
        lastAttackTime = Time.time;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, -jumpForce * 0.8f);
        
        if (attackEffectPrefab != null)
        {
            // Set the rotation to make it horizontal
            Quaternion horizontalRotation = Quaternion.Euler(0, 0, -90);
            GameObject effect = Instantiate(attackEffectPrefab, attackPoint.position, horizontalRotation);
            Destroy(effect, 0.3f);
        }
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy: " + enemy.name);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, pogoForce);
            break;
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