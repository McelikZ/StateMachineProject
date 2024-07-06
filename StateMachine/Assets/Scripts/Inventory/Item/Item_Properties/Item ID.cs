using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemID : MonoBehaviour
{
   
    public int ID;
    public Item item;

    public string uniqueValue;
    public TextMeshProUGUI amountText;
    private void Awake()
    {
        GenerateUniqueID();
    }

    private void Start()
    {
        RefreshID();
    }

    public int RefreshID()
    {
        ID = transform.parent.GetComponent<SlotID>().ID;
        item.ID = ID;
        return ID;
    }
    public void GenerateUniqueID()
    {
        uniqueValue = System.Guid.NewGuid().ToString(); // Benzersiz bir ID oluþtur
        item.uniqueValue = uniqueValue;
        Debug.Log("Generated Unique ID: " + uniqueValue);
    }
}
