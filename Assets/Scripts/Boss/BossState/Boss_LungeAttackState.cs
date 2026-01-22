using UnityEngine;


public class Boss_LungeAttackState : BossState
{
    private Arena arena;
    private Vector3 leftWallPos;
    private Vector3 rightWallPos;

    private bool isJumping = true;
    private float moveSpeedMultiplier = 2.5f;

    private float farFromGround = 8;
    private float farFromWall = 8;
    private bool isLeftWallFarthest;

    public Boss_LungeAttackState(Boss boss, StateMachine stateMachine, string animParam, Arena arena) : base(boss, stateMachine, animParam)
    {
        this.arena = arena;
    }

    public override void Enter()
    {
        base.Enter();

        isJumping = true;
        leftWallPos = arena.GetLeftWallPos();
        rightWallPos = arena.GetLeftWallPos();

        isLeftWallFarthest = Mathf.Abs(boss.transform.position.x - leftWallPos.x) > Mathf.Abs(boss.transform.position.x - rightWallPos.x) ? true : false;

        if (boss.facingDir != GetWallDirection())
        {
            boss.Flip();
        }

    }

    public override void Update()
    {
        base.Update();

        DashAfterJump();

        Vector3 destination = isLeftWallFarthest ? new Vector3(leftWallPos.x - farFromWall, leftWallPos.y + farFromGround) : new Vector3(rightWallPos.x + farFromWall, rightWallPos.y + farFromGround);

        CheckFinishedJump(destination);
        

        if (isJumping)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, destination, boss.jumpForce * 1.25f * Time.deltaTime);
        }




    }

    private void DashAfterJump()
    {
        if (!isJumping)
        {
            if (boss.frontWallDetected)
            {
                anim.SetBool("isLungeAttack", false);
                stateMachine.canChangeState = true;
                stateMachine.ChangeState(boss.idleState);
            }

            else if (boss.isGround)
            {
                anim.SetBool("isMoving", false);
                boss.SetVelocity(boss.moveSpeed * boss.facingDir * moveSpeedMultiplier, rb.linearVelocity.y);
                entityVfx.CreateSmokeVfx();
                anim.SetBool("isLungeAttack", true);

            }
        }
    }

    private void CheckFinishedJump(Vector3 destination)
    {
        if (Vector3.Distance(boss.transform.position, destination) < 0.01f)
        {
            if (boss.facingDir != GetPlayerDirection())
            {
                boss.Flip();
            }

            isJumping = false;
        }
    }

    protected float GetWallDirection()
    {
        if (isLeftWallFarthest)
        {
            return boss.transform.position.x < leftWallPos.x ? 1 : -1;
        }

        return boss.transform.position.x < rightWallPos.x ? 1 : -1;

    }
}
