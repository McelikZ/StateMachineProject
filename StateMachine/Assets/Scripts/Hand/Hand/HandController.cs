using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Rigidbody playerRigidbody; // Oyuncu Rigidbody bile�eni
    public HandData handData; // El verileri

    private void Start()
    {
        // Ba�lang�� pozisyonunu ve rotasyonunu el verilerine ata
        handData.InstallPosition = transform.localPosition;
        handData.InstallRotation = transform.localRotation;
    }

    private void Update()
    {
        HandleHandPosition(); // El pozisyonunu i�le
        HandleHandRotation(); // El rotasyonunu i�le
    }

    // El pozisyonunu i�leyen fonksiyon
    private void HandleHandPosition()
    {
        // Kamera ve oyuncu giri�lerini al
        float InputX = -CameraInput.instance.mouseX;
        float InputY = -CameraInput.instance.mouseY;
        float horizontal = -PlayerInput.Instance.currentInput.x;
        float vertical = PlayerInput.Instance.currentInput.z;

        // El hareketini s�n�rla
        float moveX = Mathf.Clamp(InputX * handData.handAmount, -handData.maxAmount, handData.maxAmount);
        float moveY = Mathf.Clamp(InputY * handData.handAmount, -handData.maxAmount, handData.maxAmount);

        // Elin son pozisyonunu hesapla
        Vector3 finalPosition = new Vector3(moveX, moveY + -playerRigidbody.velocity.y / 60, 0);

        // Eli yumu�ak bir �ekilde hareket ettir
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + handData.InstallPosition, Time.deltaTime * handData.handSmooth);
    }

    // El rotasyonunu i�leyen fonksiyon
    private void HandleHandRotation()
    {
        // Kamera giri�lerini al
        float InputX = -CameraInput.instance.mouseX;
        float InputY = -CameraInput.instance.mouseY;

        // El e�imini s�n�rla
        float TiltX = Mathf.Clamp(InputX * handData.RotationAmount, -handData.MaxRotationAmount, handData.MaxRotationAmount);
        float TiltY = Mathf.Clamp(InputY * handData.RotationSmooth, -handData.MaxRotationAmount, handData.MaxRotationAmount);

        // E�ilim verisini i�le (��kme durumunda)
        HandleCrouchRotation();

        // El hareketini i�le
        Vector3 vector = new Vector3(Mathf.Max(PlayerInput.Instance.currentInput.z * 0.4f, 0) * handData.RotationMovementMultipler, 0, -PlayerInput.Instance.currentInput.x * handData.RotationMovementMultipler);

        // Elin son rotasyonunu hesapla
        Vector3 finalRotation = new Vector3(-TiltY, 0, TiltX + handData.CroughRotation) + vector;

        // Eli yumu�ak bir �ekilde d�nd�r
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(finalRotation) * handData.InstallRotation, Time.deltaTime * handData.RotationSmooth);
    }

    // ��kme durumunda el rotasyonunu i�leyen fonksiyon
    private void HandleCrouchRotation()
    {
        // E�er ��kme tu�u bas�l�ysa ��kme rotasyonunu g�ncelle
        if (handData.EnabledCroughRotation && Input.GetKey(handData.CroughKey))
            handData.CroughRotation = Mathf.Lerp(handData.CroughRotation, handData.RotationCroughMultipler, handData.RotationCroughSmooth * Time.deltaTime);
        else
            handData.CroughRotation = Mathf.Lerp(handData.CroughRotation, 0f, handData.RotationCroughSmooth * Time.deltaTime);
    }
}
