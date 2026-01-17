using UnityEngine;

public abstract class PlayerState : EntityState
{
    protected Player player;
    protected Rigidbody2D rb;
    protected Animator anim;

    public PlayerState(Player player, StateMachine stateMachine, string animParam) : base(stateMachine, animParam)
    {
        this.player = player;

        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponentInChildren<Animator>();
    }

    public override void UpdateAnimationParameter(bool activate)
    {
        anim.SetBool(animParam, activate);
    }
}
