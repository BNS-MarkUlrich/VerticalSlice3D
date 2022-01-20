using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    [SerializeField] private float jumpForce;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _groundClip;
    [SerializeField] private AudioClip _blockClip;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            if (collision.gameObject.name == "Ground")
            {
                _audioSource.PlayOneShot(_groundClip, 1);
            }
            else if(collision.gameObject.name == "Cube")
            {
                _audioSource.PlayOneShot(_blockClip, 1);
            }

           
        }
    }
}
