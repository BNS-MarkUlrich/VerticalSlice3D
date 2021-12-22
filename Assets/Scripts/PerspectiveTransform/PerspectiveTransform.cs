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
        selectedParent.GetComponent<Collider>().isTrigger = true;
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
            mouseWorldPoint = new Vector3(hitData.point.x, hitData.point.y, hitData.point.z);
            Debug.DrawLine(this.gameObject.transform.position, hitData.point, Color.red);
            
        }
        newDistance = hitData.distance;

        switch (currentState)
        {
            case States.SelectionState:
                //scaleModifier = 0;
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

                selectedRigidBody.GetComponent<Collider>().isTrigger = true;
                selectedRigidBody.isKinematic = true;
                selectedRigidBody.useGravity = false;
                selectedObject.transform.parent = selectedParent.transform;  // Pickup system
                /// Initialise End
                currentState = States.GrabbedState;
                break;
            case States.GrabbedState:
                /// Grabbed Begin
                scaleModifier = newDistance / originalDistance;
                selectedParent.transform.localScale = originalScale * scaleModifier;
                selectedObject.transform.rotation = new Quaternion(0,0,0,1); // Pickup system remove/change
                LayerMask boxMask = LayerMask.GetMask("Default");
                collisionDetected = Physics.BoxCast(selectedParent.GetComponent<Collider>().bounds.center, transform.localScale, transform.forward, out boxHit, transform.rotation = Quaternion.identity, 1000, boxMask);
                if (collisionDetected)
                {
                    Vector3 mouseLocalPoint = Vector3.MoveTowards(transform.position, transform.position + transform.forward * boxHit.distance, boxHit.distance);
                    selectedObject.transform.position = mouseLocalPoint;
                }
                /// Grabbed End
                if (Input.GetMouseButtonDown(0)) // Pickup system
                {
                    collisionDetected = false;
                    selectedRigidBody.GetComponent<Collider>().isTrigger = false;
                    selectedObject.transform.parent = transform.parent;  // Pickup system
                    selectedObject.transform.position = mouseWorldPoint;
                    selectedObject.transform.rotation = new Quaternion(0, 0, 0, 1); // Pickup system remove/change
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