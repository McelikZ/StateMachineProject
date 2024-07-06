using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    // Singleton instance
    public static ScriptManager Instance;

    // Yönetilecek scriptlerin listesi
    internal List<MonoBehaviour> scripts = new List<MonoBehaviour>();

    private void Awake()
    {
        // Singleton pattern - tek bir instance oluþturulur
        Instance = this;

        // Script bileþenlerini listeye ekleyin
        scripts.Add(GameObject.FindObjectOfType<HandController>()); // HandController bileþenini ekleyin
        scripts.Add(GameObject.FindObjectOfType<Hand>()); // Hand bileþenini ekleyin
        scripts.Add(GameObject.FindObjectOfType<CameraController>()); // CameraController bileþenini ekleyin
        scripts.Add(GameObject.FindObjectOfType<Player>()); // Player bileþenini ekleyin
        scripts.Add(GameObject.FindObjectOfType<ChangeItem>()); // ChangeItem bileþenini ekleyin
    }
    // Tüm scriptleri belirli bir duruma getiren genel bir fonksiyon
    public void SetAllScriptsActive(bool active)
    {
        // Liste içerisindeki tüm scriptleri etkinleþtir veya devre dýþý býrak
        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = active; // Script'i belirtilen duruma ayarla
            }
            else
            {
                Debug.LogWarning("Script bulunamadý."); // Script bulunamazsa uyarý mesajý yazdýr
            }
        }
    }
}
