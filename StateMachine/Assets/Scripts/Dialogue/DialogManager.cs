using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI Elements")]
    public GameObject dialogueParent; // Diyalog penceresini tutan GameObject
    public TextMeshProUGUI dialogueText; // Diyalog metnini gösteren TextMeshProUGUI
    public Button option1Button; // Birinci seçenek butonu
    public Button option2Button; // Ýkinci seçenek butonu

    // Diyalog verileri
    [Header("Dialogue Data")]
    public DialogueData DialogueData; // Diyalog verilerini tutan sýnýf
    internal List<DialogueString> dialogueList; // Diyalog cümlelerinin listesi

    // Diðer UI elemanlarý
    [Header("Other UI Elements")]
    public GameObject viewPointCanvas; // Bakýþ noktasý canvasý

    // Oyuncu ve kamera referanslarý
    [Header("Player and Camera References")]
    private Transform playerCamera; // Oyuncunun kamerasýnýn transformu
    [SerializeField] private GameObject playerActionObj; // Oyuncu aksiyon objesi

    // Diyalog yönetimi
    [Header("Dialogue Management")]
    private DialogueTrigger trigger; // Diyalog tetikleyicisi
    private int currentDialogueIndex = 0; // Þu anki diyalog indeksi
    private bool optionSelected = false; // Seçenek seçilip seçilmediði durumu

    // Singleton instance
    public static DialogueManager Instance; // Singleton instance

    private void Awake()
    {
        // Singleton pattern
        Instance = this;
        // DialogueTrigger bileþenini bul
        trigger = GameObject.FindObjectOfType<DialogueTrigger>();
        // Ana kameranýn transformunu al
        playerCamera = Camera.main.transform;
    }

    private void Start()
    {
        // Baþlangýçta diyalog penceresini ve butonlarý gizle
        dialogueParent.SetActive(false);
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    // Diyalog baþladýðýnda çaðrýlan fonksiyon
    public void DialogueStartingPreview(List<DialogueString> textToPrint)
    {
        viewPointCanvas.SetActive(false); // Bakýþ noktasý canvasýný gizle
        Debug.Log("Diyalog Baþlatýldý...");
        dialogueParent.gameObject.SetActive(true); // Diyalog penceresini göster
        Cursor.lockState = CursorLockMode.None; // Ýmleci serbest býrak
        Cursor.visible = true; // Ýmleci görünür yap
        ScriptManager.Instance.SetAllScriptsActive(false); // Tüm scriptleri devre dýþý býrak

        dialogueList = textToPrint; // Diyalog listesini ata
        currentDialogueIndex = 0; // Diyalog indeksini sýfýrla

        DisableButtons(); // Butonlarý devre dýþý býrak
        StartCoroutine(PrintDialogue()); // Diyalog yazdýrma coroutine'ini baþlat
    }

    // Butonlarý devre dýþý býrakan fonksiyon
    private void DisableButtons()
    {
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    // Diyalog yazdýrma coroutine'i
    private IEnumerator PrintDialogue()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            DialogueString line = dialogueList[currentDialogueIndex];
            line.startDialogueEvent?.Invoke(); // Diyalog baþlangýç eventini tetikle

            yield return StartCoroutine(TypeText(line.text)); // Metni yazdýr

            if (line.isQuestion)
            {
                SetupOptions(line); // Sorunun seçeneklerini ayarla
                yield return new WaitUntil(() => optionSelected); // Seçenek seçilmesini bekle
            }
            else
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Kullanýcýnýn týklamasýný bekle
            }

            line.endDialogueEvent?.Invoke(); // Diyalog bitiþ eventini tetikle
            optionSelected = false; // Seçenek seçilme durumunu sýfýrla
        }

        DialogueStop(); // Diyalog durdur
    }

    // Seçenekleri ayarlayan fonksiyon
    private void SetupOptions(DialogueString line)
    {
        option1Button.gameObject.SetActive(true); // Birinci butonu göster
        option2Button.gameObject.SetActive(true); // Ýkinci butonu göster

        option1Button.GetComponentInChildren<TextMeshProUGUI>().text = line.answerOption1; // Birinci seçeneði ayarla
        option2Button.GetComponentInChildren<TextMeshProUGUI>().text = line.answerOption2; // Ýkinci seçeneði ayarla

        option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump)); // Birinci buton týklama olayýný ayarla
        option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump)); // Ýkinci buton týklama olayýný ayarla
    }

    // Seçenek seçildiðinde çaðrýlan fonksiyon
    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true; // Seçenek seçildi olarak iþaretle
        DisableButtons(); // Butonlarý devre dýþý býrak
        currentDialogueIndex = indexJump; // Diyalog indeksini güncelle
    }

    // Metni yazdýran coroutine
    private IEnumerator TypeText(string text)
    {
        dialogueText.text = ""; // Diyalog metnini sýfýrla

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter; // Her bir karakteri ekle
            yield return new WaitForSeconds(DialogueData.typingSpeed); // Yazma hýzýna göre bekle
        }

        if (dialogueList[currentDialogueIndex].isEnd)
        {
            DisableButtons(); // Butonlarý devre dýþý býrak
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Kullanýcýnýn týklamasýný bekle
            DialogueStop(); // Diyaloðu durdur
        }

        currentDialogueIndex++; // Diyalog indeksini artýr
    }

    // Diyaloðu durduran fonksiyon
    private void DialogueStop()
    {
        // Bakýþ noktasý canvasýný yeniden etkinleþtir
        viewPointCanvas.SetActive(true);

        // Tüm scriptleri yeniden etkinleþtir
        ScriptManager.Instance.SetAllScriptsActive(true);

        // Diyalog tetikleyicisinin konuþma durumunu sýfýrla
        trigger.hasSpoken = false;

        // Tüm coroutine'leri durdur
        StopAllCoroutines();

        // Diyalog metnini sýfýrla
        dialogueText.text = "";

        // Diyalog penceresini gizle
        dialogueParent.SetActive(false);

        // Ýmleci kilitle ve görünmez yap
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
