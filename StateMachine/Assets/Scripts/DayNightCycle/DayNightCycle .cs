using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayDuration = 120f; // Bir günün süresi (saniye)
    public Gradient lightColor; // Iþýðýn rengini deðiþtirmek için bir gradient
    public AnimationCurve lightIntensity; // Iþýðýn þiddetini deðiþtirmek için bir animation curve
    public float startTime = 0.25f; // 0.25, günün %25'inde baþlar (sabah 6 civarý)

    private float currentTime;

    void Start()
    {
        // Baþlangýç zamanýný ayarla
        currentTime = startTime * dayDuration;
        UpdateLighting();
    }

    void Update()
    {
        // Zamaný ilerlet
        currentTime += Time.deltaTime;

        // Zamaný döngüsel hale getir
        if (currentTime >= dayDuration)
        {
            currentTime = 0f;
        }

        UpdateLighting();
    }

    void UpdateLighting()
    {
        // Zamaný 0-1 arasýnda normalleþtir
        float timeNormalized = currentTime / dayDuration;

        // Iþýðýn rengini ve þiddetini ayarla
        directionalLight.color = lightColor.Evaluate(timeNormalized);
        directionalLight.intensity = lightIntensity.Evaluate(timeNormalized) * 2f;

        // Iþýðýn yönünü ayarla (güneþin doðup batmasýný simüle etmek için)
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timeNormalized * 360f) - 90f, 170f, 0));
    }
}
