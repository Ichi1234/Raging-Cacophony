
using UnityEngine;

public class Boss_ProjectileAttackState : BossState
{
    private GameObject bossPoop;
    public Boss_ProjectileAttackState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        FlipToFacePlayer(true);

        stateTimer = 2;
    }

    public override void Update()
    {
        base.Update();
        
        if (stateTimer <= 0)
        {
            bossPoop = bossVfx.CreateBossPoop();
            stateMachine.ChangeState(boss.decisionState);
        }

    }
}
