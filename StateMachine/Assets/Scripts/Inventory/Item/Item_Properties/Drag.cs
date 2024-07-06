using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Public Fields
    public static Drag Instance;
    #endregion

    #region Internal Fields
    internal Transform parentAfterDrag;
    #endregion

    #region Private Fields
    [SerializeField] private Image image;
    private ItemID itemID;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        itemID = GetComponent<ItemID>();
    }
    #endregion

    #region Drag Functions
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        itemID.RefreshID();
        InventoryManager.Instance.RefreshInventoryID();
        InventorySaveSystem.instance.JsonSave();
    }
    #endregion
}
