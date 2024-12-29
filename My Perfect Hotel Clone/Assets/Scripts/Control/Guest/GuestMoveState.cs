using UnityEngine;

public class GuestMoveState : GuestBaseState
{
    private Vector3 destination = Vector3.zero;
    public GuestMoveState(GuestStatemachine statemachine) : base(statemachine){}

    public GuestMoveState(GuestStatemachine statemachine, Vector3 destination) : base(statemachine)
    {
        this.destination = destination;
    }

    public override void Enter()
    {
        base.Enter();
        SM.Animator.CrossFadeInFixedTime("Move", 0.1f);
        
        SM.Guest.OnQueuePositionSet += Guest_OnQueuePositionSet;
    }

    public override void Exit()
    {
        base.Exit();

        SM.Guest.OnQueuePositionSet -= Guest_OnQueuePositionSet;
    }

    void Guest_OnQueuePositionSet(Vector3 destination)
    {
        Debug.Log("Queue position set. Should change state");
        SM.SwitchState(new GuestMoveState(SM, destination));
    }

    public override void Tick()
    {
        base.Tick();
        
        SM.Agent.SetDestination(destination);

        if (Vector3.Distance(SM.Guest.transform.position, destination) < 0.1f)
        {
            Debug.Log("Destination Reached");
            if (SM.Guest.Room == null)
            {
                SM.SwitchState(new GuestIdleState(SM));
                return;
            }
            
            SM.SwitchState(new GuestSleepState(SM));
            return;
        }
    }
}
