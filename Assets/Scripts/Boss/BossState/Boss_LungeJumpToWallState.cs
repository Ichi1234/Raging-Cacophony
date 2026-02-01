using System;
using UnityEngine;


public class Boss_LungeJumpToWallState : BossState
{
    private Arena arena;
    private Vector3 leftWallPos;
    private Vector3 rightWallPos;

    private bool jumpToLeftWall;
    private Vector2 destination;


    public Boss_LungeJumpToWallState(Boss boss, StateMachine stateMachine, string animParam, Arena arena) : base(boss, stateMachine, animParam)
    {
        this.arena = arena;
    }

    public override void Enter()
    {
        base.Enter();

        leftWallPos = arena.GetLeftWallPos();
        rightWallPos = arena.GetRightWallPos();

        jumpToLeftWall = UnityEngine.Random.Range(0, 2) == 1 ? true : false; 

        if (boss.facingDir != GetWallDirection())
        {
            boss.Flip();
        }

        Vector3 destinationBeforeClamp = jumpToLeftWall ? new Vector3(leftWallPos.x + arena.farFromWall, leftWallPos.y + arena.farFromGround)
            : new Vector3(rightWallPos.x - arena.farFromWall, rightWallPos.y + arena.farFromGround);

        destination = arena.ClampInsideArena(destinationBeforeClamp);

    }

    public override void Update()
    {
        base.Update();


        if (HasReachedWall())
        {
            FlipToFacePlayer();

            if (boss.isGround)
            {
                OnJumpFinished();
            }
        }

        if (!HasReachedWall())
        {
            Vector2 newPos = Vector2.MoveTowards(
                        rb.position,
                        destination,
                        boss.JumpForce * Time.fixedDeltaTime
            );

            rb.MovePosition(newPos);
        }
        
    }

    private void OnJumpFinished()
    {
        stateMachine.ChangeState(boss.lungeDashState);
    }

    private bool HasReachedWall()
    {
        return (Mathf.Abs(boss.transform.position.x - destination.x) < 0.05f) ;
    }


    protected float GetWallDirection()
    {
        if (jumpToLeftWall)
        {
            return boss.transform.position.x < leftWallPos.x ? 1 : -1;
        }

        return boss.transform.position.x < rightWallPos.x ? 1 : -1;

    }
}
