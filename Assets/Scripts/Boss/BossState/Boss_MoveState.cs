

public class Boss_MoveState : BossState
{
    private float moveTowardPlayerChance = 0.6f;

    public Boss_MoveState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
            stateMachine.UnlockedState();
            stateMachine.ChangeState(boss.idleState);
        }

        float playerDirection = GetPlayerDirection();

        boss.SetVelocity(boss.moveSpeed * playerDirection, rb.linearVelocity.y);

        boss.HandleFlip(playerDirection);

    }

    public void MoveAwayFromPlayer()
    {
        float playerDistance = GetDistanceBetweenPlayer();

        if (boss.backWallDetected || boss.frontWallDetected || farPlayerDistance < playerDistance)
        {
            stateMachine.UnlockedState();
            stateMachine.ChangeState(boss.idleState);
        }

        float playerDirection = GetPlayerDirection();

        boss.SetVelocity(boss.moveSpeed * -playerDirection, rb.linearVelocity.y);

        boss.HandleFlip(playerDirection);

    }

}
