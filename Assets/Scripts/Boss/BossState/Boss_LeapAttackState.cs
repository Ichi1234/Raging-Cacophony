using UnityEngine;

public class Boss_LeapAttackState : BossState
{
    private float yDistanceFromPlayer = 20;
    private Vector3 currentTopOfPlayerPosition;

    public Boss_LeapAttackState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (boss.facingDir != GetPlayerDirection())
        {
            boss.Flip();
        }

        Vector3 playerPosition = player.transform.position;
        currentTopOfPlayerPosition = new Vector3(playerPosition.x, playerPosition.y + yDistanceFromPlayer);
    }

    public override void Update()
    {
        base.Update();

        if (boss.transform.position.y >= currentTopOfPlayerPosition.y)
        {
            stateMachine.canChangeState = true;
            stateMachine.ChangeState(boss.slamAttackState);
        }

        boss.transform.position = Vector3.MoveTowards(boss.transform.position, currentTopOfPlayerPosition , boss.jumpForce * Time.deltaTime);
    }
}
