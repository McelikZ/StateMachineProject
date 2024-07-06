using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI Elements")]
    public GameObject dialogueParent; // Diyalog penceresini tutan GameObject
    public TextMeshProUGUI dialogueText; // Diyalog metnini g�steren TextMeshProUGUI
    public Button option1Button; // Birinci se�enek butonu
    public Button option2Button; // �kinci se�enek butonu

    // Diyalog verileri
    [Header("Dialogue Data")]
    public DialogueData DialogueData; // Diyalog verilerini tutan s�n�f
    internal List<DialogueString> dialogueList; // Diyalog c�mlelerinin listesi

    // Di�er UI elemanlar�
    [Header("Other UI Elements")]
    public GameObject viewPointCanvas; // Bak�� noktas� canvas�

    // Oyuncu ve kamera referanslar�
    [Header("Player and Camera References")]
    private Transform playerCamera; // Oyuncunun kameras�n�n transformu
    [SerializeField] private GameObject playerActionObj; // Oyuncu aksiyon objesi

    // Diyalog y�netimi
    [Header("Dialogue Management")]
    private DialogueTrigger trigger; // Diyalog tetikleyicisi
    private int currentDialogueIndex = 0; // �u anki diyalog indeksi
    private bool optionSelected = false; // Se�enek se�ilip se�ilmedi�i durumu

    // Singleton instance
    public static DialogueManager Instance; // Singleton instance

    private void Awake()
    {
        // Singleton pattern
        Instance = this;
        // DialogueTrigger bile�enini bul
        trigger = GameObject.FindObjectOfType<DialogueTrigger>();
        // Ana kameran�n transformunu al
        playerCamera = Camera.main.transform;
    }

    private void Start()
    {
        // Ba�lang��ta diyalog penceresini ve butonlar� gizle
        dialogueParent.SetActive(false);
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    // Diyalog ba�lad���nda �a�r�lan fonksiyon
    public void DialogueStartingPreview(List<DialogueString> textToPrint)
    {
        viewPointCanvas.SetActive(false); // Bak�� noktas� canvas�n� gizle
        Debug.Log("Diyalog Ba�lat�ld�...");
        dialogueParent.gameObject.SetActive(true); // Diyalog penceresini g�ster
        Cursor.lockState = CursorLockMode.None; // �mleci serbest b�rak
        Cursor.visible = true; // �mleci g�r�n�r yap
        ScriptManager.Instance.SetAllScriptsActive(false); // T�m scriptleri devre d��� b�rak

        dialogueList = textToPrint; // Diyalog listesini ata
        currentDialogueIndex = 0; // Diyalog indeksini s�f�rla

        DisableButtons(); // Butonlar� devre d��� b�rak
        StartCoroutine(PrintDialogue()); // Diyalog yazd�rma coroutine'ini ba�lat
    }

    // Butonlar� devre d��� b�rakan fonksiyon
    private void DisableButtons()
    {
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    // Diyalog yazd�rma coroutine'i
    private IEnumerator PrintDialogue()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            DialogueString line = dialogueList[currentDialogueIndex];
            line.startDialogueEvent?.Invoke(); // Diyalog ba�lang�� eventini tetikle

            yield return StartCoroutine(TypeText(line.text)); // Metni yazd�r

            if (line.isQuestion)
            {
                SetupOptions(line); // Sorunun se�eneklerini ayarla
                yield return new WaitUntil(() => optionSelected); // Se�enek se�ilmesini bekle
            }
            else
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Kullan�c�n�n t�klamas�n� bekle
            }

            line.endDialogueEvent?.Invoke(); // Diyalog biti� eventini tetikle
            optionSelected = false; // Se�enek se�ilme durumunu s�f�rla
        }

        DialogueStop(); // Diyalog durdur
    }

    // Se�enekleri ayarlayan fonksiyon
    private void SetupOptions(DialogueString line)
    {
        option1Button.gameObject.SetActive(true); // Birinci butonu g�ster
        option2Button.gameObject.SetActive(true); // �kinci butonu g�ster

        option1Button.GetComponentInChildren<TextMeshProUGUI>().text = line.answerOption1; // Birinci se�ene�i ayarla
        option2Button.GetComponentInChildren<TextMeshProUGUI>().text = line.answerOption2; // �kinci se�ene�i ayarla

        option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump)); // Birinci buton t�klama olay�n� ayarla
        option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump)); // �kinci buton t�klama olay�n� ayarla
    }

    // Se�enek se�ildi�inde �a�r�lan fonksiyon
    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true; // Se�enek se�ildi olarak i�aretle
        DisableButtons(); // Butonlar� devre d��� b�rak
        currentDialogueIndex = indexJump; // Diyalog indeksini g�ncelle
    }

    // Metni yazd�ran coroutine
    private IEnumerator TypeText(string text)
    {
        dialogueText.text = ""; // Diyalog metnini s�f�rla

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter; // Her bir karakteri ekle
            yield return new WaitForSeconds(DialogueData.typingSpeed); // Yazma h�z�na g�re bekle
        }

        if (dialogueList[currentDialogueIndex].isEnd)
        {
            DisableButtons(); // Butonlar� devre d��� b�rak
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Kullan�c�n�n t�klamas�n� bekle
            DialogueStop(); // Diyalo�u durdur
        }

        currentDialogueIndex++; // Diyalog indeksini art�r
    }

    // Diyalo�u durduran fonksiyon
    private void DialogueStop()
    {
        // Bak�� noktas� canvas�n� yeniden etkinle�tir
        viewPointCanvas.SetActive(true);

        // T�m scriptleri yeniden etkinle�tir
        ScriptManager.Instance.SetAllScriptsActive(true);

        // Diyalog tetikleyicisinin konu�ma durumunu s�f�rla
        trigger.hasSpoken = false;

        // T�m coroutine'leri durdur
        StopAllCoroutines();

        // Diyalog metnini s�f�rla
        dialogueText.text = "";

        // Diyalog penceresini gizle
        dialogueParent.SetActive(false);

        // �mleci kilitle ve g�r�nmez yap
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
