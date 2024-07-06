using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wood : ExpendableItem
{
    public override void UseItem()
    {
        base.UseItem();
        InventoryManager.Instance.UsedItemInfo("Taþ");
        Debug.Log("Taþ Kullanýldý...");
    }
}
