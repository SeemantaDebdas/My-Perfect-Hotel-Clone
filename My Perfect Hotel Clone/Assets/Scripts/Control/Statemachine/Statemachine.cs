using UnityEngine;

public class Statemachine : MonoBehaviour
{
    protected State currentState;
    protected State nextState;
    public virtual void SwitchState(State state)
    {
        nextState = state;

        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    protected void Update()
    {
        currentState?.Tick();
    }

    protected void LateUpdate()
    {
        currentState?.LateTick();
    }

    protected void FixedUpdate()
    {
        currentState?.FixedTick();
    }
}