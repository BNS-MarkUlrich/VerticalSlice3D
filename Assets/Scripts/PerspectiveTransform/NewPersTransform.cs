using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPersTransform : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")]
    [SerializeField] public GameObject selectedObject; // Pickup system
    [SerializeField] protected Rigidbody selectedRigidBody;
    [SerializeField] protected GameObject selectedParent; // Pickup system
    [SerializeField] protected LayerMask selectableLayer;

    [SerializeField] protected Vector3 originalPosition;
    [SerializeField] protected Quaternion originalRotation;
    [SerializeField] protected Vector3 originalScale;

    public bool collisionDetected;

    [Header("Mouse Position")]
    protected RaycastHit collisionHit;
    protected RaycastHit hitData;

    [Header("Scale & Distance")]
    [SerializeField] protected float incrementDistance;
    [SerializeField] protected float newDistance;
    [SerializeField] protected float originalDistance;
    [SerializeField] protected float scaleModifier;

    [SerializeField] protected float shortestDistance;

    protected RaycastHit boxHit;
    protected RaycastHit[] boxHits;

    protected Collider[] overlapHits;

    [SerializeField] protected Vector3 originalBoxCastPos;
    [SerializeField] protected Vector3 newBoxCastPos;

    [Header("Bools")]
    [SerializeField] public bool selection;
    [SerializeField] public bool initialise;
    [SerializeField] public bool grabbed;
    [SerializeField] public bool dropped;

    [Header("Switch Cases")]
    [SerializeField] public States currentState = States.SelectionState;

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
                selection = true;
                initialise = false;
                grabbed = false;
                dropped = false;
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
                selection = false;
                initialise = true;
                grabbed = false;
                dropped = false;
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
            case States.GrabbedState:
                selection = false;
                initialise = false;
                grabbed = true;
                dropped = false;
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
                            //Debug.Log(boxHits[y].transform.name);
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
                selection = false;
                initialise = false;
                grabbed = false;
                dropped = true;
                /// Initialise Begin
                collisionDetected = false;
                selectedRigidBody.GetComponent<Collider>().isTrigger = false;
                selectedObject.layer = 0;
                selectedRigidBody.isKinematic = false;
                selectedRigidBody.useGravity = true;
                selectedObject.transform.parent = this.transform.parent.transform.parent;  // Pickup system
                /// Initialise End
                currentState = States.SelectionState;
                break;
        }
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    public States GetCurrentState()
    {
        return currentState;
    }

    public enum States
    {
        SelectionState,
        InitialiseState,
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
