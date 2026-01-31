

public class Player_JumpState : PlayerState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(rb.linearVelocity.x, player.JumpForce);

        player.JumpAnimation();

    }

    public override void Update()
    {
        base.Update();

        if (player.isGround && rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (input.Player.Move.WasPerformedThisFrame())
        {
            player.SetVelocity(player.moveInput.x * player.MoveSpeed, rb.linearVelocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();

    }

  

}
