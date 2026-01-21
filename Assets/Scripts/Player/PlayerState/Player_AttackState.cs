using UnityEngine;

public class Player_AttackState : PlayerState
{
    private float attackPositionDistance = 0.94f;

    public Player_AttackState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        stateMachine.canChangeState = false;

        base.Enter();

        entityVfx.CreateAttackVfx();

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.canChangeState = true;
            stateMachine.ChangeState(player.idleState);
        }
    }

    protected void HandleAttackPosition(PlayerAttackTypes attackType)
    {
        Vector2 destinationPosition = Vector2.zero;

        switch (attackType)
        {
            case PlayerAttackTypes.Up:
                destinationPosition = new Vector2(0, attackPositionDistance);
                break;
            case PlayerAttackTypes.Basic:
                destinationPosition = new Vector2(attackPositionDistance, 0.1f);
                break;
            case PlayerAttackTypes.Down:
                destinationPosition = new Vector2(0, -attackPositionDistance);
                break;

        }

        player.entityCombat.attackPosition.transform.localPosition = destinationPosition;
    }

    protected void HandleAttackRotation(PlayerAttackTypes attackTypes)
    {

        switch (attackTypes)
        {
            case PlayerAttackTypes.Up:
                player.entityCombat.attackPosition.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case PlayerAttackTypes.Basic:
                player.entityCombat.attackPosition.transform.rotation = player.facingDir == 1 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 0, 180);
                break;
            case PlayerAttackTypes.Down:
                player.entityCombat.attackPosition.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }
}
