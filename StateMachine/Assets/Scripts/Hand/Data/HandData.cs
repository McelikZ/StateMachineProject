using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HandData", menuName = "HandData/Hand_Data")]
public class HandData : ScriptableObject
{
    [Header("HandsHolder")]
    public bool handEnabled = true;

    [Space, Header("Main")]
    [Range(0.0005f, 0.02f)] public float handBasemount = 0.005f;
    [Range(1.0f, 3.0f)] public float SprintAmount = 1.4f;
    [Range(5f, 20f)] public float Frequency = 13.0f;
    [Range(50f, 10f)] public float handBaseSmooth = 24.2f;

    [Header("RotationMovement")]
    public bool EnabledRotationMovement = true;
    [Range(0.1f, 10.0f)] public float RotationMultipler = 6f;
    public float ToggleSpeed = 1.5f;
    public float AmountValue;

    [Header("Rotation-Position")]
    public Vector3 StartPosition;
    public Vector3 StartRotation;
    public Vector3 FinalPosition;
    public Vector3 FinalRotation;

    [Header("HandsSmooth")]
    [Range(1, 10)] public float handSmooth = 4f;
    [Range(0.001f, 1)] public float handAmount = 0.03f;
    [Range(0.001f, 1)] public float maxAmount = 0.04f;

    [Header("Rotation")]
    [Range(1, 10)] public float RotationSmooth = 4.0f;
    [Range(0.1f, 10)] public float RotationAmount = 1.0f;
    [Range(0.1f, 10)] public float MaxRotationAmount = 5.0f;
    [Range(0.1f, 10)] public float RotationMovementMultipler = 1.0f;

    [Header("CroughRotation")]
    public bool EnabledCroughRotation = false;
    [Range(0.1f, 20)] public float RotationCroughSmooth = 15.0f;
    [Range(5f, 50)] public float RotationCroughMultipler = 18.0f;
    public float CroughRotation;

    public Vector3 InstallPosition;
    public Quaternion InstallRotation;

    [Header("Input")]
    public KeyCode CroughKey = KeyCode.LeftControl;

}
