using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveTransform : MonoBehaviour
{
    [Header("Selected Object")]
    public GameObject selectedObject;
    private Vector3 originalPosition;
    private Vector3 originalScale;

    [Header("Mouse Position")]
    private Vector3 mousePositionWorld;

    [Header("Scale & Distance")]
    private float newDistance;
    private float originalDistance;
    private float scaleModifier;

    /*private void Start()
    {
        originalPosition = selectedObject.transform.position;
        originalScale = selectedObject.transform.localScale;
        originalDistance = Vector3.Distance(this.gameObject.transform.position, originalPosition);
    }*/

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag != "Selectable")
        {
            mousePositionWorld = hitData.point;
            //distance = Vector3.Distance(this.gameObject.transform.position, mousePositionWorld);
            newDistance = Vector3.Distance(this.gameObject.transform.position, mousePositionWorld);
        }
        scaleModifier = newDistance / originalDistance;

        SetSelectedGameObject(true);
    }

    public void PerspectiveTransformScale()
    {
        selectedObject.transform.position = mousePositionWorld;
        selectedObject.transform.localScale = originalScale * scaleModifier;
    }

    public void InitialiseSelectedObject()
    {
        originalPosition = selectedObject.transform.position;
        originalScale = selectedObject.transform.localScale;
        originalDistance = Vector3.Distance(this.gameObject.transform.position, originalPosition);
    }

    public void SetSelectedGameObject(bool selected)
    {
        if (selected == true && selectedObject != null)
        {
            //InvokeRepeating("PerspectiveTransformScale", 0, 0);
            PerspectiveTransformScale();
            Invoke("InitialiseSelectedObject", 0);
        }
        else
        {
            CancelInvoke();
        }
    }
}