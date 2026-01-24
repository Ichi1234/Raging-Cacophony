using UnityEngine;

public class Boss_IdleState : BossState
{
    public Boss_IdleState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        boss.SetVelocity(0, 0);
        stateMachine.canChangeState = true;
    }

    public override void Update()
    {
        base.Update();

    }
}
