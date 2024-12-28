using UnityEngine;

public class GuestSleepState : GuestBaseState
{
    private float sleepTime = 5f;
    float sleepCounter = 0f;
    public GuestSleepState(GuestStatemachine statemachine) : base(statemachine)
    {
    }


    public override void Enter()
    {
        base.Enter();
        SM.Animator.CrossFadeInFixedTime("Sleep", 0.1f);

        Transform restTransform = SM.Guest.Room.GetRestTransform();
        
        SM.transform.position = restTransform.position;
        SM.transform.rotation = Quaternion.LookRotation(restTransform.forward);

        sleepCounter = sleepTime;
        
        SM.Agent.ResetPath();
        SM.Agent.enabled = false;
    }

    public override void Tick()
    {
        base.Tick();
        sleepCounter -= Time.deltaTime;

        if (sleepCounter <= 0f)
        {
            SM.SwitchState(new GuestMoveState(SM, SM.Guest.ExitTransform.position));
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        SM.transform.position = SM.Guest.Room.GetExitTransform().position;
        SM.Agent.enabled = true;
        SM.Guest.UnassignRoom();
    }
}
