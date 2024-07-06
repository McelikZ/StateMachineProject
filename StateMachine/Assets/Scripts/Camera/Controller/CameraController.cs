using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // CameraInput s�n�f� referans�
    private CameraInput input;
    // Fare hassasiyet ayarlar�
    public float mouseSenstivityY, mouseSenstivityX;
    // Takip edilecek oyuncunun Transform bile�eni
    public Transform player;
    // Fare hareketi de�erleri
    private float mouseXValue, mouseYValue;
    // Kameran�n hedef oyuncuya olan mesafesi
    [SerializeField] private Vector3 targetDistance;

    // Kameran�n x ve y eksenlerindeki rotasyonu
    private float xRotation;
    private float yRotation;

    // Awake fonksiyonu, script etkinle�tirildi�inde �a�r�l�r
    private void Awake()
    {
        // CameraInput bile�enini al
        input = GetComponent<CameraInput>();
    }

    // Start fonksiyonu, ilk karede �a�r�l�r
    private void Start()
    {
        // �mleci kilitle ve gizle
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update fonksiyonu, her karede �a�r�l�r
    private void Update()
    {
        // Kameran�n pozisyonunu oyuncunun pozisyonuna yumu�ak bir ge�i�le ayarla
        transform.position = Vector3.Lerp(this.transform.position, player.position + targetDistance, Time.deltaTime * 10);

        // Fare hareketine g�re X ve Y de�erlerini g�ncelle
        mouseXValue += input.mouseX * mouseSenstivityX * Time.deltaTime;
        mouseYValue += input.mouseY * mouseSenstivityY * Time.deltaTime;

        // X ekseni rotasyonunu fare Y hareketine g�re ayarla
        xRotation = mouseYValue;

        // X ekseni rotasyonunu -75 ile 75 derece aras�nda s�n�rla
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        // Kameran�n d�n�� a��s�n� belirle
        this.transform.eulerAngles = new Vector3(xRotation, mouseXValue, 0);
    }
}
