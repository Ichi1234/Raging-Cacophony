using UnityEngine;

public class Boss_BasicAttackState : BossState
{
    public Boss_BasicAttackState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Im here");

        HandleAttackRotation();

        boss.SetVelocity(0, rb.linearVelocity.y);

        entityVfx.CreateAttackVfx();

    }

    public override void Update()
    {
        base.Update();


        if (triggerCalled)
        {
            stateMachine.canChangeState = true;
            stateMachine.ChangeState(boss.idleState);
        }
    }

    protected void HandleAttackRotation()
    {
        boss.entityCombat.attackPosition.transform.rotation = boss.facingDir == -1 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(180, 0, 180);
            
    }
}
