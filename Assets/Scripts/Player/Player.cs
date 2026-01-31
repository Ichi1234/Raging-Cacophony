using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_UpAttackState upAttackState { get; private set; }
    public Player_DownAttackState downAttackState { get; private set; }
    public Player_StunState stunState { get; private set; }

    public Vector2 moveInput { get; private set; }
    public PlayerInputSet input { get; private set; }

    private Player_Health entityHealth;
    public Player_Berserk playerBerserk;
    private Entity_Vfx entityVfx;

    private Vector3 originalScale;
    private Coroutine movementAnimationCo;
    private Coroutine knockBackCo;


    public float lastAttackTime { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        entityVfx = GetComponent<Entity_Vfx>();
        entityHealth = GetComponent<Player_Health>();
        playerBerserk = GetComponent<Player_Berserk>();

        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "isIdle");
        moveState = new Player_MoveState(this, stateMachine, "isMoving");
        jumpState = new Player_JumpState(this, stateMachine, "");
        dashState = new Player_DashState(this, stateMachine, "");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "isBasicAttack");
        upAttackState = new Player_UpAttackState(this, stateMachine, "isUpAttack");
        downAttackState = new Player_DownAttackState(this, stateMachine, "isDownAttack");
        stunState = new Player_StunState(this, stateMachine, "isIdle");

        stateMachine.Initialize(idleState);

        originalScale = transform.localScale;


    }

    protected override void Update()
    {
        base.Update();

    }

    private void HandleBerserkerStage()
    {
        moveSpeed = baseMoveSpeed * playerBerserk.curStage.movementSpeedMultiplier;
        entityCombat.attackDamage = entityCombat.baseAttackDamage * playerBerserk.curStage.attackDamageMultiplier;
        entityCombat.attackCooldown = entityCombat.baseAttackCoolDown * playerBerserk.curStage.attackCooldownMultiplier;
        entityHealth.regenPerSecond = playerBerserk.curStage.regenerateHealthPerSecond;
        entityVfx.attackObjectColor = playerBerserk.curStage.slashColor;
    }

    private void OnEnable()
    {
        Player_Berserk.OnStageChanged += HandleBerserkerStage;

        input.Enable();

        input.Player.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Player.Move.canceled += context => moveInput = Vector2.zero;

    }

    private void OnDisable()
    {
        input.Disable();
    }



    public override void HandleFlip()
    {
        if (moveInput.x != facingDir && moveInput.x != 0 && canFlip)
        {
            Flip();
        }
    }

    public void ReduceAttackCooldown(float reduceBy) => lastAttackTime -= reduceBy;

    public void SetLastTimeAttack(float lastAttackTime) => this.lastAttackTime = lastAttackTime;

    private IEnumerator ChangeTransformAnimation(Vector3 start, Vector3 end, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            transform.localScale = Vector3.Lerp(start, end, t);
            yield return null;
        }

        transform.localScale = end;
    }

    public void JumpAnimation()
    {
        if (movementAnimationCo != null)
        {
            StopCoroutine(movementAnimationCo);
        }

        movementAnimationCo = StartCoroutine(JumpAnimationCo());
    }

    public void DashAnimation()
    {
        if (movementAnimationCo != null)
        {
            StopCoroutine(movementAnimationCo);
        }

        movementAnimationCo = StartCoroutine(DashAnimationCo());
    }

    public void KnockBack(float attackDir, float attackKnockback)
    {
        if (knockBackCo != null)
        {
            StopCoroutine(knockBackCo);
        }

        knockBackCo = StartCoroutine(KnockBackCo(attackDir, attackKnockback));
    }

    public IEnumerator KnockBackCo(float attackDir, float attackKnockback)
    {

        input.Disable();
        stateMachine.ChangeState(stunState);
        stateMachine.LockedState();

        SetVelocity(attackDir * attackKnockback, attackKnockback * 1.25f);

        yield return new WaitForSeconds(0.5f);

        stateMachine.UnlockedState();
        stateMachine.ChangeState(idleState);
        input.Enable();

    }


    public IEnumerator JumpAnimationCo()
    {
        Vector3 shrinkTo = new Vector3(originalScale.x / 2, originalScale.y * 1.2f);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, shrinkTo, 0.25f));

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, originalScale, 0.25f));

    }

    public IEnumerator DashAnimationCo()
    {
        Vector3 shrinkTo = new Vector3(originalScale.x * 1.5f, originalScale.y / 2f);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, shrinkTo, 0.25f));

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ChangeTransformAnimation(transform.localScale, originalScale, 0.25f));

    }


}
