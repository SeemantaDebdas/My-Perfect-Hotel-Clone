using System;
using MPH.Control;
using UnityEngine;

public class PlayerStatemachine : Statemachine
{
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public PlayerTouchInput TouchInput { get; private set; }
    
    [field: Space,SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [field: SerializeField] public float RotateSpeed { get; private set; } = 20f;
    
    private void Start()
    {
        SwitchState(new PlayerIdleState(this));
    }
}
