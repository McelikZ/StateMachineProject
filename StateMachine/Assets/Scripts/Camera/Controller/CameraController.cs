using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // CameraInput sýnýfý referansý
    private CameraInput input;
    // Fare hassasiyet ayarlarý
    public float mouseSenstivityY, mouseSenstivityX;
    // Takip edilecek oyuncunun Transform bileþeni
    public Transform player;
    // Fare hareketi deðerleri
    private float mouseXValue, mouseYValue;
    // Kameranýn hedef oyuncuya olan mesafesi
    [SerializeField] private Vector3 targetDistance;

    // Kameranýn x ve y eksenlerindeki rotasyonu
    private float xRotation;
    private float yRotation;

    // Awake fonksiyonu, script etkinleþtirildiðinde çaðrýlýr
    private void Awake()
    {
        // CameraInput bileþenini al
        input = GetComponent<CameraInput>();
    }

    // Start fonksiyonu, ilk karede çaðrýlýr
    private void Start()
    {
        // Ýmleci kilitle ve gizle
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update fonksiyonu, her karede çaðrýlýr
    private void Update()
    {
        // Kameranýn pozisyonunu oyuncunun pozisyonuna yumuþak bir geçiþle ayarla
        transform.position = Vector3.Lerp(this.transform.position, player.position + targetDistance, Time.deltaTime * 10);

        // Fare hareketine göre X ve Y deðerlerini güncelle
        mouseXValue += input.mouseX * mouseSenstivityX * Time.deltaTime;
        mouseYValue += input.mouseY * mouseSenstivityY * Time.deltaTime;

        // X ekseni rotasyonunu fare Y hareketine göre ayarla
        xRotation = mouseYValue;

        // X ekseni rotasyonunu -75 ile 75 derece arasýnda sýnýrla
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        // Kameranýn dönüþ açýsýný belirle
        this.transform.eulerAngles = new Vector3(xRotation, mouseXValue, 0);
    }
}
