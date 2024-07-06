using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBaseState : Hand_States
{
    public HandBaseState(Hand hand, Hand_StateMachine stateMachine, HandData handData, string animationBoolenName) : base(hand, stateMachine, handData, animationBoolenName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (hand.Input.attackCheck)
        {
            hand.StateMachine.ChangeState(hand.AttackState);
        }
        else
        {
            hand.StateMachine.ChangeState(hand.EmptyState);
        }
    }
    public override void PhsysicUpdate()
    {
        base.PhsysicUpdate();
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Exit()
    {
        base.Exit();
    }

}
