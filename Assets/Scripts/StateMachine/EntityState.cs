using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class EntityState
{
    public StateMachine stateMachine { get; private set; }
    protected float stateTimer;
    protected Entity_Vfx entityVfx;
    public string animParam { get; private set; }

    protected EntityState(StateMachine stateMachine, string animParam)
    {
        this.stateMachine = stateMachine;
        this.animParam = animParam;
    }


    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }


    public virtual void Exit()
    {

    }

    public virtual void UpdateAnimationParameter(bool activate)
    {

    }
}
