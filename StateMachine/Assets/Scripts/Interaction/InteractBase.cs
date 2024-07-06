using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractBase : Interaction
{
    public GameObject viewPoint;

    public override void UseViewPoint()
    {
        viewPoint.GetComponent<Viewpoint>().ImageUI.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
