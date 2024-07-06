using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallState : PlayerGroundedState
{
    public Player_FallState(Player player, Player_StateMachine stateMachine, PlayerData playerData, string animationBoolenName) : base(player, stateMachine, playerData, animationBoolenName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void PhsysicUpdate()
    {
        base.PhsysicUpdate();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
