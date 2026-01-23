using UnityEngine;

public class Player_Health : Entity_Health
{
    public override bool TakeDamage(AttackData attackData, float attackDir, Collider2D targetCollision)
    {
        Entity_Vfx entityVfx = GetComponent<Entity_Vfx>();

        ReduceHealth(attackData.damage);
        entityVfx.CreateOnHitEffect();

        if (targetCollision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = targetCollision.GetComponent<Player>();
            player.KnockBack(attackDir, attackData.knockback);
        }

        return true;
    }
}
