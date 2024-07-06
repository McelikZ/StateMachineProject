using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food, Equipment, Default, Bow, Sword
}
[System.Serializable]
public abstract class Item : ScriptableObject
{
    public GameObject prefab;
    public int ID;
    public Sprite itemIcon;
    public int amount;
    public ItemType type;
    public bool isStack;
    public string uniqueValue;
    
}
