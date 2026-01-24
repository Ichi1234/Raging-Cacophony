using UnityEngine;


public class Boss_LungeAttackState : BossState
{
    private Arena arena;
    private Vector3 leftWallPos;
    private Vector3 rightWallPos;
    private Vector3 topWallPos;

    private bool isJumping = true;
    private float moveSpeedMultiplier = 2.5f;
    private GameObject smokeVfx;

    private bool jumpToLeftWall;

    public Boss_LungeAttackState(Boss boss, StateMachine stateMachine, string animParam, Arena arena) : base(boss, stateMachine, animParam)
    {
        this.arena = arena;
    }

    public override void Enter()
    {
        base.Enter();

        isJumping = true;
        leftWallPos = arena.GetLeftWallPos();
        rightWallPos = arena.GetRightWallPos();
        topWallPos = arena.GetTopWallPos();

        jumpToLeftWall = Random.Range(0, 2) == 1 ? true : false; 

        if (boss.facingDir != GetWallDirection())
        {
            boss.Flip();
        }

    }

    public override void Update()
    {
        base.Update();

        DashAfterJump();

        Vector3 destination = jumpToLeftWall ? new Vector3(leftWallPos.x + arena.farFromWall, leftWallPos.y + arena.farFromGround) : new Vector3(rightWallPos.x - arena.farFromWall, rightWallPos.y + arena.farFromGround);

        destination = arena.ClampInsideArena(destination);

        CheckFinishedJump(destination);
        

        if (isJumping)
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, destination, boss.jumpForce * 1.25f * Time.deltaTime);
        }

    }

    private void FinishDash()
    {
        if (smokeVfx != null)
        {
            smokeVfx.GetComponent<VFX_Controller>().DestroyVfx();
        }
        anim.SetBool("isLungeAttack", false);

        if (boss.bigSmokeVfx == null)
        {
            boss.LungeAttackChangeState();
        }
    }
    private void DashAfterJump()
    {
        if (!isJumping)
        {
            if (boss.frontWallDetected)
            {
                FinishDash();
            }

            else if (boss.isGround)
            {
                anim.SetBool("isMoving", false);
                boss.SetVelocity(boss.moveSpeed * boss.facingDir * moveSpeedMultiplier, rb.linearVelocity.y);
                if (smokeVfx == null)
                {
                    smokeVfx = bossVfx.CreateSmokeVfx();
                }
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
        if (jumpToLeftWall)
        {
            return boss.transform.position.x < leftWallPos.x ? 1 : -1;
        }

        return boss.transform.position.x < rightWallPos.x ? 1 : -1;

    }
}
