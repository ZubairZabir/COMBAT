using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayers;

    [Header("Animation")]
    public Animator animator;

    Rigidbody2D rb;
    bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");  // A/D or Arrow keys

        // Move
        rb.linearVelocity = new Vector2(x * moveSpeed, rb.linearVelocity.y);

        // Animator speed parameter
        if (animator) animator.SetFloat("Speed", Mathf.Abs(x));

        // Flip sprite
        if (x > 0 && !facingRight) Flip();
        if (x < 0 && facingRight) Flip();

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SPACE pressed");
            if (IsGrounded())
            {
                Debug.Log("Grounded → jumping");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                if (animator) animator.SetTrigger("Jump");
            }
            else
            {
                Debug.Log("Not grounded");
            }
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayers);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
