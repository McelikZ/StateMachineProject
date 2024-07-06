using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class InventorySaveSystem : MonoBehaviour
{
    #region Singleton
    public static InventorySaveSystem instance;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        JsonLoad();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            RemoveJsonFile();
        }
    }
    #endregion

    #region Json Save Functions

    private string InventoryFilePath
    {
        get
        {
            string savePath = Path.Combine(Application.persistentDataPath, "Saves");
            return Path.Combine(savePath, "Inventory.json");
        }
    }

    public void JsonSave()
    {
        List<string> jsonList = new List<string>();

        foreach (var item in InventoryData.Instance.Inventory)
        {
            string jsonString = JsonUtility.ToJson(item);
            jsonList.Add(jsonString);
        }

        string filePath = InventoryFilePath;

        try
        {
            string savePath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            File.WriteAllText(filePath, string.Join("\n", jsonList.ToArray()));
            Debug.Log("Inventory saved successfully to: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving inventory: {e.Message}");
        }
    }

    public void JsonLoad()
    {
        string filePath = InventoryFilePath;

        if (File.Exists(filePath))
        {
            try
            {
                string[] jsonLines = File.ReadAllLines(filePath);

                foreach (string jsonLine in jsonLines)
                {
                    Debug.Log("Loaded JSON: " + jsonLine);
                    var slot = JsonUtility.FromJson<InventorySlot>(jsonLine);
                    if (slot.ItemObject != null) // Add this check to avoid null reference
                    {
                        InventoryManager.Instance.AddItem(slot.ItemObject, slot.Item, slot.Amount, slot.ID, slot.UniqueValue);
                        InventoryManager.Instance.spawnedItem.GetComponent<ItemID>().uniqueValue = slot.UniqueValue;
                        InventoryManager.Instance.spawnedItem.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "X" + slot.Amount.ToString();

                    }
                    else
                    {
                        Debug.LogWarning("ItemObject is null for the loaded item.");
                    }
                }

                Debug.Log("Inventory loaded successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error loading inventory: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning("Inventory file does not exist. Creating a new one.");
            CreateDefaultInventoryFile(filePath);
        }
    }

    private void CreateDefaultInventoryFile(string filePath)
    {
        List<InventorySlot> defaultInventory = new List<InventorySlot>();
        // Add default inventory items if necessary.

        string defaultJson = string.Join("\n", defaultInventory.ConvertAll(slot => JsonUtility.ToJson(slot)));

        File.WriteAllText(filePath, defaultJson);

        Debug.Log("Default inventory file created.");
    }

    private void RemoveJsonFile()
    {
        string filePath = InventoryFilePath;

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Inventory.json file removed successfully.");
        }
        else
        {
            Debug.LogWarning("Inventory.json file does not exist.");
        }
    }

    #endregion
}
