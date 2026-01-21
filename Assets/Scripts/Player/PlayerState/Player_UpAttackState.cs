using UnityEngine;

public class Player_UpAttackState : Player_AttackState
{
    public Player_UpAttackState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        HandleAttackPosition(PlayerAttackTypes.Up);
        HandleAttackRotation(PlayerAttackTypes.Up);

        base.Enter();
    }
}
