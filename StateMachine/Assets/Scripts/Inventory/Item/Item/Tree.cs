using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ExpendableItem
{
    public override void UseItem()
    {
        base.UseItem();
        InventoryManager.Instance.UsedItemInfo("Aðaç");
        Debug.Log("Aðaç Kullanýldý...");
    }
}
