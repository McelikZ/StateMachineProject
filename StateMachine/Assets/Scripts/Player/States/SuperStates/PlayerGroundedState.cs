using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : Player_States
{
    // Çarpýþma kontrol bileþeni
    private CollisionControl CollisionControl
    {
        get => collisionControl ?? core.GetCoreComponent(ref collisionControl);
    }
    private CollisionControl collisionControl;

    // Yapýcý metod
    public PlayerGroundedState(Player player, Player_StateMachine stateMachine, PlayerData playerData, string animationBoolenName)
        : base(player, stateMachine, playerData, animationBoolenName)
    {
    }

    private bool jumpInput; // Zýplama giriþi

    // Kontrolleri yap
    public override void DoChecks()
    {
        base.DoChecks();
        // Ek kontrol gereksinimleri buraya ekleyin
    }

    // Duruma giriþ
    public override void Enter()
    {
        base.Enter();
    }

    // Mantýk güncelleme
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Koþma durumunu kontrol et
        if ((player.Input.currentInput.x != 0 || player.Input.currentInput.z != 0) && CollisionControl.Ground && !player.Input.jumpInput)
        {
            stateMachine.ChangeState(player.RunState);
            player.stateInfo.text = "State:Run";
        }
        // Zýplama giriþini kontrol et
        else if (player.Input.jumpInput && CollisionControl.Ground)
        {
            stateMachine.ChangeState(player.JumpState);
            player.stateInfo.text = "State:Jump";
        }
        // Yere temasý kaybetme durumunu kontrol et
        else if (!collisionControl.Ground)
        {
            stateMachine.ChangeState(player.FallState);
            player.stateInfo.text = "State:Jump";
        }
        // Bekleme durumuna geçiþ koþullarýný kontrol et
        else if (player.Input.currentInput.x == 0 && player.Input.currentInput.z == 0 && !player.Input.jumpInput && CollisionControl.Ground || !CollisionControl.Ground)
        {
            stateMachine.ChangeState(player.IdleState);
            player.stateInfo.text = "State:Idle";
        }
    }

    // Fiziksel güncelleme
    public override void PhsysicUpdate()
    {
        base.PhsysicUpdate();
    }

    // Durumdan çýkýþ
    public override void Exit()
    {
        base.Exit();
    }
}
