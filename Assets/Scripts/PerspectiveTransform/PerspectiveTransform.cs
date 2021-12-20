using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveTransform : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")]
    public GameObject selectedObject;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 originalScale;

    [Header("Mouse Position")]
    [SerializeField] private Vector3 mousePositionWorld;

    [Header("Scale & Distance")]
    [SerializeField] private float newDistance;
    [SerializeField] private float originalDistance;
    [SerializeField] private float scaleModifier;

    [Header("States")]
    [SerializeField] private States currentState = States.SelectionState;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag != "Selectable")
        {
            mousePositionWorld = hitData.point;
        }
        newDistance = Vector3.Distance(this.gameObject.transform.position, mousePositionWorld);

        switch (currentState)
        {
            case States.SelectionState:
                // Call Selection/Pickup System here???
                scaleModifier = 1;
                if (Input.GetMouseButtonDown(0) && hitData.transform.tag == "Selectable") // Must select object to begin. Remove once pickup system is done
                {
                    selectedObject = hitData.transform.gameObject; // Remove once pickup system is done
                    currentState = States.InitialiseState;
                }
                break;
            case States.InitialiseState:
                // Initialise Begin
                originalPosition = selectedObject.transform.position;
                originalScale = selectedObject.transform.localScale;
                originalDistance = Vector3.Distance(this.gameObject.transform.position, originalPosition);
                // Initialise End
                currentState = States.GrabbedState;
                break;
            case States.GrabbedState:
                scaleModifier = newDistance / originalDistance;
                selectedObject.transform.position = mousePositionWorld;
                selectedObject.transform.localScale = originalScale * scaleModifier;
                if (Input.GetMouseButtonDown(0)) // Remove once pickup system is done
                {
                    currentState = States.SelectionState;
                }
                break;
            default:
                break;
        }
    }

    public enum States
    {
        SelectionState,
        InitialiseState,
        GrabbedState
    }
}