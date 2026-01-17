using UnityEngine;

public class StateMachine
{
    private EntityState currentState;

    public void Initialize(EntityState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        currentState.Exit();
        UpdateAnimationParameter(false);

        currentState = newState;
        currentState.Enter();
        UpdateAnimationParameter(true);
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
