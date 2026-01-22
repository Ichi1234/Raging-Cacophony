using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    [Header("Attack Details")]
    public float attackDamage = 10;
    public float attackCooldown = 1;
    public Transform attackPosition;
    public float basicAttackKnockback = 4;
    public float contactKnockback = 8;


    public Vector3 attackHitboxSize;
    public Vector3 attackHitboxOffset;


    public void PerformAttack(Collider2D targetCollision, float knockback)
    {
        if (IsOpponent(targetCollision))
        {
            Entity_Health entityHealth = targetCollision.gameObject.GetComponent<Entity_Health>();
            float attackDir = transform.position.x < targetCollision.transform.position.x ? 1 : -1;
            entityHealth.TakeDamage(attackDamage, attackDir, knockback, targetCollision);
        }
    }

    private bool IsOpponent(Collider2D targetCollision)
    {
        return targetCollision.gameObject.layer == LayerMask.NameToLayer("Boss") 
            || targetCollision.gameObject.layer == LayerMask.NameToLayer("Player") 
            && targetCollision.gameObject.layer != gameObject.layer;
    }
}
