using UnityEngine;

public class BossState : EntityState
{
    protected Boss boss;
    protected Player player;
    protected Rigidbody2D rb;
    protected Boss_Combat bossCombat;
    protected Boss_Vfx bossVfx;

    protected float nearPlayerDistance = 8;
    protected float farPlayerDistance = 13;

    protected float randomChangeState;
    protected float curStateRandomResult;

    protected BossSpecialAttackTypes specialAttackTypes;

    protected float prepareAttackTime = 1;
    private float lastAttackTime;


    public BossState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
        this.boss = boss;

        rb = boss.GetComponent<Rigidbody2D>();
        anim = boss.GetComponentInChildren<Animator>();
        bossVfx = boss.GetComponent<Boss_Vfx>();
        bossCombat = boss.GetComponent<Boss_Combat>();
        player = boss.GetPlayer();

        lastAttackTime -= boss.entityCombat.AttackCooldown;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log(stateMachine.currentState);

        curStateRandomResult = Random.value;

    }

    protected void FlipToFacePlayer()
    {
        if (boss.facingDir != GetPlayerDirection())
        {
            boss.Flip();
        }
    }

    protected float GetDistanceBetweenPlayer() => Mathf.Abs(player.transform.position.x - boss.transform.position.x);
    protected float GetPlayerDirection() => boss.transform.position.x < player.transform.position.x ? 1 : -1;

}
