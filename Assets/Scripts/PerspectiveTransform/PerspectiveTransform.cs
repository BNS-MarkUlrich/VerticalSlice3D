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
    //public bool sphereCollision;

    [Header("Mouse Position")]
    [SerializeField] private Vector3 mouseWorldPoint;
    [SerializeField] private RaycastHit collisionHit;
    [SerializeField] private RaycastHit hitData;

    [Header("Scale & Distance")]
    [SerializeField] private float newDistance;
    [SerializeField] private float originalDistance;
    [SerializeField] private float scaleModifier;

    [SerializeField] private RaycastHit boxHit;
    [SerializeField] private RaycastHit[] boxHits;

    [SerializeField] private RaycastHit sweepTestData;

    private Vector3 maxScale = new Vector3(5,5,5);
    private Vector3 lastScale = new Vector3(4.9f, 4.9f, 4.9f);

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
        Debug.Log(selectedObject.transform.localScale.x);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (selectedObject.transform.localScale.x >= 5f)
        {
            Debug.Log("resice");
        }
        /// Call Selection/Pickup System here
        if (Physics.Raycast(ray, out collisionHit, 1000))
        {
            Vector3 mouseLocalPoint = Vector3.MoveTowards(transform.position, transform.position + transform.forward * collisionHit.distance, collisionHit.distance);
            //mouseWorldPoint = new Vector3(collisionHit.point.x, collisionHit.point.y, collisionHit.point.z);
            Debug.DrawLine(this.gameObject.transform.position, collisionHit.point, Color.blue);
        }


        switch (currentState)
        {
            case States.SelectionState:
                scaleModifier = 1;
                if (Input.GetMouseButtonDown(0)) // Must select object to begin. Pickup system
                {
                    if (Physics.Raycast(ray, out hitData, 1000) && hitData.transform.tag == "Selectable")
                    {
                        selectedObject = hitData.transform.gameObject; // Pickup system
                        selectedRigidBody = selectedObject.GetComponent<Rigidbody>();
                        selectedObject.layer = 2;
                        currentState = States.InitialiseState;
                    }
                }
                break;
            case States.InitialiseState:
                /// Initialise Begin
                originalPosition = selectedRigidBody.worldCenterOfMass;
                originalRotation = selectedObject.transform.rotation;
                originalScale = selectedObject.transform.localScale;
                originalDistance = Vector3.Distance(this.gameObject.transform.position, originalPosition);

                selectedRigidBody.GetComponent<Collider>().isTrigger = true;
                selectedRigidBody.isKinematic = true;
                selectedRigidBody.useGravity = false;
                //selectedRigidBody.SetDensity(10);
                //= selectedRigidBody.worldCenterOfMass;
                //selectedRigidBody.sleepThreshold = ;
                //selectedRigidBody.solverIterations = ;
                selectedObject.transform.parent = selectedParent.transform;  // Pickup system
                /// Initialise End
                currentState = States.GrabbedState;
                break;
            case States.GrabbedState:
                /// Grabbed Begin

                //selectedObject.transform.rotation = new Quaternion(0,0,0,1); // Pickup system remove/change
                mouseWorldPoint = hitData.transform.position;
                Physics.BoxCast(transform.position, selectedObject.transform.localScale/2, transform.forward, out boxHit, selectedObject.transform.rotation, collisionHit.distance); // = Quaternion.identity
                //collisionDetected = selectedRigidBody.SweepTest(ray.direction, out sweepTestData, collisionHit.distance); // = Quaternion.identity
                //Debug.DrawLine(this.gameObject.transform.position, transform.position + transform.forward * boxHit.distance);
                if (Vector3.Distance(selectedRigidBody.worldCenterOfMass, transform.position + transform.forward * boxHit.distance) > 6)
                {
                    float ErrorDistance = Vector3.Distance(selectedRigidBody.worldCenterOfMass, transform.position + transform.forward * boxHit.distance) / 2;

                    newDistance = Vector3.Distance(this.gameObject.transform.position, transform.position + transform.forward * boxHit.distance);
                    scaleModifier = newDistance / originalDistance;
                    //selectedParent.transform.localScale = originalScale * scaleModifier;
                    //selectedObject.transform.localScale = originalScale * scaleModifier;

                    Vector3 medianPoint = (selectedRigidBody.worldCenterOfMass + transform.position + transform.forward * boxHit.distance) / 2f;
                    Vector3 objectErrorDestination = Vector3.MoveTowards(selectedRigidBody.worldCenterOfMass, medianPoint, ErrorDistance);
                    Vector3 boxHitDestination = Vector3.MoveTowards(transform.position + transform.forward * boxHit.distance, medianPoint, collisionHit.distance);
                    //Vector3.Lerp(selectedObject.transform.position, boxHit.point, 2);
                    Vector3.Lerp(selectedObject.transform.position, objectErrorDestination, 2);
                    //Vector3.Lerp(boxHit.point, selectedRigidBody.worldCenterOfMass, 2);
                    //selectedObject.transform.position = objectErrorDestination;
                    if (selectedObject.transform.position.y < 0)
                    {
                        Debug.Log("Too low");
                    }

                    Debug.DrawLine(selectedRigidBody.worldCenterOfMass, transform.position + transform.forward * boxHit.distance);
                    Debug.Log("Bye");
                }
                else if (Vector3.Distance(Camera.main.transform.position, selectedObject.transform.position) <= 0.5)
                {
                    newDistance = collisionHit.distance;
                    scaleModifier = newDistance / originalDistance;
                    //selectedParent.transform.localScale = originalScale * scaleModifier;
                    //selectedObject.transform.localScale = originalScale * scaleModifier;
                    Vector3 boxHitDestination = Vector3.MoveTowards(transform.position, transform.position + transform.forward * boxHit.distance, collisionHit.distance);
                    Vector3.Lerp(selectedObject.transform.position, boxHitDestination, 2f);
                    //selectedObject.transform.position = collisionHit.point;
                    Debug.Log("Smoll");
                }
                else
                {
                    
                    newDistance = Vector3.Distance(this.gameObject.transform.position, transform.position + transform.forward * boxHit.distance);
                    scaleModifier = newDistance / originalDistance;
                    //selectedParent.transform.localScale = originalScale * scaleModifier;
                    //selectedObject.transform.localScale = originalScale * scaleModifier;
                    Vector3 mouseLocalPoint = Vector3.MoveTowards(transform.position, transform.position + transform.forward * boxHit.distance, boxHit.distance);
                    //selectedRigidBody.AddForce(selectedRigidBody.worldCenterOfMass * 250);
                    selectedObject.transform.position = mouseLocalPoint;
                }
                /*newDistance = Vector3.Distance(this.gameObject.transform.position, boxHit.point);
                scaleModifier = newDistance / originalDistance;
                //selectedParent.transform.localScale = originalScale * scaleModifier;
                selectedObject.transform.localScale = originalScale * scaleModifier;
                Vector3 mouseLocalPoint = Vector3.MoveTowards(transform.position, transform.position + transform.forward * boxHit.distance, boxHit.distance);
                //selectedRigidBody.AddForce(selectedRigidBody.worldCenterOfMass * 250);
                selectedObject.transform.position = mouseLocalPoint;*/
                Debug.Log(boxHit.point);
                /// Grabbed End
                if (Input.GetMouseButtonDown(0)) // Pickup system
                {
                    currentState = States.DropState;
                }
                break;
            case States.DropState:
                /// Initialise Begin
                collisionDetected = false;
                selectedRigidBody.GetComponent<Collider>().isTrigger = false;
                selectedObject.layer = 0;
                selectedObject.transform.parent = transform.parent;  // Pickup system
                selectedObject.transform.position = mouseWorldPoint;
                //selectedObject.transform.rotation = new Quaternion(0, 0, 0, 1); // Pickup system remove/change
                selectedRigidBody.isKinematic = false;
                selectedRigidBody.useGravity = true;
                selectedObject.transform.parent = transform.parent;  // Pickup system
                /// Initialise End
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
            Gizmos.DrawWireCube(transform.position + transform.forward * boxHit.distance, selectedObject.transform.localScale);
            //Gizmos.DrawMesh(selectedObject.GetComponent<Mesh>(), transform.position + transform.forward * boxHit.distance, selectedParent.transform.rotation, selectedParent.transform.localScale);
        }


        /// BoxHits
        /*float totalX = 0f;
        float totalY = 0f;
        float totalZ = 0f;
        foreach (var hit in boxHits)
        {
            totalX += hit.point.x;
            totalY += hit.point.y;
            totalZ += hit.point.z;
            //Gizmos.DrawLine(transform.position, hit.point);
        }
        float centerX = totalX / boxHits.Length;
        float centerY = totalY / boxHits.Length;
        float centerZ = totalZ / boxHits.Length;
        Vector3 centerPoint = new Vector3(centerX, centerY, centerZ);
        Gizmos.DrawLine(transform.position, centerPoint);
        Gizmos.DrawWireCube(centerPoint, selectedParent.transform.localScale);*/
        /// !BoxHits
    }
}