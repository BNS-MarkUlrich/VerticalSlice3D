using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")] 
    private float number;
    public bool grab;
    private Camera MainCamera;
    [SerializeField] private Quaternion lastCameraPosition;
    private Quaternion newCameraPosition;

    public GameObject pickUpObject;
    private Rigidbody pickUpRigidObject;

    private FPControl _controls;
    private FPControl.PlayerInputActions _inputControls;

    private InputParse inputParse;

    private float sensX;

    private NewPersTransform persTransform;

    private void Start()
    {
        inputParse = FindObjectOfType<InputParse>();
        MainCamera = Camera.main;
        sensX = 3;
        number = 0.2f;
        _controls = new FPControl();
        _inputControls = _controls.PlayerInput;
    }

    public void HoldRotate(InputAction.CallbackContext context)
    {
        inputParse._isRotating = true;
        inputParse._isLooking = false;
    }
    public void Grab(InputAction.CallbackContext context) 
    {
        StartCoroutine(grabbing());
        //Debug.Log("grab");
    }
    public void Rotating(Vector2 pos)
    {
        pickUpObject.transform.Rotate(0, -pos.x / 15, 0);
    }
    private void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hitData;

        pickUpObject = FindObjectOfType<NewPersTransform>().selectedObject;

        if (pickUpObject != null)
        {
            persTransform = FindObjectOfType<NewPersTransform>();
            /*if (persTransform.selection == true)
            {
                scaleModifier = 1; // Reset scale modifier
                if (Input.GetMouseButtonDown(0)) // Must select object to begin.
                {
                    if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Selectable") // Check for selectables
                    {
                        selectedObject = hitData.transform.gameObject; // Selected object is now grabbed
                        selectedRigidBody = selectedObject.GetComponent<Rigidbody>();
                        selectedObject.layer = 2;
                        currentState = States.InitialiseState;
                    }
                }
            }*/
            if (persTransform.initialise == true)
            {

                /// Initialise Begin
                number = 0.2f;
                /// Initialise End
            }
            if (persTransform.grabbed == true)
            {
                /// Grabbed Begin
                // lets the selected object rotate on pickup
                if (number >= 0)
                {
                    number -= Time.deltaTime;
                    Quaternion rotate = Quaternion.Euler(transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y, 0);
                    pickUpObject.transform.rotation = Quaternion.Lerp(pickUpObject.transform.rotation, rotate, 6 * Time.deltaTime);
                }
                /// Grabbed End
                if (grab) // Pickup system
                {
                    pickUpRigidObject = pickUpObject.GetComponent<Rigidbody>();
                    pickUpRigidObject.freezeRotation = false; // Pickup system
                    pickUpObject.transform.parent = transform.parent;  // Pickup system
                    pickUpRigidObject.isKinematic = false;
                    pickUpRigidObject.useGravity = true;
                }
                if (persTransform.dropped == true)
                {
                    inputParse._isLooking = true;
                    inputParse._isRotating = false;
                }
            }
        }
    }
    private IEnumerator grabbing()
    {
        grab = true;
        //Debug.Log("yield");
        yield return new WaitForEndOfFrame();
        grab = false;
    }
}