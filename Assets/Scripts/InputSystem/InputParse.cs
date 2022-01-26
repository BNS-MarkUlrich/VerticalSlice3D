using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputParse : MonoBehaviour
{
    private FPControl _controls;
    private FPControl.PlayerInputActions _inputControls;
    [SerializeField] private Walking _sWalking;
    [SerializeField] private CameraMoving _sLooking;
    [SerializeField] private Jumping _sJumping;
    [SerializeField] private Pickup _sRotateMode;
    [SerializeField] private Pickup _sGrab;
    public bool _isLooking;
    public bool _isRotating;
    public bool _canGrab;
    //[SerializeField] private ResetClone _sResetClone;
    //[SerializeField] private RestartChallenge _sRestartChallenge;
    private Pickup Pickup;
    // Start is called before the first frame update
    void Start()
    {
        
        _isRotating = false;
        _isLooking = true;
        _canGrab = false;
        _controls = new FPControl();
        _inputControls = _controls.PlayerInput;
        _sWalking = this.gameObject.GetComponent<Walking>();
        _sJumping = this.gameObject.GetComponent<Jumping>();
        _sLooking = this.gameObject.GetComponentInChildren<CameraMoving>();
        _sRotateMode = this.gameObject.GetComponent<Pickup>();
        _sGrab = this.gameObject.GetComponent<Pickup>();
        _inputControls.Jumping.performed += _sJumping.Jump;
        _inputControls.GrabReleaseUse.performed += _sGrab.Grab;
        _inputControls.RotateMode.performed += _sRotateMode.HoldRotate;
        //_inputControls.//.performed += _sResetClonde.//;
        //_inputControls.//.performed += _sRestartChallenge.//;

        _inputControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _sWalking.Walk(_inputControls.Movement.ReadValue<Vector2>());
        if (_isLooking)
        {
            _sLooking.Looking(_inputControls.Lookingrotating.ReadValue<Vector2>());
        }
        if (_isRotating)
        {
            _sRotateMode.Rotating(_inputControls.Lookingrotating.ReadValue<Vector2>());
        }

        _inputControls.RotateMode.canceled += _ => _isRotating = false;
        _inputControls.RotateMode.canceled += _ => _isLooking = true;

    }
}
