using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveTransform : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")]
    [SerializeField] public GameObject selectedObject;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 originalScale;

    [SerializeField] private Rigidbody selectedObjectRigidBody;
    [SerializeField] private SpringJoint selectedObjectSprintJoint;

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
        LayerMask mask = LayerMask.GetMask("Default");

        if (Physics.Raycast(ray, out hitData, 1000, mask) && hitData.transform.tag != "Selectable")
        {
            mousePositionWorld = hitData.point;
        }
        newDistance = Vector3.Distance(this.gameObject.transform.position, mousePositionWorld);

        switch (currentState)
        {
            case States.SelectionState:
                // Call Selection/Pickup System here???
                scaleModifier = 1;
                //selectedObjectRigidBody.useGravity = true;
                if (Input.GetMouseButtonDown(0)) // Must select object to begin. Remove once pickup system is done
                {
                    if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Selectable")
                    {
                        selectedObject = hitData.transform.gameObject; // Remove once pickup system is done
                        selectedObjectRigidBody = selectedObject.GetComponent<Rigidbody>();
                        currentState = States.InitialiseState;
                    }
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
                //selectedObject.transform.position = mousePositionWorld;
                //selectedObjectRigidBody.position = mousePositionWorld;
                //float mousePositionWorldY = mousePositionWorld.y + originalScale.y * scaleModifier / 2;
                //mousePositionWorld = new Vector3(mousePositionWorld.x, mousePositionWorldY, mousePositionWorld.z);
                //selectedObjectRigidBody.GetComponentInChildren<SpringJoint>().gameObject.transform.position = mousePositionWorld;
                selectedObjectRigidBody.position = mousePositionWorld;
                selectedObjectRigidBody.useGravity = false;
                selectedObjectRigidBody.transform.localScale = originalScale * scaleModifier;
                if (Input.GetMouseButtonDown(0)) // Remove once pickup system is done
                {
                    selectedObjectRigidBody.useGravity = true;
                    currentState = States.SelectionState;
                }
                break;
            default:
                break;
        }
    }

    private enum States
    {
        SelectionState,
        InitialiseState,
        GrabbedState
    }
}