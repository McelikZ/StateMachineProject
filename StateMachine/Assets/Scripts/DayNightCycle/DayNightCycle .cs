using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayDuration = 120f; // Bir g�n�n s�resi (saniye)
    public Gradient lightColor; // I����n rengini de�i�tirmek i�in bir gradient
    public AnimationCurve lightIntensity; // I����n �iddetini de�i�tirmek i�in bir animation curve
    public float startTime = 0.25f; // 0.25, g�n�n %25'inde ba�lar (sabah 6 civar�)

    private float currentTime;

    void Start()
    {
        // Ba�lang�� zaman�n� ayarla
        currentTime = startTime * dayDuration;
        UpdateLighting();
    }

    void Update()
    {
        // Zaman� ilerlet
        currentTime += Time.deltaTime;

        // Zaman� d�ng�sel hale getir
        if (currentTime >= dayDuration)
        {
            currentTime = 0f;
        }

        UpdateLighting();
    }

    void UpdateLighting()
    {
        // Zaman� 0-1 aras�nda normalle�tir
        float timeNormalized = currentTime / dayDuration;

        // I����n rengini ve �iddetini ayarla
        directionalLight.color = lightColor.Evaluate(timeNormalized);
        directionalLight.intensity = lightIntensity.Evaluate(timeNormalized) * 2f;

        // I����n y�n�n� ayarla (g�ne�in do�up batmas�n� sim�le etmek i�in)
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timeNormalized * 360f) - 90f, 170f, 0));
    }
}
