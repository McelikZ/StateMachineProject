using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interaction
{
    public List<DialogueString> dialogueStrings = new List<DialogueString>();
    //[SerializeField] private Transform NPCTransform;

    internal bool hasSpoken = false;
    public override void UseViewPoint()
    {
        DialogueManager.Instance.DialogueStartingPreview(dialogueStrings);
    }
}

[System.Serializable]
public class DialogueString
{
    public string text; // NPC'nin söylediði metni temsil eder.
    public bool isEnd; // Konuþmanýn son satýrýný temsil eder.
    [Header("Branch")]
    public bool isQuestion;

    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}
