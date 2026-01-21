using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    [Header("Attack Details")]
    public float attackDamage = 10;
    public float attackCooldown = 1;
    public Transform attackPosition;

    public Vector3 attackHitboxSize;
    public Vector3 attackHitboxOffset;


    public void PerformAttack(Collider2D targetCollision)
    {
        if (IsOpponent(targetCollision))
        {
            Entity_Health entityHealth = targetCollision.gameObject.GetComponent<Entity_Health>();
            entityHealth.ReduceHealth(attackDamage);
        }
    }

    private bool IsOpponent(Collider2D targetCollision)
    {
        return targetCollision.gameObject.layer == LayerMask.NameToLayer("Boss") 
            || targetCollision.gameObject.layer == LayerMask.NameToLayer("Player") 
            && targetCollision.gameObject.layer != gameObject.layer;
    }
}
