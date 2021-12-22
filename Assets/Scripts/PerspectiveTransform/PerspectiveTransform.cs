using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerspectiveTransform : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")]
    [SerializeField] public GameObject selectedObject; // Pickup system
    [SerializeField] private Rigidbody selectedRigidBody;
    [SerializeField] private GameObject selectedParent; // Pickup system

    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Quaternion originalRotation;
    [SerializeField] private Vector3 originalScale;

    public bool collisionDetected;

    [Header("Mouse Position")]
    [SerializeField] private Vector3 mouseWorldPoint;

    [Header("Scale & Distance")]
    [SerializeField] private float newDistance;
    [SerializeField] private float originalDistance;
    [SerializeField] private float scaleModifier;

    [SerializeField] private RaycastHit boxHit;

    [Header("Switch Cases")]
    [SerializeField] private States currentState = States.SelectionState;

    private void Start()
    {
        selectedParent = new GameObject("SelectedObject Handler"); // Pickup system
        selectedParent.AddComponent<BoxCollider>();
        selectedParent.GetComponent<BoxCollider>().isTrigger = true;
        selectedParent.layer = 6;
        selectedParent.transform.parent = Camera.main.transform; // Pickup system
        selectedParent.transform.localPosition = new Vector3(0, 0, 0); // Pickup system
        selectedParent.transform.localRotation = new Quaternion(0, 0, 0, 0); // Pickup system
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitData;
        LayerMask mask = LayerMask.GetMask("Selectables");

        /// Call Selection/Pickup System here
        if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Selectable")
        {
            Vector3 mouseLocalPoint = Vector3.MoveTowards(transform.position, transform.position + transform.forward * hitData.distance, hitData.distance);
            mouseWorldPoint = new Vector3(hitData.point.x, hitData.point.y, mouseLocalPoint.z);
            Debug.DrawLine(this.gameObject.transform.position, hitData.point, Color.red);
            
        }
        newDistance = hitData.distance;

        switch (currentState)
        {
            case States.SelectionState:
                scaleModifier = 1;
                if (Input.GetMouseButtonDown(0)) // Must select object to begin. Pickup system
                {
                    if (Physics.Raycast(ray, out hitData, 1000, mask) && hitData.transform.tag == "Selectable")
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
                originalRotation = selectedObject.transform.worldToLocalMatrix.rotation;
                originalScale = selectedObject.transform.localScale;
                originalDistance = Vector3.Distance(this.gameObject.transform.position, originalPosition);
                
                selectedRigidBody.isKinematic = true;
                selectedRigidBody.useGravity = true;
                selectedObject.transform.parent = selectedParent.transform;  // Pickup system
                /// Initialise End
                currentState = States.GrabbedState;
                break;
            case States.GrabbedState:
                /// Grabbed Begin
                scaleModifier = newDistance / originalDistance;
                selectedObject.transform.localScale = originalScale * scaleModifier;
                selectedParent.transform.localScale = selectedObject.transform.localScale;
                LayerMask boxMask = LayerMask.GetMask("Default");
                collisionDetected = Physics.BoxCast(selectedParent.GetComponent<Collider>().bounds.center, selectedParent.transform.localScale, transform.forward, out boxHit, transform.rotation = Quaternion.identity, 1000, boxMask);
                if (collisionDetected)
                {
                    Vector3 mouseLocalPoint = Vector3.MoveTowards(transform.position, transform.position + transform.forward * boxHit.distance, boxHit.distance);
                    selectedObject.transform.position = mouseLocalPoint;
                    selectedObject.transform.localRotation = boxHit.transform.rotation;
                }
                /// Grabbed End
                if (Input.GetMouseButtonDown(0)) // Pickup system
                {
                    collisionDetected = false;
                    selectedObject.transform.parent = transform.parent;  // Pickup system
                    selectedObject.transform.position = mouseWorldPoint;
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
        Legacy,
        NormHit,
        NormHitFloor
    }
    
    private enum States
    {
        SelectionState,
        InitialiseState,
        GrabbedState
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (collisionDetected)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * boxHit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * boxHit.distance, transform.localScale);
        }
    }
}