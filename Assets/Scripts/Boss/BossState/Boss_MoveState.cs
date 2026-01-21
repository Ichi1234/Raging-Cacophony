using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_MoveState : BossState
{
    private float moveTowardPlayerChance = 0.6f;

    public Boss_MoveState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.canChangeState = false;
    }

    public override void Update()
    {
        base.Update();

        if (curStateRandomResult < moveTowardPlayerChance)
        {
            MoveTowardPlayer();
        }

        else
        {
            MoveAwayFromPlayer();
        }
    }

    public void MoveTowardPlayer()
    {
        float playerDistance = GetDistanceBetweenPlayer();

        if (playerDistance <= nearPlayerDistance)
        {
            stateMachine.canChangeState = true;
            stateMachine.ChangeState(boss.idleState);
        }

        float playerDirection = GetPlayerDirection();

        boss.SetVelocity(boss.moveSpeed * playerDirection, rb.linearVelocity.y);

        boss.HandleFlip(-playerDirection);

    }

    public void MoveAwayFromPlayer()
    {
        float playerDistance = GetDistanceBetweenPlayer();

        if (boss.wallDetected || farPlayerDistance < playerDistance)
        {
            stateMachine.canChangeState = true;
            stateMachine.ChangeState(boss.idleState);
        }

        float playerDirection = GetPlayerDirection();

        boss.SetVelocity(boss.moveSpeed * -playerDirection, rb.linearVelocity.y);

        boss.HandleFlip(playerDirection);

    }

    private float GetPlayerDirection() => boss.transform.position.x < player.transform.position.x ? 1 : -1;

    private float GetDistanceBetweenPlayer() => Mathf.Abs(player.transform.position.x - boss.transform.position.x);
}
