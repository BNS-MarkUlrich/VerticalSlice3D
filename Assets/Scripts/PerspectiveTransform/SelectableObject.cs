using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        // Call function from PerspectiveTransform that DECREASES raycast distance?
        //FindObjectOfType<PerspectiveTransform>().collisionDetected = true;
    }



    private void OnCollisionExit(Collision collision)
    {
        // Call function from PerspectiveTransform that INCREASES raycast distance?
        //FindObjectOfType<PerspectiveTransform>().collisionDetected = false;
    }
}