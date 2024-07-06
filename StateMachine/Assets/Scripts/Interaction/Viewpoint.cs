using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Viewpoint : MonoBehaviour
{
    [Header("Viewpoint")]
    [Space, SerializeField] Camera cam;
    [SerializeField] public GameObject PlayerController;
    [SerializeField] Image ImagePrefab;
    [Space, SerializeField, Range(0.1f, 20)] float MaxViewRange = 8;
    [SerializeField, Range(0.1f, 20)] float MaxTextViewRange = 3;
    [SerializeField] GameObject targetItem;
    float Distance;
    public Text ImageText;
    [SerializeField] internal Image ImageUI;
    void Start()
    {
        cam = Camera.main;
        PlayerController = GameObject.Find("PlayerController");
        ImageUI = Instantiate(ImagePrefab, GameObject.Find("ViewPointCanvas").transform);
        ImageText = ImageUI.GetComponentInChildren<Text>();
    }
    void Update()
    {
        ImageUI.transform.position = cam.WorldToScreenPoint(calculateWorldPosition(transform.position, cam));
        Distance = Vector3.Distance(PlayerController.transform.position, transform.position);

        if (Distance < MaxTextViewRange)
        {
            UseItem();
            Color OpacityColor = ImageText.color;
            OpacityColor.a = Mathf.Lerp(OpacityColor.a, 1, 10 * Time.deltaTime);
            ImageText.color = OpacityColor;
        }
        else
        {
            Color OpacityColor = ImageText.color;
            OpacityColor.a = Mathf.Lerp(OpacityColor.a, 0, 10 * Time.deltaTime);
            ImageText.color = OpacityColor;
        }

        if (Distance < MaxViewRange)
        {
            // UseItem();
            Color OpacityColor = ImageUI.color;
            OpacityColor.a = Mathf.Lerp(OpacityColor.a, 1, 10 * Time.deltaTime);
            ImageUI.color = OpacityColor;
        }
        else
        {
            Color OpacityColor = ImageUI.color;
            OpacityColor.a = Mathf.Lerp(OpacityColor.a, 0, 10 * Time.deltaTime);
            ImageUI.color = OpacityColor;
        }

    }
    private Vector3 calculateWorldPosition(Vector3 position, Camera camera)
    {
        Vector3 camNormal = camera.transform.forward;
        Vector3 vectorFromCam = position - camera.transform.position;
        float camNormDot = Vector3.Dot(camNormal, vectorFromCam.normalized);
        if (camNormDot <= 0f)
        {
            float camDot = Vector3.Dot(camNormal, vectorFromCam);
            Vector3 proj = (camNormal * camDot * 1.01f);
            position = camera.transform.position + (vectorFromCam - proj);
        }
        return position;
    }
    private void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            targetItem.GetComponent<Interaction>().UseViewPoint();
        }
    }

}

