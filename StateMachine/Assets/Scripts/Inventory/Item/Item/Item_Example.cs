using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Example : ExpendableItem
{
    public override void UseItem()
    {
        base.UseItem();
        InventoryManager.Instance.UsedItemInfo("Elma");
    }
}
