using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : ExpendableItem
{
    public override void UseItem()
    {
        base.UseItem();
        InventoryManager.Instance.UsedItemInfo("A�a�");
        Debug.Log("A�a� Kullan�ld�...");
    }
}
