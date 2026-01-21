using UnityEngine;

public class Player_DownAttackState : Player_AttackState
{
    public Player_DownAttackState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        HandleAttackPosition(PlayerAttackTypes.Down);
        HandleAttackRotation(PlayerAttackTypes.Down);

        base.Enter();
    }
}
