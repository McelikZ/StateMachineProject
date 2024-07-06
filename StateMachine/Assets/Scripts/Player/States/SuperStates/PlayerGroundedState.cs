using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : Player_States
{
    // �arp��ma kontrol bile�eni
    private CollisionControl CollisionControl
    {
        get => collisionControl ?? core.GetCoreComponent(ref collisionControl);
    }
    private CollisionControl collisionControl;

    // Yap�c� metod
    public PlayerGroundedState(Player player, Player_StateMachine stateMachine, PlayerData playerData, string animationBoolenName)
        : base(player, stateMachine, playerData, animationBoolenName)
    {
    }

    private bool jumpInput; // Z�plama giri�i

    // Kontrolleri yap
    public override void DoChecks()
    {
        base.DoChecks();
        // Ek kontrol gereksinimleri buraya ekleyin
    }

    // Duruma giri�
    public override void Enter()
    {
        base.Enter();
    }

    // Mant�k g�ncelleme
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Ko�ma durumunu kontrol et
        if ((player.Input.currentInput.x != 0 || player.Input.currentInput.z != 0) && CollisionControl.Ground && !player.Input.jumpInput)
        {
            stateMachine.ChangeState(player.RunState);
            player.stateInfo.text = "State:Run";
        }
        // Z�plama giri�ini kontrol et
        else if (player.Input.jumpInput && CollisionControl.Ground)
        {
            stateMachine.ChangeState(player.JumpState);
            player.stateInfo.text = "State:Jump";
        }
        // Yere temas� kaybetme durumunu kontrol et
        else if (!collisionControl.Ground)
        {
            stateMachine.ChangeState(player.FallState);
            player.stateInfo.text = "State:Jump";
        }
        // Bekleme durumuna ge�i� ko�ullar�n� kontrol et
        else if (player.Input.currentInput.x == 0 && player.Input.currentInput.z == 0 && !player.Input.jumpInput && CollisionControl.Ground || !CollisionControl.Ground)
        {
            stateMachine.ChangeState(player.IdleState);
            player.stateInfo.text = "State:Idle";
        }
    }

    // Fiziksel g�ncelleme
    public override void PhsysicUpdate()
    {
        base.PhsysicUpdate();
    }

    // Durumdan ��k��
    public override void Exit()
    {
        base.Exit();
    }
}
