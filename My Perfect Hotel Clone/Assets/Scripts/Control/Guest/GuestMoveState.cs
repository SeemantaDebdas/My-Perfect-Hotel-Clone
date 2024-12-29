using UnityEngine;

public class GuestMoveState : GuestBaseState
{
    private Vector3 destination = Vector3.zero;
    Transform lookAtTarget = null;
    public GuestMoveState(GuestStatemachine statemachine) : base(statemachine){}

    public GuestMoveState(GuestStatemachine statemachine, Vector3 destination, Transform lookAtTarget = null) : base(statemachine)
    {
        this.lookAtTarget = lookAtTarget;
        this.destination = destination;
    }

    public override void Enter()
    {
        base.Enter();
        SM.Animator.CrossFadeInFixedTime("Move", 0.1f);
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

    public override void Exit()
    {
        base.Exit();
        
        if(lookAtTarget != null)
            SM.transform.rotation = Quaternion.LookRotation((lookAtTarget.position - SM.transform.position).normalized);
    }
}
