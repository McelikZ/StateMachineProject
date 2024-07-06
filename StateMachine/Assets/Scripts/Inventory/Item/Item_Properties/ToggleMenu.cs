using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public void UseButtonClick()
    {
        InventoryManager.Instance.clickedObject.GetComponent<ExpendableItem>().UseItem();
    }
    public void DropButton()
    {
        DropItem(1);

    }
    public void DropItem(int dropAmount)
    {

        for (int i = 0; i < InventoryData.Instance.Inventory.Count; i++)
        {
            Debug.Log("Drop Çalýþýyor...");
            if (InventoryData.Instance.Inventory[i].UniqueValue == InventoryManager.Instance.clickedObject.GetComponent<ItemID>().uniqueValue)
            {
                if (InventoryData.Instance.Inventory[i].Amount > 0)
                {
                    InventoryManager.Instance.clickedObject.GetComponent<ItemID>().amountText.text = "x" + (InventoryData.Instance.Inventory[i].Amount - 1).ToString();
                    InventoryData.Instance.Inventory[i].Amount -= dropAmount;
                    if (InventoryData.Instance.Inventory[i].Amount == 0)
                    {
                        InventoryData.Instance.Inventory.RemoveAt(i);
                        Destroy(InventoryManager.Instance.clickedObject);
                    }
                }

            }
        }
    }
}
