using UnityEngine;

public class StateMachine
{
    public EntityState currentState;
    public bool canChangeState = true;

    public void Initialize(EntityState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        if (canChangeState)
        {
            currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }
        
    }

    public void CallUpdateCurrentState()
    {
        currentState.Update();
    }

    private void UpdateAnimationParameter(bool activate)
    {
        currentState.UpdateAnimationParameter(activate);
    }
}
