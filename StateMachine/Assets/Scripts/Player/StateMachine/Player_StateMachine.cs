using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_StateMachine
{
    public Player_States CurrentState { get; private set; }

    public void Initialize(Player_States startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(Player_States newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

}
