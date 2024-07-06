using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            Drag drag = dropped.GetComponent<Drag>();
            if (drag != null)
            {
                drag.parentAfterDrag = transform;
            }
        }
    }
}
