using UnityEngine;

public class PlayerBaseState : State
{
    protected PlayerStatemachine SM;

    public PlayerBaseState(PlayerStatemachine statemachine)
    {
        SM = statemachine;
    }
}
