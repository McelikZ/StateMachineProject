using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData/Dialogue_Data")]
public class DialogueData : ScriptableObject
{
    public float typingSpeed = 0.05f;
    public float turnSpeed = 2f;
}
