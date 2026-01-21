using UnityEngine;

public class Player_BasicAttackState : Player_AttackState
{

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        player.canFlip = false;
        HandleAttackPosition(PlayerAttackTypes.Basic);
        HandleAttackRotation(PlayerAttackTypes.Basic);

        base.Enter();

        player.SetVelocity(0, rb.linearVelocity.y);


    }

    public override void Update()
    {
        base.Update();

        
    }

    public override void Exit()
    {
        base.Exit();

        player.canFlip = true;
    }


}
