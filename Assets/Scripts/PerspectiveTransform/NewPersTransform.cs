using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPersTransform : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")]
    [SerializeField] public GameObject selectedObject; // Pickup system
    [SerializeField] private Rigidbody selectedRigidBody;
    [SerializeField] private GameObject selectedParent; // Pickup system
    [SerializeField] private LayerMask selectableLayer;

    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Quaternion originalRotation;
    [SerializeField] private Vector3 originalScale;

    public bool collisionDetected;

    [Header("Mouse Position")]
    private RaycastHit collisionHit;
    private RaycastHit hitData;

    [Header("Scale & Distance")]
    [SerializeField] private float incrementDistance;
    [SerializeField] private float newDistance;
    [SerializeField] private float originalDistance;
    [SerializeField] private float scaleModifier;

    [SerializeField] private float shortestDistance;

    private RaycastHit boxHit;
    private RaycastHit[] boxHits;

    private Collider[] overlapHits;

    [SerializeField] private Vector3 originalBoxCastPos;
    [SerializeField] private Vector3 newBoxCastPos;

    [Header("Switch Cases")]
    [SerializeField] private States currentState = States.SelectionState;

    private void Start()
    {
        selectedParent = new GameObject("SelectedObject Handler"); // Pickup system
        selectedParent.AddComponent<BoxCollider>();
        selectedParent.GetComponent<Collider>().isTrigger = true;
        selectedParent.layer = 2;
        selectedParent.transform.parent = Camera.main.transform; // Pickup system
        selectedParent.transform.localPosition = new Vector3(0, 0, 0); // Pickup system
        selectedParent.transform.localRotation = new Quaternion(0, 0, 0, 0); // Pickup system
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out collisionHit, 1000)) // Check for collision in level
        {
           Debug.DrawLine(this.gameObject.transform.position, collisionHit.point, Color.blue);
        }

        switch (currentState)
        {
            case States.SelectionState:
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
                break;
            case States.InitialiseState:
                /// Initialise Begin
                // Get original data
                originalPosition = selectedObject.transform.position;
                originalRotation = selectedObject.transform.rotation;
                originalScale = selectedObject.transform.localScale;
                originalDistance = Vector3.Distance(transform.position, originalPosition);
                //originalDistance = hitData.distance;
                // !Get original data

                // Rigidbody setup
                selectedRigidBody.GetComponent<Collider>().isTrigger = true;
                selectedRigidBody.isKinematic = true;
                selectedRigidBody.useGravity = false;
                // !Rigidbody setup

                //selectedObject.transform.parent = selectedParent.transform;  // Set parent
                /// Initialise End
                currentState = States.GrabbedState;
                break;
            case States.InitialiseTwoState:
                if (originalScale.z >= 2)
                {
                    
                }
                else
                {
                    currentState = States.GrabbedState;
                }
                break;
            case States.GrabbedState:
                /// Grabbed Begin
                collisionDetected = true;
                newDistance = Vector3.Distance(transform.position, selectedObject.transform.position);
                scaleModifier = newDistance / originalDistance;
                selectedObject.transform.localScale = originalScale * scaleModifier; // Set scale
                incrementDistance = collisionHit.distance / 10;
                originalBoxCastPos = transform.position;
                for (int i = 0; i < 10; i++)
                {
                    if (i < 10)
                    {
                        newBoxCastPos = Vector3.MoveTowards(originalBoxCastPos, originalBoxCastPos + transform.forward * incrementDistance, incrementDistance); // Calculate optimal position
                        boxHits = Physics.BoxCastAll(originalBoxCastPos, (originalScale * scaleModifier)/2, transform.forward, selectedObject.transform.rotation, incrementDistance);
                        shortestDistance = Vector3.Distance(originalBoxCastPos, newBoxCastPos); ; // Max short distance
                        for (int y = 0; y < boxHits.Length; y++) // Get minimal distance in array
                        {
                            Debug.Log(boxHits[y].transform.name);
                            if (shortestDistance > boxHits[y].distance && boxHits[y].transform.tag != "Player")
                            {
                                shortestDistance = boxHits[y].distance;
                            }
                        }
                        selectedObject.transform.position = Vector3.MoveTowards(selectedObject.transform.position, originalBoxCastPos + transform.forward * shortestDistance, shortestDistance); // Calculate optimal position
                        Debug.DrawLine(originalBoxCastPos, newBoxCastPos, Color.red);
                    }
                    originalBoxCastPos = selectedObject.transform.position;
                }
                if (Input.GetMouseButtonDown(0)) // Click to let go of object
                {
                    currentState = States.DropState;
                }
                /// Grabbed End
                break;
            case States.DropState:
                /// Drop Begin
                collisionDetected = false;
                selectedRigidBody.GetComponent<Collider>().isTrigger = false;
                selectedObject.layer = 0;
                selectedRigidBody.isKinematic = false;
                selectedRigidBody.useGravity = true;
                selectedObject.transform.parent = this.transform.parent.transform.parent;  // Pickup system
                /// Drop End
                currentState = States.SelectionState;
                break;
            default:
                break;
        }
    }

    private enum States
    {
        SelectionState,
        InitialiseState,
        InitialiseTwoState,
        GrabbedState,
        DropState
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        //Check if there has been a hit yet
        if (collisionDetected)
        {
            Gizmos.color = Color.red;
            //Draw a Ray forward from GameObject toward the hit
            //Gizmos.DrawRay(transform.position, transform.forward * boxHit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(selectedObject.transform.position, originalScale * scaleModifier);
            //Gizmos.DrawMesh(selectedObject.GetComponent<Mesh>(), transform.position + transform.forward * boxHit.distance, selectedParent.transform.rotation, selectedParent.transform.localScale);
        }
    }
}
