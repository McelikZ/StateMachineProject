using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RunState : PlayerGroundedState
{
    public Player_RunState(Player player, Player_StateMachine stateMachine, PlayerData playerData, string animationBoolenName) : base(player, stateMachine, playerData, animationBoolenName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        Debug.Log("Run State");
        base.LogicUpdate();

        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        Vector3 moveDirection = (cameraForward * player.Input.currentInput.z + cameraRight * player.Input.currentInput.x).normalized;

        if (moveDirection.magnitude > 0)
        {
            Movement?.SetVelocity(moveDirection * playerData.runSpeed);
        }
        else
        {
            Movement?.SetVelocity(Vector3.zero);
        }
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
