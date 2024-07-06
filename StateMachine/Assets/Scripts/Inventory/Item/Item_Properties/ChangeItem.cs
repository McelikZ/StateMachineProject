using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private Animator hand;
    public void ItemChange()
    {

        if (PlayerInput.Instance.changeItemCheck)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == PlayerInput.Instance.selectedItemValue)
                {
                    Debug.LogError("Ýtem Deðiþti");
                    hand.gameObject.GetComponent<Animator>().SetBool("Hide", true);
                    Invoke(nameof(ItemEnabled), 0.2f);
                }

                else
                    items[i].gameObject.SetActive(false);
            }
            PlayerInput.Instance.changeItemCheck = false;
            Invoke(nameof(HideItem), 0.2f);

        }

    }
    private void Update() => ItemChange();
    private void HideItem() => hand.gameObject.GetComponent<Animator>().SetBool("Hide", false);
    private void ItemEnabled() => items[PlayerInput.Instance.selectedItemValue].gameObject.SetActive(true);
}