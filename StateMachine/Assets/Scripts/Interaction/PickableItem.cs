using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InteractBase
{
    public Item item;

  
    public override void UseViewPoint()
    {
        base.UseViewPoint();
        PickItem();
    }
    public void PickItem()
    {
        InventoryManager.Instance.currentInventoryMode = InventoryManager.InventoryMode.Loot;
        InventoryManager.Instance.AddItem(item.prefab, item, item.amount, item.ID, item.uniqueValue);
        Debug.Log("Pickledi");
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        InventoryManager.Instance.RefreshInventoryID();
    }

}
