using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickedItem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryManager.Instance.clickedObject = gameObject;
            InventoryManager.Instance.toggleMenu.transform.position = InventoryManager.Instance.clickedObject.transform.position;
            InventoryManager.Instance.toggleMenu.SetActive(true);
        }
    }
   
}