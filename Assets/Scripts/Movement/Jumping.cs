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
    [SerializeField] private AudioClip _pionClip;
    [SerializeField] private AudioClip _checkerboardClip;
    [SerializeField] private AudioClip _boxClip;

    private void Start()
    {
        thisRigidbody = this.gameObject.GetComponent<Rigidbody>();
        _audioSource = FindObjectOfType<AudioSource>();
        _groundClip = Resources.Load<AudioClip>("Music/Player/LandingImpactGround");
        _blockClip = Resources.Load<AudioClip>("Music/Player/LandingImpactBlocks");
        _pionClip = Resources.Load<AudioClip>("Music/Player/LandingImpactSoundPion");
        _checkerboardClip = Resources.Load<AudioClip>("Music/Player/LandingImpactCheckerBoard");
        _boxClip = Resources.Load<AudioClip>("Music/Player/LandingImpactBox");
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
            else if(collision.gameObject.name == "Pion")
            {
                _audioSource.PlayOneShot(_pionClip, 1);
            }
            else if(collision.gameObject.name == "Checkerboard")
            {
                _audioSource.PlayOneShot(_checkerboardClip, 1);
            }
            else if(collision.gameObject.name == "Box")
            {
                _audioSource.PlayOneShot(_boxClip, 1);
            }

           
        }
    }
}
