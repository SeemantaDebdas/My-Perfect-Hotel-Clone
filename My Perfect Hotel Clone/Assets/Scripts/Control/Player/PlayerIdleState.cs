using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStatemachine statemachine) : base(statemachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SM.Animator.CrossFadeInFixedTime("Idle", 0.1f);
    }

    public override void Tick()
    {
        base.Tick();

        if (SM.TouchInput.GetMoveInput().magnitude > 0.1f)
        {
            SM.SwitchState(new PlayerMoveState(SM));
            return;
        }
    }
}
