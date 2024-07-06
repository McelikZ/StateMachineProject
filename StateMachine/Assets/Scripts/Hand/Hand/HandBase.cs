using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBase : MonoBehaviour
{
    // public PlayerData playerData; // Oyuncu verileri
    public HandData handData; // El verileri
    private Rigidbody player; // Oyuncu rigidbody bile�eni

    private void Awake()
    {
        // Ba�l� oldu�u nesnenin Rigidbody bile�enini al
        player = GetComponentInParent<Rigidbody>();

        // El verilerini ba�lang�� de�erleriyle ba�lat
        handData.AmountValue = handData.handBasemount; // El miktar de�erini el ba�lang�� miktar�yla e�itle
        handData.StartPosition = transform.localPosition; // Ba�lang�� pozisyonunu yerel pozisyonla e�itle
        handData.StartRotation = transform.localRotation.eulerAngles; // Ba�lang�� rotasyonunu yerel rotasyonla e�itle
    }

    private void Update()
    {
        // El etkin de�ilse i�lem yapma
        if (!handData.handEnabled) return;

        // Oyuncunun h�z�n� hesapla
        float speed = new Vector3(player.velocity.x, 0, player.velocity.z).magnitude;

        // El pozisyon ve rotasyonunu s�f�rla
        Reset();

        // Oyuncu h�z� belirli bir de�erin �zerindeyse ve zemindeyse
        if (speed > handData.ToggleSpeed && CollisionControl.Instance.Ground)
        {
            // El pozisyonunu ve rotasyonunu ba� a�r�s� hareketine g�re g�ncelle
            handData.FinalPosition += HeadBobMotion();
            handData.FinalRotation += new Vector3(-HeadBobMotion().z, 0, HeadBobMotion().x) * handData.RotationMultipler * 10;
        }
        // Oyuncu h�z� belirli bir de�erin �zerindeyse
        else if (speed > handData.ToggleSpeed)
        {
            // El pozisyonunu ba� a�r�s� hareketine g�re g�ncelle (daha yava�)
            handData.FinalPosition += HeadBobMotion() / 2f;
        }

        // Sol Shift tu�una bas�l�rsa veya b�rak�l�rsa el miktar de�erini g�ncelle
        if (Input.GetKeyDown(KeyCode.LeftShift))
            handData.AmountValue = handData.handBasemount * handData.SprintAmount;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            handData.AmountValue = handData.handBasemount / handData.SprintAmount;

        // El pozisyonunu yumu�ak bir �ekilde g�ncelle
        transform.localPosition = Vector3.Lerp(transform.localPosition, handData.FinalPosition, handData.handBaseSmooth * Time.deltaTime);

        // El rotasyonunu yumu�ak bir �ekilde g�ncelle (e�er hareket etme etkinse)
        if (handData.EnabledRotationMovement)
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(handData.FinalRotation), handData.handBaseSmooth / 1.5f * Time.deltaTime);
    }

    // Ba� a�r�s� hareketini hesaplayan fonksiyon
    private Vector3 HeadBobMotion()
    {
        Vector3 pos = Vector3.zero;
        // Y dikeyindeki hareketi hesapla
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * handData.Frequency) * handData.AmountValue * 2f, handData.handBaseSmooth * Time.deltaTime);
        // X yatay�ndaki hareketi hesapla
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * handData.Frequency / 2f) * handData.AmountValue * 1.3f, handData.handBaseSmooth * Time.deltaTime);
        return pos;
    }

    // El pozisyonunu ve rotasyonunu s�f�rlayan fonksiyon
    private void Reset()
    {
        // E�er el pozisyonu ba�lang�� pozisyonuna e�itse i�lem yapma
        if (transform.localPosition == handData.StartPosition) return;

        // El pozisyonunu ba�lang�� pozisyonuna do�ru yumu�ak bir �ekilde g�ncelle
        handData.FinalPosition = Vector3.Lerp(handData.FinalPosition, handData.StartPosition, 1 * Time.deltaTime);
        // El rotasyonunu ba�lang�� rotasyonuna do�ru yumu�ak bir �ekilde g�ncelle
        handData.FinalRotation = Vector3.Lerp(handData.FinalRotation, handData.StartRotation, 1 * Time.deltaTime);
    }
}
