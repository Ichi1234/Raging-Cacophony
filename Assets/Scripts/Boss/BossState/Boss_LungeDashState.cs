using UnityEngine;

public class Boss_LungeDashState : BossState
{
    private float moveSpeedMultiplier = 2.5f;
    private GameObject smokeVfx;

    public Boss_LungeDashState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Update()
    {
        base.Update();

        LungeAttack();
    }

    private void LungeAttack()
    {
        if (boss.frontWallDetected)
        {
            FinishDash();
        }

        else if (boss.isGround)
        {
            boss.SetVelocity(boss.MoveSpeed * boss.facingDir * moveSpeedMultiplier, rb.linearVelocity.y);
            if (smokeVfx == null)
            {
                smokeVfx = bossVfx.CreateSmokeVfx();
            }

        }
    }

    private void FinishDash()
    {
        if (smokeVfx != null)
        {
            smokeVfx.GetComponent<VFX_Controller>().DestroyVfx();
        }

        if (boss.bigSmokeVfx == null)
        {
            boss.LungeAttackChangeState();
        }
    }

}
