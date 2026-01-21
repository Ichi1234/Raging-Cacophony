using UnityEngine;

public class BossState : EntityState
{
    protected Boss boss;
    protected Player player;
    protected Rigidbody2D rb;
    protected Entity_Combat bossCombat;

    protected float nearPlayerDistance = 8;
    protected float farPlayerDistance = 13;

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

        //stateMachine.canChangeState = false;
        curStateRandomResult = Random.value;
        randomChangeState = Random.Range(0, 100);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.P))
        {
            stateMachine.ChangeState(boss.basicAttackState);
        }

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
