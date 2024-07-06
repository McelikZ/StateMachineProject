using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_States
{
    protected Core core;

    public Hand hand;
    public Hand_StateMachine stateMachine;
    public HandData handData;
    public string animationBoolenName;
    public Hand_States(Hand hand, Hand_StateMachine stateMachine, HandData handData, string animationBoolenName)
    {
        this.hand = hand;
        this.stateMachine = stateMachine;
        this.handData = handData;
        this.animationBoolenName = animationBoolenName;
        //  core = hand.Core;
    }
    public virtual void Enter()
    {
        hand.hand_Animator.SetBool(animationBoolenName, true); //animasyonla ilgili olan kod dilediðiniz zaman kullanabilirsiniz.
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
        hand.hand_Animator.SetBool(animationBoolenName, false); //animasyonla ilgili olan kod dilediðiniz zaman kullanabilirsiniz.

    }
    public virtual void DoChecks()
    {
      //  hand.hand_Animator.SetBool(animationBoolenName, false); //animasyonla ilgili olan kod dilediðiniz zaman kullanabilirsiniz.
    }
}
