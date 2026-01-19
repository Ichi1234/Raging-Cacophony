using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerInputSet input;
    private float lastDashTime;
    private float lastAttackTime;

    public PlayerState(Player player, StateMachine stateMachine, string animParam) : base(stateMachine, animParam)
    {
        this.player = player;

        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponentInChildren<Animator>();
        entityVfx = player.GetComponent<Entity_Vfx>();
        input = player.input;

        lastDashTime -= player.dashCooldown;
        lastAttackTime -= player.entityCombat.attackCooldown;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();

        if (input == null)
        {
            Debug.Log("Player input set is null");
            return;
        }

        if (input.Player.Attack.WasPressedThisFrame() && CanAttack())
        {
            HandleAttackTypes();
            lastAttackTime = Time.time;
        }

        if (input.Player.Dash.WasPerformedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
            lastDashTime = Time.time;
            
        
        }
       
        if (input.Player.Jump.WasPerformedThisFrame() && player.isGround)
        {
            stateMachine.ChangeState(player.jumpState);
        }


    }

    public override void UpdateAnimationParameter(bool activate)
    {
        if (animParam == "")
        {
            return;
        }
        anim.SetBool(animParam, activate);
    }

    private void HandleAttackTypes()
    {
        if (player.moveInput.y == 1)
        {
            stateMachine.ChangeState(player.upAttackState);
        }
        else if (player.moveInput.y == -1 && !player.isGround)
        {
            stateMachine.ChangeState(player.downAttackState);
        }
        else
        {
            stateMachine.ChangeState(player.basicAttackState);
        }
    }
    protected bool CanDash() => Time.time - lastDashTime > player.dashCooldown;
    protected bool CanAttack() => Time.time - lastAttackTime > player.entityCombat.attackCooldown;
}
