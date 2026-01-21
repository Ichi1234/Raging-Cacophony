using UnityEngine;
using UnityEngine.Windows;

public class BossState : EntityState
{
    protected Boss boss;
    protected Player player;
    protected Rigidbody2D rb;
    protected Entity_Combat bossCombat;

    protected float nearPlayerDistance = 13;
    protected float farPlayerDistance = 20;

    protected float randomChangeState;
    protected float curStateRandomResult;

    private float lastLungeAttckTime;
    private float lastAttackTime;

    public BossState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
        this.boss = boss;

        rb = boss.GetComponent<Rigidbody2D>();
        anim = boss.GetComponentInChildren<Animator>();
        entityVfx = boss.GetComponent<Entity_Vfx>();
        bossCombat = boss.GetComponent<Entity_Combat>();
        player = boss.GetPlayer();

        lastLungeAttckTime -= boss.dashCooldown;
        lastAttackTime -= boss.entityCombat.attackCooldown;
    }

    public override void Enter()
    {
        base.Enter();

        curStateRandomResult = Random.value;
        randomChangeState = Random.Range(0, 100);
    }

    public override void Update()
    {
        base.Update();

        //if (randomChangeState < 70)
        //{
        //    stateMachine.ChangeState(boss.moveState);
        //}

        //else
        //{
        //    stateMachine.ChangeState(boss.moveState);
        //}
    }


}
