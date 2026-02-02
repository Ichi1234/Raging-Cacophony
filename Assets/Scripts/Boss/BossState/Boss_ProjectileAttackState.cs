
using UnityEngine;

public class Boss_ProjectileAttackState : BossState
{
    public Boss_ProjectileAttackState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();


        stateTimer = 1.5f;
    }

    public override void Update()
    {
        base.Update();

        FlipToFacePlayer(true);

        if (stateTimer <= 0)
        {
            bossVfx.ShootBossPoop();
            stateMachine.ChangeState(boss.decisionState);
        }

    }
}
