using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    public Collider2D hitCollider;

    void Awake()
    {
        currentHealth = maxHealth;
        if (!hitCollider) hitCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (animator) animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (animator) animator.SetBool("IsDead", true);
        if (hitCollider) hitCollider.enabled = false;
        // Destroy(gameObject, 1f); // optional
    }
}
