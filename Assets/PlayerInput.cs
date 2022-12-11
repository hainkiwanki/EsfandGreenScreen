// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""6436cef7-c761-4147-8a08-b76f55b3aa85"",
            ""actions"": [
                {
                    ""name"": ""MoveVer"",
                    ""type"": ""Value"",
                    ""id"": ""41085112-0e75-4478-a625-eae6ce59ee5c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveHor"",
                    ""type"": ""Value"",
                    ""id"": ""1c8f5bf5-5be0-4468-b8af-1ae950c7ce6e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""e877e650-49fa-403f-bfb6-73e1d79cfe65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""f8827ec3-845d-4b66-99c3-0193a517df66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Any"",
                    ""type"": ""PassThrough"",
                    ""id"": ""44415ad4-320a-49ba-8e6f-9af6a37a85a2"",
                    ""expectedControlType"": ""Key"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""0f760b0c-0ed3-4823-99c8-c63d4fafef6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Spacebar"",
                    ""type"": ""Button"",
                    ""id"": ""4477370f-615f-4eb6-8446-10290e7ced17"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""cd81368b-434f-434d-b66d-5cde92521efe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SkipCutscene"",
                    ""type"": ""Button"",
                    ""id"": ""7c3a9199-03bd-43e8-bd94-d762a00c7d46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""3bab3de5-c879-4cfc-bb32-828a0000f8f8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftHold"",
                    ""type"": ""Button"",
                    ""id"": ""e9e87110-5938-40a8-a4ab-04c62fe64e6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CheatKey"",
                    ""type"": ""Button"",
                    ""id"": ""2366bc6a-9389-479e-9750-08b24e5be7cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""LeftRight"",
                    ""id"": ""5b51b821-762a-41ad-8791-ab55820b5f1c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4321cb4d-9c24-47cc-b1d7-59f09831832f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7e86b09f-f6ee-4fc6-936e-495caf4042c2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a5608bda-3e60-4819-8449-06449995035b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f5b140d-491f-41e8-ab95-490cb62aab87"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b924e27b-d3e3-47c3-930c-ad8343ff05d1"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79987ea2-5958-473d-aac8-51b2472f9660"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""BackFront"",
                    ""id"": ""e0417865-cf3d-415d-a18a-e339a3410794"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1aa548e1-d4b1-43a0-afbf-5e74d0fe2255"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1c7045bc-18c4-4aa8-9c2f-61aeaaaded4d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cb574e31-5392-4e85-a247-ff9e73cd8e12"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Spacebar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95053dc4-d6de-47da-9a45-3ce7834c0279"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""54012b64-ecfa-4b47-bb49-90c8815a8784"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkipCutscene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28a305f4-4525-43d9-9b79-9d03b8ba38ff"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50770495-954d-4433-866c-b007d7e1255d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbe7c0b9-1ddc-43f6-afba-d924f42f3b48"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CheatKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MoveVer = m_Player.FindAction("MoveVer", throwIfNotFound: true);
        m_Player_MoveHor = m_Player.FindAction("MoveHor", throwIfNotFound: true);
        m_Player_LeftClick = m_Player.FindAction("LeftClick", throwIfNotFound: true);
        m_Player_RightClick = m_Player.FindAction("RightClick", throwIfNotFound: true);
        m_Player_Any = m_Player.FindAction("Any", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Spacebar = m_Player.FindAction("Spacebar", throwIfNotFound: true);
        m_Player_Esc = m_Player.FindAction("Esc", throwIfNotFound: true);
        m_Player_SkipCutscene = m_Player.FindAction("SkipCutscene", throwIfNotFound: true);
        m_Player_MousePos = m_Player.FindAction("MousePos", throwIfNotFound: true);
        m_Player_LeftHold = m_Player.FindAction("LeftHold", throwIfNotFound: true);
        m_Player_CheatKey = m_Player.FindAction("CheatKey", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MoveVer;
    private readonly InputAction m_Player_MoveHor;
    private readonly InputAction m_Player_LeftClick;
    private readonly InputAction m_Player_RightClick;
    private readonly InputAction m_Player_Any;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Spacebar;
    private readonly InputAction m_Player_Esc;
    private readonly InputAction m_Player_SkipCutscene;
    private readonly InputAction m_Player_MousePos;
    private readonly InputAction m_Player_LeftHold;
    private readonly InputAction m_Player_CheatKey;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveVer => m_Wrapper.m_Player_MoveVer;
        public InputAction @MoveHor => m_Wrapper.m_Player_MoveHor;
        public InputAction @LeftClick => m_Wrapper.m_Player_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_Player_RightClick;
        public InputAction @Any => m_Wrapper.m_Player_Any;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Spacebar => m_Wrapper.m_Player_Spacebar;
        public InputAction @Esc => m_Wrapper.m_Player_Esc;
        public InputAction @SkipCutscene => m_Wrapper.m_Player_SkipCutscene;
        public InputAction @MousePos => m_Wrapper.m_Player_MousePos;
        public InputAction @LeftHold => m_Wrapper.m_Player_LeftHold;
        public InputAction @CheatKey => m_Wrapper.m_Player_CheatKey;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MoveVer.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVer;
                @MoveVer.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVer;
                @MoveVer.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVer;
                @MoveHor.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHor;
                @MoveHor.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHor;
                @MoveHor.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHor;
                @LeftClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftClick;
                @RightClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRightClick;
                @Any.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAny;
                @Any.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAny;
                @Any.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAny;
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Spacebar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpacebar;
                @Spacebar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpacebar;
                @Spacebar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSpacebar;
                @Esc.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEsc;
                @SkipCutscene.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkipCutscene;
                @SkipCutscene.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkipCutscene;
                @SkipCutscene.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkipCutscene;
                @MousePos.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @LeftHold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftHold;
                @LeftHold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftHold;
                @LeftHold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeftHold;
                @CheatKey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCheatKey;
                @CheatKey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCheatKey;
                @CheatKey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCheatKey;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveVer.started += instance.OnMoveVer;
                @MoveVer.performed += instance.OnMoveVer;
                @MoveVer.canceled += instance.OnMoveVer;
                @MoveHor.started += instance.OnMoveHor;
                @MoveHor.performed += instance.OnMoveHor;
                @MoveHor.canceled += instance.OnMoveHor;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @Any.started += instance.OnAny;
                @Any.performed += instance.OnAny;
                @Any.canceled += instance.OnAny;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Spacebar.started += instance.OnSpacebar;
                @Spacebar.performed += instance.OnSpacebar;
                @Spacebar.canceled += instance.OnSpacebar;
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
                @SkipCutscene.started += instance.OnSkipCutscene;
                @SkipCutscene.performed += instance.OnSkipCutscene;
                @SkipCutscene.canceled += instance.OnSkipCutscene;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @LeftHold.started += instance.OnLeftHold;
                @LeftHold.performed += instance.OnLeftHold;
                @LeftHold.canceled += instance.OnLeftHold;
                @CheatKey.started += instance.OnCheatKey;
                @CheatKey.performed += instance.OnCheatKey;
                @CheatKey.canceled += instance.OnCheatKey;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMoveVer(InputAction.CallbackContext context);
        void OnMoveHor(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnAny(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSpacebar(InputAction.CallbackContext context);
        void OnEsc(InputAction.CallbackContext context);
        void OnSkipCutscene(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnLeftHold(InputAction.CallbackContext context);
        void OnCheatKey(InputAction.CallbackContext context);
    }
}
