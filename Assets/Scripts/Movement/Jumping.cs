using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    [SerializeField] private float jumpForce;
    [Header("All objects")]
    [SerializeField] private GameObject[] _objectsName;
    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _groundClip;
    [SerializeField] private AudioClip _blockClip;
    [SerializeField] private AudioClip _pionClip;
    [SerializeField] private AudioClip _checkerboardClip;
    [SerializeField] private AudioClip _boxClip;
    [SerializeField] private AudioClip _jumpUPClip;

    private void Start()
    {
        thisRigidbody = this.gameObject.GetComponent<Rigidbody>();
        //Audio--------------------------------------------------------------------------------
        _audioSource = FindObjectOfType<AudioSource>();
        _groundClip = Resources.Load<AudioClip>("Music/Player/LandingImpactGround");
        _blockClip = Resources.Load<AudioClip>("Music/Player/LandingImpactBlocks");
        _pionClip = Resources.Load<AudioClip>("Music/Player/LandingImpactSoundPion");
        _checkerboardClip = Resources.Load<AudioClip>("Music/Player/LandingImpactCheckerBoard");
        _boxClip = Resources.Load<AudioClip>("Music/Player/LandingImpactBox");
        _jumpUPClip = Resources.Load<AudioClip>("Music/Player/JumpUP");
        //Find all Objects---------------------------------------------------------------------
        _objectsName = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (Mathf.Abs(thisRigidbody.velocity.y) < 0.001f)
        {
            _audioSource.PlayOneShot(_jumpUPClip,2);
            thisRigidbody.AddForce(new Vector3(0, jumpForce), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            for (int i = 0; i < _objectsName.Length; i++)
            {
                // Ground--------------------------------------------------
                if (collision.gameObject.name == "Floor PlaceHolder" || collision.gameObject.name == "Floor PlaceHolder (" + i + ")")
                {
                    _audioSource.PlayOneShot(_groundClip, 1);
                    Debug.Log("ground");
                    i = _objectsName.Length;
                }
                // Block---------------------------------------------------
                else if (collision.gameObject.tag == "SelectableBlock")
                {
                    Debug.Log("Block");
                    _audioSource.PlayOneShot(_blockClip, 1);
                    i = _objectsName.Length;
                }
                //Pion-----------------------------------------------------
                else if (collision.gameObject.tag == "SelectablePion")
                {
                    _audioSource.PlayOneShot(_pionClip, 1);
                    Debug.Log("Pion");
                    i = _objectsName.Length;
                }
                //Checkerboard---------------------------------------------
                else if (collision.gameObject.tag == "SelectableCB")
                {
                    Debug.Log("CB");
                    _audioSource.PlayOneShot(_checkerboardClip, 1);
                    i = _objectsName.Length;
                }
                //Box------------------------------------------------------
                else if (collision.gameObject.tag == "SelectableBox")
                {
                    Debug.Log("Box");
                    _audioSource.PlayOneShot(_boxClip, 1);
                    i = _objectsName.Length;
                }
                else if (collision.gameObject.name == "Table" ||  collision.gameObject.name == "Table (" + i + ")")
                {
                    _audioSource.PlayOneShot(_boxClip, 1);
                    i = _objectsName.Length;
                }


            }       
        }
    }
}
