using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInput : MonoBehaviour
{
    public static CameraInput instance;

    public InputHandler cameraInput;

    internal float mouseX;
    internal float mouseY;
    private void Awake()
    {
        instance = this;
        cameraInput = new InputHandler();
        cameraInput.CameraController.Enable();

        cameraInput.CameraController.MouseX.started += OnMouseX;
        cameraInput.CameraController.MouseX.performed += OnMouseX;
        cameraInput.CameraController.MouseX.canceled += OnMouseX;

        cameraInput.CameraController.MouseY.started += OnMouseY;
        cameraInput.CameraController.MouseY.performed += OnMouseY;
        cameraInput.CameraController.MouseY.canceled += OnMouseY;


    }
    public void OnMouseX(InputAction.CallbackContext context)
    {
        mouseX = context.ReadValue<float>();
    }
    public void OnMouseY(InputAction.CallbackContext context)
    {
        mouseY = context.ReadValue<float>();

    }
}
