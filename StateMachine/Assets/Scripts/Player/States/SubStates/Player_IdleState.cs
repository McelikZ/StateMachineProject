using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_IdleState : PlayerGroundedState
{
    public Player_IdleState(Player player, Player_StateMachine stateMachine, PlayerData playerData, string animationBoolenName) : base(player, stateMachine, playerData, animationBoolenName)
    {

    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log("Ýdle State");
    
    }
    public override void PhsysicUpdate()
    {
        base.PhsysicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
}
