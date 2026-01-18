using UnityEngine;

public class Player_MoveState : PlayerState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }


    public override void Update()
    {
        base.Update();

        if (player.moveInput == Vector2.zero && player.isGround)
        {
            stateMachine.ChangeState(player.idleState);
        }
        player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
    }


}
