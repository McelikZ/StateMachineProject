using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    public Player_StateMachine StateMachine { get; private set; }
    public Player_IdleState IdleState { get; private set; }
    public Player_RunState RunState { get; private set; }
    public Player_JumpState JumpState { get; private set; }
    public Player_FallState FallState { get; private set; }
    public PlayerInput Input { get; private set; }

    public Core Core { get; private set; }


    public PlayerData playerData;
    internal string animationBoolenName;
    public Animator player_Animator;
    [SerializeField] internal GameObject hand;
    public TextMeshProUGUI stateInfo;
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        Input = GetComponent<PlayerInput>();
        player_Animator = GetComponent<Animator>();

        StateMachine = new Player_StateMachine();

        IdleState = new Player_IdleState(this, StateMachine, playerData, "Idle");
        RunState = new Player_RunState(this, StateMachine, playerData, "Run");
        JumpState = new Player_JumpState(this, StateMachine, playerData, "Jump");
        FallState = new Player_FallState(this, StateMachine, playerData, "Fall");
    }
    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhsysicUpdate();
    }
    
}
