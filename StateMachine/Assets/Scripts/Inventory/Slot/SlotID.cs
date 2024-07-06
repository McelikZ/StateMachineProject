using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotID : MonoBehaviour
{
   [SerializeField] internal int ID;
    public GameObject parent;
    private void Awake()
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i) == this.transform)
            {
                ID = i;
                break;
            }
        }
    }
}
