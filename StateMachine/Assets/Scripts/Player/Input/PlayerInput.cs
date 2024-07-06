using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    internal InputHandler playerInput;

    internal Vector2 input;
    internal Vector3 currentInput;

    internal bool jumpInput;

    internal int selectedItemValue;
    internal bool changeItemCheck;

    internal bool attackCheck;
    private void Awake()
    {
        Instance = this;

        playerInput = new InputHandler();
        playerInput.PlayerController.Enable();

        playerInput.PlayerController.Movement.started += OnMoveInput;
        playerInput.PlayerController.Movement.performed += OnMoveInput;
        playerInput.PlayerController.Movement.canceled += OnMoveInput;

        playerInput.PlayerController.Jump.started += OnJumpInput;
        // playerInput.PlayerController.Jump.performed += OnJumpInput;
        playerInput.PlayerController.Jump.canceled += OnJumpInput;

        playerInput.PlayerController.ChangeItemOne.started += OnItemChangeInputOne;
        playerInput.PlayerController.ChangeItemTwo.started += OnItemChangeInputTwo;
        playerInput.PlayerController.ChangeItemThree.started += OnItemChangeInputThree;

        playerInput.PlayerController.ChangeItem.started += OnItemChangeInput;
        playerInput.PlayerController.ChangeItem.performed += OnItemChangeInput;
        playerInput.PlayerController.ChangeItem.canceled += OnItemChangeInput;

        //  playerInput.PlayerController.ChangeItemOne.performed += OnItemChangeInputOne;
        //  playerInput.PlayerController.ChangeItemOne.canceled += OnItemChangeInputOne;

        playerInput.PlayerController.Attack.started += OnAttackInput;
        playerInput.PlayerController.Attack.canceled += OnAttackInputCanceled;

    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        currentInput.z = input.y;
        currentInput.x = input.x;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
            jumpInput = true;

        if (context.canceled)
            jumpInput = false;
    }
    public void OnItemChangeInput(InputAction.CallbackContext context) => changeItemCheck = true;
    public void OnItemChangeInputOne(InputAction.CallbackContext context) => selectedItemValue = 0;
    public void OnItemChangeInputTwo(InputAction.CallbackContext context) => selectedItemValue = 1;
    public void OnItemChangeInputThree(InputAction.CallbackContext context) => selectedItemValue = 2;
    public void OnAttackInput(InputAction.CallbackContext context) => attackCheck = true;
    public void OnAttackInputCanceled(InputAction.CallbackContext context) => attackCheck = false;

}