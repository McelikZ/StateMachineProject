using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_StateMachine
{
    public Hand_States CurrentState { get; private set; }

    public void Initialize(Hand_States startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(Hand_States newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
