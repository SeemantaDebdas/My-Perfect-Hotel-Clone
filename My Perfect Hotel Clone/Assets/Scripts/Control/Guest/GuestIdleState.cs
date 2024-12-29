using UnityEngine;

public class GuestIdleState : GuestBaseState
{
    public GuestIdleState(GuestStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SM.Agent.ResetPath();
        SM.Animator.CrossFadeInFixedTime("Idle", 0.1f);
        
        SM.Guest.OnRoomAssigned += Guest_OnRoomAssigned;
        SM.Guest.OnQueuePositionSet += Guest_OnQueuePositionSet;
    }

    void Guest_OnRoomAssigned(Room room)
    {
        SM.SwitchState(new GuestMoveState(SM, room.transform.position));
        return;
    }

    public override void Exit()
    {
        base.Exit();
        SM.Guest.OnRoomAssigned -= Guest_OnRoomAssigned;
        SM.Guest.OnQueuePositionSet -= Guest_OnQueuePositionSet;
    }

    private void Guest_OnQueuePositionSet(Vector3 destination, Transform caller)
    {
        SM.SwitchState(new GuestMoveState(SM, destination, caller));
    }
}
