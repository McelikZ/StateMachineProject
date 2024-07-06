using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    // Singleton instance
    public static ScriptManager Instance;

    // Y�netilecek scriptlerin listesi
    internal List<MonoBehaviour> scripts = new List<MonoBehaviour>();

    private void Awake()
    {
        // Singleton pattern - tek bir instance olu�turulur
        Instance = this;

        // Script bile�enlerini listeye ekleyin
        scripts.Add(GameObject.FindObjectOfType<HandController>()); // HandController bile�enini ekleyin
        scripts.Add(GameObject.FindObjectOfType<Hand>()); // Hand bile�enini ekleyin
        scripts.Add(GameObject.FindObjectOfType<CameraController>()); // CameraController bile�enini ekleyin
        scripts.Add(GameObject.FindObjectOfType<Player>()); // Player bile�enini ekleyin
        scripts.Add(GameObject.FindObjectOfType<ChangeItem>()); // ChangeItem bile�enini ekleyin
    }
    // T�m scriptleri belirli bir duruma getiren genel bir fonksiyon
    public void SetAllScriptsActive(bool active)
    {
        // Liste i�erisindeki t�m scriptleri etkinle�tir veya devre d��� b�rak
        foreach (var script in scripts)
        {
            if (script != null)
            {
                script.enabled = active; // Script'i belirtilen duruma ayarla
            }
            else
            {
                Debug.LogWarning("Script bulunamad�."); // Script bulunamazsa uyar� mesaj� yazd�r
            }
        }
    }
}
