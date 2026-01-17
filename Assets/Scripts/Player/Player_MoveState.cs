using UnityEngine;

public class Player_MoveState : PlayerState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }


    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        rb.linearVelocity = new Vector2(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
    }


}
