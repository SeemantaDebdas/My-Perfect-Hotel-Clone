using UnityEngine;

public class GuestBaseState : State
{
    protected GuestStatemachine SM;
    public GuestBaseState(GuestStatemachine statemachine)
    {
        SM = statemachine;
    }
}
