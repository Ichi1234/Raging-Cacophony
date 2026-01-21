using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class EntityState
{
    public StateMachine stateMachine { get; private set; }
    protected float stateTimer;

    protected Entity_Vfx entityVfx;

    protected Animator anim;

    public string animParam { get; private set; }
    protected bool triggerCalled = false;


    protected EntityState(Entity entity, StateMachine stateMachine, string animParam)
    {
        this.stateMachine = stateMachine;
        this.animParam = animParam;
        
    }


    public virtual void Enter()
    {
        triggerCalled = false;

        if (animParam == "")
        {
            return;
        }
        anim.SetBool(animParam, true);

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }


    public virtual void Exit()
    {
        if (animParam == "")
        {
            return;
        }
        anim.SetBool(animParam, false);
    }

    public void AnimationTriggered()
    {
        triggerCalled = true;
    }

    public virtual void UpdateAnimationParameter(bool activate)
    {

    }


}
