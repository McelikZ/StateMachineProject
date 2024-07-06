using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player_States
{
    protected Core core;

    protected Movement Movement
    {
        get => movement ?? core.GetCoreComponent(ref movement);
    }

    private Movement movement;

    public Player player;
    public Player_StateMachine stateMachine;
    public PlayerData playerData;
    public string animationBoolenName;
    public Player_States(Player player, Player_StateMachine stateMachine, PlayerData playerData, string animationBoolenName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animationBoolenName = animationBoolenName;
        core = player.Core;
    }
    public virtual void Enter()
    {
     //   player.player_Animator.SetBool(animationBoolenName, true);
        DoChecks();
    }
    public virtual void LogicUpdate()
    {
        DoChecks();
    }
    public virtual void PhsysicUpdate()
    {

    }
    public virtual void Exit()
    {
      //  player.player_Animator.SetBool(animationBoolenName, false);

    }
    public virtual void DoChecks()
    {
       // player.player_Animator.SetBool(animationBoolenName, false);
    }
}
