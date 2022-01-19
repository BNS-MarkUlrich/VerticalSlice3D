using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")]
    [SerializeField] public GameObject selectedObject; // Pickup system
    [SerializeField] private Rigidbody selectedRigidBody;
    [SerializeField] private GameObject selectedParent; // Pickup system
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 originalScale;
    [SerializeField] private Vector3 selectedParentOriginalPosition;
    private float number;
    public bool collisionDetected;
    private Vector3 lastMousePosition;
    private PlaceHolderCameraController camerscript;
    [Header("Mouse Position")]
    [SerializeField] private Vector3 mouseWorldPoint;
    [SerializeField] private float moveForce = 250;

    [Header("Scale & Distance")]
    [SerializeField] private float newDistance;
    [SerializeField] private float originalDistance;
    [SerializeField] private float scaleModifier;

    [Header("Switch Cases")]
    [SerializeField] private States currentState = States.SelectionState;
    [SerializeField] private ScalingModes currentMode = ScalingModes.Legacy;

    private void Start()
    {
        camerscript = GetComponent<PlaceHolderCameraController>();
        number = 2;
        selectedParent = new GameObject("SelectedObject Handler"); // Pickup system
        selectedParent.transform.parent = Camera.main.transform; // Pickup system
        selectedParent.transform.localPosition = new Vector3(0, 0, 3); // Pickup system
        selectedParentOriginalPosition = selectedParent.transform.localPosition; // Pickup system
        selectedParent.transform.localRotation = new Quaternion(0, 0, 0, 0); // Pickup system
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        LayerMask mask = LayerMask.GetMask("Default");

        if (Physics.Raycast(ray, out hitData, 1000, mask) && hitData.transform.tag != "Selectable")
        {
            mouseWorldPoint = hitData.point;

            Vector3 incomingVec = hitData.point - this.gameObject.transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, -incomingVec);
            Debug.DrawLine(this.gameObject.transform.position, hitData.point, Color.red);

            /*RaycastHit normhit;
            if (Physics.Raycast(hitData.point, reflectVec, out normhit))
            {
                Debug.DrawLine(hitData.point, normhit.point, Color.green);
            }*/
        }
        newDistance = hitData.distance;

        switch (currentState)
        {
            case States.SelectionState:
                scaleModifier = 1;
                /// Call Selection/Pickup System here
                if (Input.GetMouseButtonDown(0)) // Must select object to begin. Pickup system
                {
                    if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Selectable")
                    {
                        selectedObject = hitData.transform.gameObject; // Pickup system
                        selectedRigidBody = selectedObject.GetComponent<Rigidbody>();
                        currentState = States.InitialiseState;
                    }
                }
                break;
            case States.InitialiseState:
                /// Initialise Begin
                originalPosition = selectedObject.transform.position;
                originalScale = selectedObject.transform.localScale;

                originalDistance = Vector3.Distance(this.gameObject.transform.position, originalPosition);

                selectedRigidBody.isKinematic = false;
                selectedRigidBody.useGravity = true;
                selectedObject.transform.parent = selectedParent.transform;  // Pickup system
                selectedRigidBody.freezeRotation = true;
                number = 2;
                /// Initialise End
                currentState = States.GrabbedState;
                break;
            case States.GrabbedState:
                /// Grabbed Begin
                scaleModifier = newDistance / originalDistance;
                selectedObject.transform.localScale = originalScale * scaleModifier;
                // lets the selected object rotate on pickup
                if (number >= 0)
                {
                    number -= Time.deltaTime;
                    Quaternion rotate = Quaternion.Euler(transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y, 0);
                    selectedObject.transform.rotation = Quaternion.Lerp(selectedObject.transform.rotation, rotate, 6 * Time.deltaTime);
                }
                // is to rotate when pressing right mouse button

                if (Input.GetKey(KeyCode.Mouse1) && mouseWorldPoint.x < originalPosition.x)
                {
                    selectedObject.transform.Rotate(0, 1 * Time.deltaTime, 1);
                    Debug.Log("m2");
                    lastMousePosition = originalPosition;
                    camerscript.enabled = false;
                }
                else if (Input.GetKey(KeyCode.Mouse1) && lastMousePosition.x > originalPosition.x)
                {
                    selectedObject.transform.Rotate(0, 10 * Time.deltaTime, -1);
                    Debug.Log("m2");
                    lastMousePosition = originalPosition;
                    camerscript.enabled = false;
                }
                else 
                {
                    camerscript.enabled = true;
                }
                if (mouseWorldPoint.y < selectedObject.transform.localScale.y / 2)
                {
                    currentMode = ScalingModes.Legacy;
                }
                else
                {
                    currentMode = ScalingModes.Modern;
                }
                switch (currentMode)
                {
                    case ScalingModes.Modern:
                        /// Option 1: (Accurate, looks like source material, doesn't follow mouse centre, jittery)
                        /// Modern Begin
                        selectedParent.transform.position = mouseWorldPoint;
                        selectedObject.transform.parent = selectedParent.transform;
                        selectedObject.transform.position = selectedParent.transform.position;
                        Vector3 moveDirection = (selectedParent.transform.position - selectedObject.transform.position);
                        selectedRigidBody.AddForce(moveDirection * moveForce);
                        //selectedObject.transform.localPosition = new Vector3(0, 0, mouseWorldPoint.z - selectedObject.transform.localScale.z);
                        selectedRigidBody.isKinematic = true;
                        selectedRigidBody.useGravity = false;
                        /// Modern End
                        break;
                    case ScalingModes.Legacy:
                        /// Option 2: (Fast, smooth, looks clean, clipping, unstable)
                        /// Legacy Begin
                        selectedParent.transform.localPosition = selectedParentOriginalPosition;
                        float holdDistance = newDistance - mouseWorldPoint.z;
                        Vector3 newWorldPoint = new Vector3(0, 0, holdDistance);
                        //selectedObject.transform.localPosition = newWorldPoint;
                        //selectedObject.transform.position = mouseWorldPoint;
                        selectedObject.transform.position = new Vector3(mouseWorldPoint.x, selectedObject.transform.localScale.y / 2, mouseWorldPoint.z);
                        // selectedObject.transform.position = selectedParent.transform.position; // Pickup system
                        selectedRigidBody.isKinematic = true;
                        selectedRigidBody.useGravity = false;
                        /// Legacy End
                        break;
                    default:
                        break;
                }
                /// Grabbed End
                if (Input.GetMouseButtonDown(0)) // Pickup system
                {
                    selectedRigidBody.freezeRotation = false; // Pickup system
                    selectedObject.transform.parent = transform.parent;  // Pickup system
                    selectedRigidBody.isKinematic = false;
                    selectedRigidBody.useGravity = true;
                    currentState = States.SelectionState;
                }
                break;
            default:
                break;
        }
    }

    private enum ScalingModes
    {
        Modern,
        Legacy
    }

    private enum States
    {
        SelectionState,
        InitialiseState,
        GrabbedState
    }
    private void rotate()
    {
        
    }
}