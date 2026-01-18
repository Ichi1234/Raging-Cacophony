using UnityEngine;

public class Player_AttackState : PlayerState
{

    public Player_AttackState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        entityVfx.CreateAttackVfx();

        
        //stateMachine.ChangeState(player.idleState);
    }


}
