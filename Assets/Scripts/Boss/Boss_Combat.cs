using UnityEngine;

public class Boss_Combat : Entity_Combat
{
    [SerializeField] protected float basicAttackKnockback = 4;
    [SerializeField] protected float contactKnockback = 8;

    public float BasicAttackKnockback => basicAttackKnockback;
    public float ContactKnockback => contactKnockback;

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
