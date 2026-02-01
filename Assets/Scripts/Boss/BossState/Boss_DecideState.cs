
using UnityEngine;

public class Boss_DecideState : BossState
{
    private float attackChance = 0.7f;

    public Boss_DecideState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        boss.SetVelocity(0, 0);
     
        base.Enter();

        DecideNextState();

    }

    public override void Update()
    {
        base.Update();

    }

    private void DecideNextState()
    {
        stateMachine.UnlockedState();

        if (Random.value < attackChance)
            ChooseBehaviorBasedOnPlayerDistance();
        else
            stateMachine.ChangeState(boss.moveState);
    }

    private void ChooseBehaviorBasedOnPlayerDistance()
    {
        float distanceFromPlayer = GetDistanceBetweenPlayer();

        if (distanceFromPlayer <= nearPlayerDistance)
        {
            PlayerNearBehavior();
        }

        else if (distanceFromPlayer >= farPlayerDistance)
        {
            PlayerFarBehavior();

        }

        else
        {
            stateMachine.ChangeState(boss.moveState);
        }
    }

    private void PlayerFarBehavior()
    {
        float whichFarAttack = Random.Range(0, 100);

        if (whichFarAttack >= 70)
        {
            specialAttackTypes = BossSpecialAttackTypes.LeapAttack;
            stateMachine.ChangeState(boss.prepareToAttackState);
        }

        else if (whichFarAttack >= 30)
        {
            // projectile state
            specialAttackTypes = BossSpecialAttackTypes.LungeAttack;
            stateMachine.ChangeState(boss.prepareToAttackState);
        }

        else
        {
            // lunge attack
            specialAttackTypes = BossSpecialAttackTypes.LungeAttack;
            stateMachine.ChangeState(boss.prepareToAttackState);
        }
    }

    private void PlayerNearBehavior()
    {
        if (Mathf.Abs(player.transform.position.x - boss.transform.position.x) <= 3f && player.transform.position.y > boss.transform.position.y)
        {
            specialAttackTypes = BossSpecialAttackTypes.LeapAttack;
            stateMachine.ChangeState(boss.prepareToAttackState);
        }

        else
        {
            stateMachine.ChangeState(boss.basicAttackState);
        }
    }
}
