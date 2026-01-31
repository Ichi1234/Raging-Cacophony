using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;


    protected Player_Combat playerCombat;
    protected Entity_Vfx entityVfx;
    private float lastDashTime;

    public PlayerState(Player player, StateMachine stateMachine, string animParam) : base(player, stateMachine, animParam)
    {
        this.player = player;

        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponentInChildren<Animator>();
        entityVfx = player.GetComponent<Entity_Vfx>();
        playerCombat = player.GetComponent<Player_Combat>();

        input = player.input;

        lastDashTime -= player.dashCooldown;
        player.ReduceAttackCooldown(playerCombat.attackCooldown);
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
            player.SetLastTimeAttack(Time.time);
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
    protected bool CanAttack() => Time.time - player.lastAttackTime > player.entityCombat.attackCooldown;
}
