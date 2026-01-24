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

    private float attackChance = 0.7f;

    protected float randomChangeState;
    protected float curStateRandomResult;

    protected BossSpecialAttackTypes specialAttackTypes;

    protected float prepareAttackTime = 1;
    private float lastLungeAttckTime;
    private float lastAttackTime;

    public BossState(Boss boss, StateMachine stateMachine, string animParam) : base(boss, stateMachine, animParam)
    {
        this.boss = boss;

        rb = boss.GetComponent<Rigidbody2D>();
        anim = boss.GetComponentInChildren<Animator>();
        bossVfx = boss.GetComponent<Boss_Vfx>();
        bossCombat = boss.GetComponent<Boss_Combat>();
        player = boss.GetPlayer();

        lastLungeAttckTime -= boss.dashCooldown;
        lastAttackTime -= boss.entityCombat.attackCooldown;
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.canChangeState = false;
        curStateRandomResult = Random.value;
        randomChangeState = Random.value;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.P))
        {
            stateMachine.ChangeState(boss.leapAttackState);
        }

        if (randomChangeState < attackChance)
        {
            float distanceFromPlayer = GetDistanceBetweenPlayer();

            if (distanceFromPlayer <= nearPlayerDistance)
            {
                stateMachine.ChangeState(boss.basicAttackState);
            }

            else if (distanceFromPlayer >= farPlayerDistance)
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
        }

        else
        {
            stateMachine.ChangeState(boss.moveState);
        }
    }

    protected float GetDistanceBetweenPlayer() => Mathf.Abs(player.transform.position.x - boss.transform.position.x);
    protected float GetPlayerDirection() => boss.transform.position.x < player.transform.position.x ? 1 : -1;


}
