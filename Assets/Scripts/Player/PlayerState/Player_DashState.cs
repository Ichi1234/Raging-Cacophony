
public class Player_DashState : PlayerState
{
    public Player_DashState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.DashDuration;
        player.DashAnimation();
     
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.DashForce * player.facingDir, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
