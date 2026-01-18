using UnityEngine;

public class Player_DashState : PlayerState
{
    public Player_DashState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        player.DashAnimation();
     
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashForce * player.facingDir, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
