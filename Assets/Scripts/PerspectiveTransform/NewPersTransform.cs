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
    [SerializeField] private float newDistance;
    [SerializeField] private float originalDistance;
    [SerializeField] private float scaleModifier;

    private RaycastHit boxHit;
    private RaycastHit[] boxHits;

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
                originalPosition = selectedRigidBody.worldCenterOfMass;
                originalRotation = selectedObject.transform.rotation;
                originalScale = selectedObject.transform.localScale;
                originalDistance = Vector3.Distance(this.gameObject.transform.position, originalPosition);
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
                /// Grabbed Begin
                collisionDetected = true;
                selectedObject.transform.rotation = new Quaternion(selectedObject.transform.rotation.x, 0, selectedObject.transform.rotation.z, 1); // Reset rotation
                //boxHits = Physics.OverlapBox(selectedObject.transform.position, selectedObject.transform.localScale/2);
                boxHits = Physics.BoxCastAll(transform.position, selectedObject.transform.localScale/2, transform.forward, selectedObject.transform.rotation, collisionHit.distance); // = Quaternion.identity

                //float distance = Vector3.Distance(transform.position, selectedObject.transform.position);
                float shortestDistance = collisionHit.distance; // Max short distance
                for (int i = 0; i < boxHits.Length; i++) // Get minimal distance in array
                {
                    //distance = Vector3.Distance(transform.position, boxHits[i].transform.position);
                    if (shortestDistance > boxHits[i].distance)
                    {
                        shortestDistance = boxHits[i].distance;
                    }
                }
                //Debug.Log("Shortest Distance = " + shortestDistance);

                // Debug Lines
                foreach (var newhit in boxHits)
                {
                    Debug.DrawLine(transform.position, newhit.point);
                    //Debug.Log(newhit.transform.name);
                }
                // !Debug Lines

                newDistance = shortestDistance;
                scaleModifier = newDistance / originalDistance; // Set scale modifier
                Collider[] newBoxHits = Physics.OverlapBox(selectedObject.transform.position, originalScale * scaleModifier, originalRotation, selectableLayer);

                // Debug Lines
                foreach (var newhit in newBoxHits)
                {
                    Debug.Log(newhit.transform.name);
                }
                // !Debug Lines

                if (newBoxHits.Length == 0)
                    selectedObject.transform.localScale = originalScale * scaleModifier; // Set scale

                //selectedObject.transform.localScale = originalScale * scaleModifier; // Set scale
                Vector3 mouseLocalPoint = Vector3.MoveTowards(transform.position, transform.position + transform.forward * newDistance, newDistance); // Calculate optimal position
                selectedObject.transform.position = mouseLocalPoint; // Set position
                /// Grabbed End
                if (Input.GetMouseButtonDown(0)) // Click to let go of object
                {
                    currentState = States.DropState;
                }
                break;
            case States.DropState:
                /// Drop Begin
                collisionDetected = false;
                selectedRigidBody.GetComponent<Collider>().isTrigger = false;
                selectedObject.layer = 0;
                selectedRigidBody.isKinematic = false;
                selectedRigidBody.useGravity = true;
                selectedObject.transform.parent = transform.parent;  // Pickup system
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
            Gizmos.DrawRay(transform.position, transform.forward * boxHit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(selectedObject.transform.position, originalScale * scaleModifier);
            //Gizmos.DrawMesh(selectedObject.GetComponent<Mesh>(), transform.position + transform.forward * boxHit.distance, selectedParent.transform.rotation, selectedParent.transform.localScale);
        }
    }
}
