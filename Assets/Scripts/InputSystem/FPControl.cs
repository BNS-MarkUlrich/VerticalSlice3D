// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputSystem/FPControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @FPControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @FPControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""FPControl"",
    ""maps"": [
        {
            ""name"": ""PlayerInput"",
            ""id"": ""544434f5-d764-4b38-8adc-cff2331f43b6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f31a6221-c748-425c-8300-b745ba8381c6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Looking/rotating"",
                    ""type"": ""Value"",
                    ""id"": ""d7232fbe-e7fc-4ec7-ae58-f6e3771e2935"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jumping"",
                    ""type"": ""Button"",
                    ""id"": ""894e759f-2adc-4ba9-9cc8-3afe55e0d42e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grab/Release/Use"",
                    ""type"": ""Button"",
                    ""id"": ""dd20ccfe-1cf1-4429-b824-c23025925739"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate Mode"",
                    ""type"": ""Button"",
                    ""id"": ""a9cf6142-e033-4ce9-a0ca-e7d59c3efdbf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset Clone"",
                    ""type"": ""Button"",
                    ""id"": ""3e917089-66d1-40e0-ae7a-dfcb8650a489"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Restart Challenge"",
                    ""type"": ""Button"",
                    ""id"": ""8bda9dde-d340-483b-ade8-b4db77a4c487"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fc026e84-3c50-4404-97cb-6f3f7166ffbf"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Looking/rotating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c8e00b6-bfaf-4c2b-9071-1c4171e55b8d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Looking/rotating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e6c4ca2-84c9-498d-94ee-74cf946dd653"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2fb43e9f-e608-4231-9da8-1a40fb43cfa1"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d28103b-7c6f-4e8b-8eb8-552e563a7ea3"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jumping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4f1f718-58eb-45eb-b90e-37121970c5ca"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab/Release/Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc308c67-5ac2-46f5-a9a3-2e82dda705df"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab/Release/Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1190e7c8-bf0b-4aec-a1e8-71077faf2dcd"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab/Release/Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e175e3e0-41c0-4c33-b4c8-cfd142fbf2ca"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab/Release/Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""790fa8c8-a57c-4c6b-90e7-19c7d686dcd7"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab/Release/Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""060ba59e-14e0-48a3-9437-631da4089c3d"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab/Release/Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1781353-2bda-4db2-8e12-73516c89c378"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab/Release/Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""e62982e1-e724-4f5e-8f9f-471afa52b9ff"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7def5eb4-9b7b-465b-8f4a-81b6e610f8ed"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""77f26f0a-b80d-4450-aca2-27b596e08d56"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""955ea794-010a-403e-941a-59c9a3039dcb"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""59248577-5d9b-49b9-8ad7-809a5585e133"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a53550b7-00fa-4b28-8d07-cf5d7dee5754"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b384e42-cfd1-4936-8b78-61d43174079a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8198e840-fdce-4037-8158-8b61f2ad9967"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""becbb626-db1f-407e-9f6f-3f156ff02308"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bb0f526-a67d-4f39-b381-fc7389f82779"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart Challenge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93c57a7a-2a6b-4c9f-81bd-faa9c2a2c0b2"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart Challenge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8dce3de9-e857-4c44-a2ce-c3303e9c742f"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart Challenge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""491d099c-4f25-42e8-9534-8a657ae4b03c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset Clone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c69deaa-a70d-4d16-8dae-183422b78632"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset Clone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a8cd881-3b7c-48c4-a7eb-4273edd6bb4b"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset Clone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInput
        m_PlayerInput = asset.FindActionMap("PlayerInput", throwIfNotFound: true);
        m_PlayerInput_Movement = m_PlayerInput.FindAction("Movement", throwIfNotFound: true);
        m_PlayerInput_Lookingrotating = m_PlayerInput.FindAction("Looking/rotating", throwIfNotFound: true);
        m_PlayerInput_Jumping = m_PlayerInput.FindAction("Jumping", throwIfNotFound: true);
        m_PlayerInput_GrabReleaseUse = m_PlayerInput.FindAction("Grab/Release/Use", throwIfNotFound: true);
        m_PlayerInput_RotateMode = m_PlayerInput.FindAction("Rotate Mode", throwIfNotFound: true);
        m_PlayerInput_ResetClone = m_PlayerInput.FindAction("Reset Clone", throwIfNotFound: true);
        m_PlayerInput_RestartChallenge = m_PlayerInput.FindAction("Restart Challenge", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerInput
    private readonly InputActionMap m_PlayerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_PlayerInput_Movement;
    private readonly InputAction m_PlayerInput_Lookingrotating;
    private readonly InputAction m_PlayerInput_Jumping;
    private readonly InputAction m_PlayerInput_GrabReleaseUse;
    private readonly InputAction m_PlayerInput_RotateMode;
    private readonly InputAction m_PlayerInput_ResetClone;
    private readonly InputAction m_PlayerInput_RestartChallenge;
    public struct PlayerInputActions
    {
        private @FPControl m_Wrapper;
        public PlayerInputActions(@FPControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerInput_Movement;
        public InputAction @Lookingrotating => m_Wrapper.m_PlayerInput_Lookingrotating;
        public InputAction @Jumping => m_Wrapper.m_PlayerInput_Jumping;
        public InputAction @GrabReleaseUse => m_Wrapper.m_PlayerInput_GrabReleaseUse;
        public InputAction @RotateMode => m_Wrapper.m_PlayerInput_RotateMode;
        public InputAction @ResetClone => m_Wrapper.m_PlayerInput_ResetClone;
        public InputAction @RestartChallenge => m_Wrapper.m_PlayerInput_RestartChallenge;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnMovement;
                @Lookingrotating.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLookingrotating;
                @Lookingrotating.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLookingrotating;
                @Lookingrotating.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnLookingrotating;
                @Jumping.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJumping;
                @Jumping.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJumping;
                @Jumping.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnJumping;
                @GrabReleaseUse.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnGrabReleaseUse;
                @GrabReleaseUse.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnGrabReleaseUse;
                @GrabReleaseUse.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnGrabReleaseUse;
                @RotateMode.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRotateMode;
                @RotateMode.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRotateMode;
                @RotateMode.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRotateMode;
                @ResetClone.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnResetClone;
                @ResetClone.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnResetClone;
                @ResetClone.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnResetClone;
                @RestartChallenge.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRestartChallenge;
                @RestartChallenge.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRestartChallenge;
                @RestartChallenge.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnRestartChallenge;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Lookingrotating.started += instance.OnLookingrotating;
                @Lookingrotating.performed += instance.OnLookingrotating;
                @Lookingrotating.canceled += instance.OnLookingrotating;
                @Jumping.started += instance.OnJumping;
                @Jumping.performed += instance.OnJumping;
                @Jumping.canceled += instance.OnJumping;
                @GrabReleaseUse.started += instance.OnGrabReleaseUse;
                @GrabReleaseUse.performed += instance.OnGrabReleaseUse;
                @GrabReleaseUse.canceled += instance.OnGrabReleaseUse;
                @RotateMode.started += instance.OnRotateMode;
                @RotateMode.performed += instance.OnRotateMode;
                @RotateMode.canceled += instance.OnRotateMode;
                @ResetClone.started += instance.OnResetClone;
                @ResetClone.performed += instance.OnResetClone;
                @ResetClone.canceled += instance.OnResetClone;
                @RestartChallenge.started += instance.OnRestartChallenge;
                @RestartChallenge.performed += instance.OnRestartChallenge;
                @RestartChallenge.canceled += instance.OnRestartChallenge;
            }
        }
    }
    public PlayerInputActions @PlayerInput => new PlayerInputActions(this);
    public interface IPlayerInputActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLookingrotating(InputAction.CallbackContext context);
        void OnJumping(InputAction.CallbackContext context);
        void OnGrabReleaseUse(InputAction.CallbackContext context);
        void OnRotateMode(InputAction.CallbackContext context);
        void OnResetClone(InputAction.CallbackContext context);
        void OnRestartChallenge(InputAction.CallbackContext context);
    }
}
