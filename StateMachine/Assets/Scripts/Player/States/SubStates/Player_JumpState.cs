using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_JumpState : PlayerGroundedState
{
    private int amountOfJumpsLeft;
    public Player_JumpState(Player player, Player_StateMachine stateMachine, PlayerData playerData, string animationBoolenName) : base(player, stateMachine, playerData, animationBoolenName)
    {

    }
    public override void Enter()
    {
       
        base.Enter();
        Debug.Log("Jump State");
        amountOfJumpsLeft--;
        Movement?.SetVelocitY(Vector2.up * playerData.jumpSpeed);

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhsysicUpdate()
    {
        base.PhsysicUpdate();

    }
    public override void Exit()
    {
        base.Exit();
    }
    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
