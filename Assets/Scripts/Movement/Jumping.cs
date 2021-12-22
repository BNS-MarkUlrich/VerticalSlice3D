using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    [SerializeField] private float jumpForce;

    private void Start()
    {
        thisRigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(thisRigidbody.velocity.y) < 0.001f)
        {
            thisRigidbody.AddForce(new Vector3(0, jumpForce), ForceMode.Impulse);
        }
    }
}
