using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Hand : MonoBehaviour
{
    public Hand_StateMachine StateMachine { get; private set; }

    public Hand_AttackState AttackState { get; private set; }
    public Hand_EmptyState EmptyState { get; private set; }

    public PlayerInput Input { get; private set; }

    //  public Core Core { get; private set; }


    public HandData handData;
    internal string animationBoolenName;
    public Animator hand_Animator;
    public TextMeshProUGUI stateInfo;
    [SerializeField] internal GameObject hand;
    private void Awake()
    {
        //Core = GetComponentInChildren<Core>();
        Input = GetComponentInParent<PlayerInput>();
        hand_Animator = GetComponent<Animator>();

        StateMachine = new Hand_StateMachine();

        AttackState = new Hand_AttackState(this, StateMachine, handData, "Attack");
        EmptyState = new Hand_EmptyState(this, StateMachine, handData, "Empty");
    }
    private void Start()
    {
        StateMachine.Initialize(AttackState);
    }
    private void Update()
    {
        // Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();
        Debug.Log("Attack Check:" + Input.attackCheck);
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhsysicUpdate();
    }

}
