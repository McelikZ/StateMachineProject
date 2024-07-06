using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class InventoryData : MonoBehaviour
{
    #region Singleton
    public static InventoryData Instance { get; private set; }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Inventory
    [Header("Inventory")]
    public List<InventorySlot> Inventory = new List<InventorySlot>();
    #endregion


    //#region Item List
    //[Header("Item List")]
    //[SerializeField] internal List<GameObject> ItemList = new List<GameObject>();
   
    //#endregion

    #region Item Position List
    [Header("Item Position")]
    [SerializeField] internal List<Transform> ItemListPos = new List<Transform>();
   
    #endregion

}