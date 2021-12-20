using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputParse : MonoBehaviour
{
    private FPControl _controls;
    private FPControl.PlayerInputActions _inputControls;
    [SerializeField] private Walking _sWalking;
    //[SerializeField] private Looking _sLooking;
    [SerializeField] private Jumping _sJumping;
    //[SerializeField] private Grab _sGrab;
    //[SerializeField] private RotateMode _sRotateMode;
    //[SerializeField] private ResetClone _sResetClone;
    //[SerializeField] private RestartChallenge _sRestartChallenge;
    
    // Start is called before the first frame update
    void Start()
    {
        _controls = new FPControl();
        _inputControls = _controls.PlayerInput;
        _sWalking = this.gameObject.GetComponent<Walking>();
        _sJumping = this.gameObject.GetComponent<Jumping>();

        _inputControls.Jumping.performed += _sJumping.Jump;
        //_inputControls.//.performed += _sGrab.//;
        //_inputControls.//.performed += _sRotateMode.//;
        //_inputControls.//.performed += _sResetClonde.//;
        //_inputControls.//.performed += _sRestartChallenge.//;

        _inputControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        _sWalking.Walk(_inputControls.Movement.ReadValue<Vector2>());
        //_sLooking.//(_inputControls.//.ReadValue<Vector2>());
    }
}
