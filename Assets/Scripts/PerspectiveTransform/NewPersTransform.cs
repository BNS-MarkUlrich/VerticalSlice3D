using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPersTransform : MonoBehaviour
{
    // Remove [SerializeField] later
    [Header("Selected Object")]
    public GameObject selectedObject; // Pickup system
    protected Rigidbody selectedRigidBody;
    protected GameObject selectedParent; // Pickup system
    protected LayerMask selectableLayer;

    protected Vector3 originalPosition;
    protected Quaternion originalRotation;
    protected Vector3 originalScale;

    public bool collisionDetected;

    [Header("Mouse Position")]
    protected RaycastHit collisionHit;
    protected RaycastHit hitData;

    [Header("Scale & Distance")]
    protected float incrementDistance;
    [SerializeField] protected float incrementAmount = 10;
    protected float newDistance;
    protected float originalDistance;
    protected float scaleModifier;

    protected float shortestDistance;

    protected RaycastHit[] boxHits;
    protected Vector3 originalBoxCastPos;
    protected Vector3 newBoxCastPos;

    [Header("Bools")]
    public bool selection;
    public bool initialise;
    public bool grabbed;
    public bool dropped;

    [Header("Pick Up System")]
    public Pickup pickupSystem;

    [Header("Switch Cases")]
    public States currentState = States.SelectionState;

    private void Start()
    {

        selectedParent = new GameObject("SelectedObject Handler"); // Pickup system
        selectedParent.AddComponent<BoxCollider>();
        selectedParent.GetComponent<Collider>().isTrigger = true;
        selectedParent.layer = 2;
        selectedParent.transform.parent = Camera.main.transform; // Pickup system
        selectedParent.transform.localPosition = new Vector3(0, 0, 0); // Pickup system
        selectedParent.transform.localRotation = new Quaternion(0, 0, 0, 0); // Pickup system

        pickupSystem = FindObjectOfType<Pickup>();
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
                if (pickupSystem.grab) // Must select object to begin.
                {
                    if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "SelectableBox" || hitData.transform.tag == "SelectableBlock" || hitData.transform.tag == "SelectablePion" || hitData.transform.tag == "SelectableCB") // Check for selectables
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
                //selectedObject.transform.localScale = originalScale * scaleModifier; // Set scale
                incrementDistance = collisionHit.distance / incrementAmount;
                originalBoxCastPos = transform.position;
                for (int i = 0; i < incrementAmount; i++)
                {
                    if (i < incrementAmount)
                    {
                        newBoxCastPos = Vector3.MoveTowards(originalBoxCastPos, originalBoxCastPos + transform.forward * incrementDistance, incrementDistance); // Calculate optimal position
                        if (selectedObject.tag == "SelectableBox" || selectedObject.tag == "SelectableBlock")
                        {
                            boxHits = Physics.BoxCastAll(originalBoxCastPos, (originalScale * scaleModifier) / 2, transform.forward, selectedObject.transform.rotation, incrementDistance);
                        }
                        else if ( selectedObject.tag == "SelectablePion" || selectedObject.tag == "SelectableCB")
                        {
                            Vector3 p1 = originalBoxCastPos + selectedObject.GetComponent<CapsuleCollider>().center + Vector3.up * -selectedObject.transform.localScale.y * 0.5f;
                            Vector3 p2 = p1 + Vector3.up * selectedObject.GetComponent<CapsuleCollider>().height/4;
                            float capsuleCollider = selectedObject.GetComponent<CapsuleCollider>().radius /2;
                            boxHits = Physics.CapsuleCastAll( p1,p2, capsuleCollider, transform.forward, incrementDistance);
                        }
                        shortestDistance = Vector3.Distance(originalBoxCastPos, newBoxCastPos); ; // Max short distance
                        for (int y = 0; y < boxHits.Length; y++) // Get minimal distance in array
                        {
                            Debug.DrawLine(transform.position, boxHits[y].point);
                            if (boxHits[y].transform.tag == "Player")
                            {
                                //Debug.Log(boxHits[y].transform.name);
                                selectedObject.transform.position = Vector3.MoveTowards(originalBoxCastPos, originalBoxCastPos + transform.forward * shortestDistance, shortestDistance); // Calculate optimal position
                            }
                            else 
                            {
                                selectedObject.transform.localScale = originalScale * scaleModifier; // Set scale
                            }
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
                if (pickupSystem.grab) // Click to let go of object
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
            //Gizmos.DrawWireCube(selectedObject.transform.position, originalScale * scaleModifier);
            //Gizmos.DrawMesh(selectedObject.GetComponent<Mesh>(), transform.position + transform.forward * boxHit.distance, selectedParent.transform.rotation, selectedParent.transform.localScale);
        }
    }
}
