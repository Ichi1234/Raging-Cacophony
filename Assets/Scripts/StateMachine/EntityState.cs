using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class EntityState
{
    public StateMachine stateMachine { get; private set; }
    public string animParam { get; private set; }
    protected float stateTimer;
    protected float stateEnterTime;

    protected EntityState(StateMachine stateMachine, string animParam)
    {
        this.stateMachine = stateMachine;
        this.animParam = animParam;
    }

    public virtual void Enter()
    {
        stateEnterTime = stateTimer;
    }

    public virtual void Update()
    {
        stateTimer += Time.deltaTime;
    }


    public virtual void Exit()
    {

    }

    public virtual void UpdateAnimationParameter(bool activate)
    {

    }
}
