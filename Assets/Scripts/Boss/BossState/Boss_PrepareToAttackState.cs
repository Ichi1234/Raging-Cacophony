using UnityEngine;

public class Boss_PrepareToAttackState : BossState
{
    public Boss_PrepareToAttackState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();


        stateTimer = prepareAttackTime;
        boss.SetVelocity(0, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer <= 0)
        {
            stateMachine.canChangeState = true;

            switch (specialAttackTypes)
            {
                case BossSpecialAttackTypes.LeapAttack:
                    stateMachine.ChangeState(boss.leapAttackState);
                    break;
                case BossSpecialAttackTypes.LungeAttack:
                    stateMachine.ChangeState(boss.lungeAttackState);
                    break;
            }
        }
    }
}
