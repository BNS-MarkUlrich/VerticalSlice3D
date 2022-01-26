using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool grab;
    private Camera MainCamera;
    [SerializeField] private Quaternion lastCameraPosition;
    private Quaternion newCameraPosition;
    
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


    private FPControl _controls;
    private FPControl.PlayerInputActions _inputControls;

    private InputParse inputParse;

    private float sensX;


    private void Start()
    {
        inputParse = FindObjectOfType<InputParse>();
        MainCamera = Camera.main;
        sensX = 3;
        number = 0.2f;
        _controls = new FPControl();
        _inputControls = _controls.PlayerInput;
        selectedParent = new GameObject("SelectedObject Handler"); // Pickup system
        selectedParent.transform.parent = Camera.main.transform; // Pickup system
        selectedParent.transform.localPosition = new Vector3(0, 0, 3); // Pickup system
        selectedParentOriginalPosition = selectedParent.transform.localPosition; // Pickup system
        selectedParent.transform.localRotation = new Quaternion(0, 0, 0, 0); // Pickup system
    }

    public void HoldRotate(InputAction.CallbackContext context)
    {
        inputParse._isRotating = true;
        inputParse._isLooking = false;
    }
    public void Grab(InputAction.CallbackContext context) 
    {
        StartCoroutine(grabbing());
        Debug.Log("grab");
    }
    public void Rotating(Vector2 pos)
    {
        selectedObject.transform.Rotate(0, -pos.x / 15, 0);
    }
    private void Update()
    {
        Debug.Log(grab);

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
                if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Selectable" && grab)
                {
                    Debug.Log("grabbing");
                    selectedObject = hitData.transform.gameObject; // Pickup system
                    selectedRigidBody = selectedObject.GetComponent<Rigidbody>();
                    currentState = States.InitialiseState;
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
                //selectedRigidBody.freezeRotation = true;
                number = 0.2f;
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
                if (grab) // Pickup system
                {
                    selectedRigidBody.freezeRotation = false; // Pickup system
                    selectedObject.transform.parent = transform.parent;  // Pickup system
                    selectedRigidBody.isKinematic = false;
                    selectedRigidBody.useGravity = true;
                    currentState = States.SelectionState;
                }
                break;
            case States.DropState:
                /// Initialise Begin
                inputParse._isLooking = true;
                inputParse._isRotating = false;
                collisionDetected = false;
                selectedRigidBody.GetComponent<Collider>().isTrigger = false;
                selectedObject.layer = 0;
                selectedObject.transform.parent = transform.parent;  // Pickup system
                selectedObject.transform.position = mouseWorldPoint;
                //selectedObject.transform.rotation = new Quaternion(0, 0, 0, 1); // Pickup system remove/change
                selectedRigidBody.isKinematic = false;
                selectedRigidBody.useGravity = true;
                /// Initialise End
                currentState = States.SelectionState;
                break;
            default:
                break;
        }

    }
    private IEnumerator grabbing()
    {
        grab = true;
        Debug.Log("yield");
        yield return new WaitForEndOfFrame();
        grab = false;
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
        GrabbingState,
        GrabbedState,
        DropState
    }
}