using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Refs")]
    public Animator animator;
    public Transform attackPoint;

    [Header("Attack")]
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 25;
    public float attacksPerSecond = 2f;

    float nextAttackTime;

    void Update()
    {
        // press J to attack (swap to Fire1/Mouse0 if you want)
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attack");
            nextAttackTime = Time.time + 1f / attacksPerSecond;

            // if you didn't add an Animation Event:
            // DoDamage();
        }
    }

    // called by the Animation Event on the Attack clip
    public void DoDamage()
    {
        var hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var h in hits)
        {
            var hp = h.GetComponent<EnemyHealth>();
            if (hp != null) hp.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!attackPoint) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
