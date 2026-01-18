using UnityEngine;

public class Player_DashState : PlayerState
{
    public Player_DashState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.dashForce * player.facingDir, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer - stateEnterTime > player.dashDuration)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
