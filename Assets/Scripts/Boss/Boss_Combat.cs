using UnityEngine;

public class Boss_Combat : Entity_Combat
{
    public float basicAttackKnockback = 4;
    public float contactKnockback = 8;

    public void ContactAttack(Collider2D targetCollision, AttackData attackData)
    {
        if (IsOpponent(targetCollision))
        {
            Entity_Health entityHealth = targetCollision.gameObject.GetComponent<Entity_Health>();
            float attackDir = transform.position.x < targetCollision.transform.position.x ? 1 : -1;
            isHitOpponent = entityHealth.TakeDamage(attackData, attackDir, targetCollision);
        }
    }
}
