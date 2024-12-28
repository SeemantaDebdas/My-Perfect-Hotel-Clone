using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class GuestStatemachine : Statemachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Guest Guest { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    private void Start()
    {
        SwitchState(new GuestIdleState(this));
        return;
    }
}
