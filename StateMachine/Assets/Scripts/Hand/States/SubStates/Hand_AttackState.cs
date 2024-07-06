using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_AttackState : HandBaseState
{
    public Hand_AttackState(Hand hand, Hand_StateMachine stateMachine, HandData handData, string animationBoolenName) : base(hand, stateMachine, handData, animationBoolenName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
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

