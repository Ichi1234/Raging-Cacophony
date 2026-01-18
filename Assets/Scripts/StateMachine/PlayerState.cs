using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerInputSet input;

    public PlayerState(Player player, StateMachine stateMachine, string animParam) : base(stateMachine, animParam)
    {
        this.player = player;

        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponentInChildren<Animator>();
        input = player.input;
    }

    public override void Update()
    {
        base.Update();

        if (input == null)
        {
            Debug.Log("Player input set is null");
            return;
        }

        if (input.Player.Jump.WasPressedThisFrame() && player.isGround)
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
}
