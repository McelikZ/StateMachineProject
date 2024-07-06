using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager Instance { get; private set; }
    #endregion

    #region Enum
    public enum InventoryMode
    {
        Loading,
        Loot
    }

    internal InventoryMode currentInventoryMode = InventoryMode.Loading;
    #endregion

    public string uniqueValues;
    [SerializeField] internal GameObject clickedObject;
    internal GameObject spawnedItem;
    [SerializeField] internal GameObject toggleMenu;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] GameObject usedItemPanel;


    #region Unity Methods

    private void Awake()
    {
        Instance = this;
        currentInventoryMode = InventoryMode.Loading;
    }
    private void OnApplicationQuit()
    {
        InventorySaveSystem.instance.JsonSave();
    }
    private void Start()
    {
        InitializeInventory();
    }
    private void Update()
    {
        CanvasEnabledControl();

    }
    #endregion

    #region Item Management
    internal void AddItem(GameObject target, Item item, int amount, int ID, string uniqueValue)
    {
        bool hasItem = InventoryData.Instance.Inventory.Exists(slot => slot.Item == item);



        if (hasItem && item.isStack)
        {
            var amountItem = InventoryData.Instance.Inventory.Find(slot => slot.Item == item);
            amountItem.AddAmount(amount);
            RefreshTextAmount();
        }
        else
        {
            AddItemToList(target, InventoryData.Instance.ItemListPos, item, ID, uniqueValue, amount);

            Debug.LogWarning("Adding Item - Prefab: " + target + ", ID: " + ID + ", Item: " + item.name + ", Amount: " + amount);

        }
    }
    private void AddItemToList(GameObject target, List<Transform> targetListPos, Item item, int ID, string uniqueValue, int amount)
    {
        for (int i = 0; i < targetListPos.Count; i++)
        {
            if (targetListPos[i].transform.childCount == 0)
            {
                if (currentInventoryMode == InventoryMode.Loot)
                {
                    Debug.Log("Yeni hedef_LOOT:" + target);
                    spawnedItem = Instantiate(target, targetListPos[i]);
                    spawnedItem.GetComponent<ItemID>().amountText.text = "x" + amount.ToString();
                    uniqueValues = spawnedItem.GetComponent<ItemID>().uniqueValue;
                    InventoryData.Instance.Inventory.Add(new InventorySlot(target, item, amount, ID, uniqueValues));

                    break;
                }
                if (currentInventoryMode == InventoryMode.Loading)
                {
                    // Slot'un ID'si hedef ID ile eþleþiyorsa devam et
                    if (targetListPos[i].GetComponent<SlotID>().ID == ID)
                    {
                        Debug.Log("Yeni hedef_LOADÝNG:" + target);
                        spawnedItem = Instantiate(target, targetListPos[i]);
                        InventoryData.Instance.Inventory.Add(new InventorySlot(target, item, amount, ID, uniqueValue));

                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Tüm slotlar dolu");
            }
        }
    }
    internal void RefreshTextAmount()
    {
        foreach (var item in InventoryData.Instance.Inventory)
        {
            foreach (var slot in InventoryData.Instance.ItemListPos)
            {
                if (slot.childCount > 0)
                {
                    if (slot.GetChild(0).GetComponent<ItemID>().item == item.Item)
                    {
                        slot.GetChild(0).GetComponent<ItemID>().amountText.text = "X" + item.Amount.ToString();
                    }
                }
            }
        }
    }
    internal void RefreshInventoryID()
    {
        foreach (Transform item in InventoryData.Instance.ItemListPos)
        {
            if (item != null)
            {
                if (item.childCount > 0)
                {
                    GameObject targetItem = item.gameObject;

                    foreach (var inventoryItem in InventoryData.Instance.Inventory)
                    {
                        var itemIDComponent = targetItem.transform.GetChild(0).GetComponent<ItemID>();

                        if (itemIDComponent.uniqueValue == inventoryItem.UniqueValue)
                        {
                            inventoryItem.ID = itemIDComponent.ID;
                            Debug.Log("Item ID Updated...");
                        }
                    };
                }
            }

        }
    }
    private void InitializeInventory()
    {
        inventoryCanvas.SetActive(true);
        inventoryCanvas.GetComponent<Canvas>().enabled = false;
    }
    private void CanvasEnabledControl()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = true;
            ScriptManager.Instance.SetAllScriptsActive(false);
            SetCursorState(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryCanvas.GetComponent<Canvas>().enabled = false;
            ScriptManager.Instance.SetAllScriptsActive(true);
            SetCursorState(false);
        }
    }
    private void SetCursorState(bool active)
    {
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }
    internal void UsedItemInfo(string text)
    {
        usedItemPanel.SetActive(true);
        usedItemPanel.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = text + " Kullanýldý...";
    }

    #endregion

    #region Menu Management

    #endregion
}
#region InventorySlot
[System.Serializable]
public class InventorySlot
{
    public GameObject ItemObject;
    public Item Item;
    public int Amount;
    public int ID;
    public string UniqueValue;

    public InventorySlot(GameObject target, Item item, int amount, int itemID, string uniqueValue)
    {
        ItemObject = target;
        Item = item;
        Amount = amount;
        ID = itemID;
        UniqueValue = uniqueValue;
    }

    public void AddAmount(int value)
    {
        Amount += value;
    }
}

#endregion