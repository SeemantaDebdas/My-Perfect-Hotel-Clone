using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SM.Animator.CrossFadeInFixedTime("Move", 0.1f);
    }

    public override void Tick()
    {
        base.Tick();
        if (SM.TouchInput.GetMoveInput().magnitude < 0.1f)
        {
            SM.SwitchState(new PlayerIdleState(SM));
            return;
        }
        
        SM.Controller.Move(SM.TouchInput.GetMoveInput().normalized * (Time.deltaTime * SM.MoveSpeed));
        
        Quaternion targetRotation = Quaternion.LookRotation(SM.TouchInput.GetMoveInput().normalized);
        
        SM.transform.rotation = Quaternion.Slerp(SM.transform.rotation, 
            targetRotation, 
            SM.RotateSpeed * Time.deltaTime);
    }
}
