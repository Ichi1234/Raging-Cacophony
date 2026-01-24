using UnityEngine;

public class Player_Combat : Entity_Combat
{
    public override void PerformAttack(Collider2D targetCollision)
    {
        base.PerformAttack(targetCollision);

        if (isHitOpponent && IsOpponent(targetCollision))
        {
            KnockBackIfHitOpponent(currentAttackData);
        }
    }

    private void KnockBackIfHitOpponent(AttackData attackData)
    {
        Debug.Log("Im here?");
        Player player = GetComponent<Player>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (player.isGround && attackData.playerAttackType == PlayerAttackTypes.Basic)
        {
            Debug.Log("BASIC ATtACK");
            player.SetVelocity(attackData.knockback * -player.facingDir, rb.linearVelocity.y);
        }

        else if (attackData.playerAttackType == PlayerAttackTypes.Down)
        {
            Debug.Log("Hopping time");
            player.SetVelocity(rb.linearVelocity.x, attackData.knockback);
        }
    }
}
