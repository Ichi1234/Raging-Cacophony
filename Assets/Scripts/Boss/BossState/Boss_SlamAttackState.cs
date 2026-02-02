
public class Boss_SlamAttackState : BossState
{
    public Boss_SlamAttackState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        boss.SetVelocity(0, -boss.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (boss.isGround)
        {
            stateMachine.ChangeState(boss.decisionState);
        }
    }
}
