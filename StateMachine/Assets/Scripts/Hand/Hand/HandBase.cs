using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBase : MonoBehaviour
{
    // public PlayerData playerData; // Oyuncu verileri
    public HandData handData; // El verileri
    private Rigidbody player; // Oyuncu rigidbody bileþeni

    private void Awake()
    {
        // Baðlý olduðu nesnenin Rigidbody bileþenini al
        player = GetComponentInParent<Rigidbody>();

        // El verilerini baþlangýç deðerleriyle baþlat
        handData.AmountValue = handData.handBasemount; // El miktar deðerini el baþlangýç miktarýyla eþitle
        handData.StartPosition = transform.localPosition; // Baþlangýç pozisyonunu yerel pozisyonla eþitle
        handData.StartRotation = transform.localRotation.eulerAngles; // Baþlangýç rotasyonunu yerel rotasyonla eþitle
    }

    private void Update()
    {
        // El etkin deðilse iþlem yapma
        if (!handData.handEnabled) return;

        // Oyuncunun hýzýný hesapla
        float speed = new Vector3(player.velocity.x, 0, player.velocity.z).magnitude;

        // El pozisyon ve rotasyonunu sýfýrla
        Reset();

        // Oyuncu hýzý belirli bir deðerin üzerindeyse ve zemindeyse
        if (speed > handData.ToggleSpeed && CollisionControl.Instance.Ground)
        {
            // El pozisyonunu ve rotasyonunu baþ aðrýsý hareketine göre güncelle
            handData.FinalPosition += HeadBobMotion();
            handData.FinalRotation += new Vector3(-HeadBobMotion().z, 0, HeadBobMotion().x) * handData.RotationMultipler * 10;
        }
        // Oyuncu hýzý belirli bir deðerin üzerindeyse
        else if (speed > handData.ToggleSpeed)
        {
            // El pozisyonunu baþ aðrýsý hareketine göre güncelle (daha yavaþ)
            handData.FinalPosition += HeadBobMotion() / 2f;
        }

        // Sol Shift tuþuna basýlýrsa veya býrakýlýrsa el miktar deðerini güncelle
        if (Input.GetKeyDown(KeyCode.LeftShift))
            handData.AmountValue = handData.handBasemount * handData.SprintAmount;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            handData.AmountValue = handData.handBasemount / handData.SprintAmount;

        // El pozisyonunu yumuþak bir þekilde güncelle
        transform.localPosition = Vector3.Lerp(transform.localPosition, handData.FinalPosition, handData.handBaseSmooth * Time.deltaTime);

        // El rotasyonunu yumuþak bir þekilde güncelle (eðer hareket etme etkinse)
        if (handData.EnabledRotationMovement)
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(handData.FinalRotation), handData.handBaseSmooth / 1.5f * Time.deltaTime);
    }

    // Baþ aðrýsý hareketini hesaplayan fonksiyon
    private Vector3 HeadBobMotion()
    {
        Vector3 pos = Vector3.zero;
        // Y dikeyindeki hareketi hesapla
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * handData.Frequency) * handData.AmountValue * 2f, handData.handBaseSmooth * Time.deltaTime);
        // X yatayýndaki hareketi hesapla
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * handData.Frequency / 2f) * handData.AmountValue * 1.3f, handData.handBaseSmooth * Time.deltaTime);
        return pos;
    }

    // El pozisyonunu ve rotasyonunu sýfýrlayan fonksiyon
    private void Reset()
    {
        // Eðer el pozisyonu baþlangýç pozisyonuna eþitse iþlem yapma
        if (transform.localPosition == handData.StartPosition) return;

        // El pozisyonunu baþlangýç pozisyonuna doðru yumuþak bir þekilde güncelle
        handData.FinalPosition = Vector3.Lerp(handData.FinalPosition, handData.StartPosition, 1 * Time.deltaTime);
        // El rotasyonunu baþlangýç rotasyonuna doðru yumuþak bir þekilde güncelle
        handData.FinalRotation = Vector3.Lerp(handData.FinalRotation, handData.StartRotation, 1 * Time.deltaTime);
    }
}
