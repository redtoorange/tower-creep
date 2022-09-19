//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Input/GameInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""9dcbf238-ac42-4e2b-8caa-4cec1bcb1519"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""729ff90b-d372-4753-b768-0b679a144711"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""StopBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""3054e4c8-2bdb-44da-ab06-e48b44e6337c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PlaceBuilding"",
                    ""type"": ""Button"",
                    ""id"": ""a3d3769f-9af0-43c1-900c-9d5f497a57c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""796719a4-243c-4410-b3e7-772569247922"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""58e9750d-8038-4e06-a2dc-4d7e4409e01c"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""26e5533a-2cac-4340-89bb-4f38f4b6a980"",
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
                    ""id"": ""37248835-b78c-4e1e-94a8-971581889797"",
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
                    ""id"": ""e99efc61-fe90-44ff-b97b-b567548f2588"",
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
                    ""id"": ""eab6fbb9-4341-44b3-b021-974fd2de5394"",
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
                    ""id"": ""3def58f1-c007-4e54-b2b8-08e662c39aa0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StopBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85a01824-a384-4286-a4f1-c612f52dc6d3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceBuilding"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5eb2e6a-37a5-4ec8-9072-8db78bcbe32c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BuildBarKeys"",
            ""id"": ""25bd0841-7bcf-4afa-ac3f-9493533b4ad8"",
            ""actions"": [
                {
                    ""name"": ""Slot_1"",
                    ""type"": ""Button"",
                    ""id"": ""b665ec19-11db-4dd3-9fce-6ba010744103"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_2"",
                    ""type"": ""Button"",
                    ""id"": ""f4dfc3fa-1fd2-45b6-a04d-399532080dc9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_3"",
                    ""type"": ""Button"",
                    ""id"": ""f0583459-538c-421d-be23-5a35c337a33f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_4"",
                    ""type"": ""Button"",
                    ""id"": ""03a22577-0399-4123-9387-4605e71d03f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_5"",
                    ""type"": ""Button"",
                    ""id"": ""570b9083-fc56-4703-ad8f-2a920a5c6c71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_6"",
                    ""type"": ""Button"",
                    ""id"": ""72906023-fe74-4d4f-880f-c510ec8ac214"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_7"",
                    ""type"": ""Button"",
                    ""id"": ""28e15b8d-c7cd-4dac-8bb2-4409029d48eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_8"",
                    ""type"": ""Button"",
                    ""id"": ""10893814-2024-43df-a575-df236e69d001"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Slot_9"",
                    ""type"": ""Button"",
                    ""id"": ""bd3092f6-06b5-4120-b2ad-9b32983088fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c8477fbc-7395-4ad3-91fd-59c140c35ed0"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbf4ada9-bc1d-4348-9cc9-db56a541a94e"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""061645e0-6823-4c3b-99ea-94e17a4606f0"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3acd1a92-9882-4a94-982c-2ae32ec6b340"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bb951cf-d568-4ed6-9c77-b83379389a8d"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8e61cf7-59d5-4d35-8e07-5edf663f4119"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec172dc7-a6bd-4378-98e0-25f1bba48d0b"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bbfcc1f0-de1a-4200-a3bc-31648e4d4bd6"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35468dd6-92af-4e20-b061-a1cb1b18807f"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Slot_9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Movement = m_PlayerActions.FindAction("Movement", throwIfNotFound: true);
        m_PlayerActions_StopBuilding = m_PlayerActions.FindAction("StopBuilding", throwIfNotFound: true);
        m_PlayerActions_PlaceBuilding = m_PlayerActions.FindAction("PlaceBuilding", throwIfNotFound: true);
        m_PlayerActions_PauseGame = m_PlayerActions.FindAction("PauseGame", throwIfNotFound: true);
        // BuildBarKeys
        m_BuildBarKeys = asset.FindActionMap("BuildBarKeys", throwIfNotFound: true);
        m_BuildBarKeys_Slot_1 = m_BuildBarKeys.FindAction("Slot_1", throwIfNotFound: true);
        m_BuildBarKeys_Slot_2 = m_BuildBarKeys.FindAction("Slot_2", throwIfNotFound: true);
        m_BuildBarKeys_Slot_3 = m_BuildBarKeys.FindAction("Slot_3", throwIfNotFound: true);
        m_BuildBarKeys_Slot_4 = m_BuildBarKeys.FindAction("Slot_4", throwIfNotFound: true);
        m_BuildBarKeys_Slot_5 = m_BuildBarKeys.FindAction("Slot_5", throwIfNotFound: true);
        m_BuildBarKeys_Slot_6 = m_BuildBarKeys.FindAction("Slot_6", throwIfNotFound: true);
        m_BuildBarKeys_Slot_7 = m_BuildBarKeys.FindAction("Slot_7", throwIfNotFound: true);
        m_BuildBarKeys_Slot_8 = m_BuildBarKeys.FindAction("Slot_8", throwIfNotFound: true);
        m_BuildBarKeys_Slot_9 = m_BuildBarKeys.FindAction("Slot_9", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Movement;
    private readonly InputAction m_PlayerActions_StopBuilding;
    private readonly InputAction m_PlayerActions_PlaceBuilding;
    private readonly InputAction m_PlayerActions_PauseGame;
    public struct PlayerActionsActions
    {
        private @GameInputActions m_Wrapper;
        public PlayerActionsActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerActions_Movement;
        public InputAction @StopBuilding => m_Wrapper.m_PlayerActions_StopBuilding;
        public InputAction @PlaceBuilding => m_Wrapper.m_PlayerActions_PlaceBuilding;
        public InputAction @PauseGame => m_Wrapper.m_PlayerActions_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMovement;
                @StopBuilding.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnStopBuilding;
                @StopBuilding.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnStopBuilding;
                @StopBuilding.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnStopBuilding;
                @PlaceBuilding.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPlaceBuilding;
                @PlaceBuilding.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPlaceBuilding;
                @PlaceBuilding.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPlaceBuilding;
                @PauseGame.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnPauseGame;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @StopBuilding.started += instance.OnStopBuilding;
                @StopBuilding.performed += instance.OnStopBuilding;
                @StopBuilding.canceled += instance.OnStopBuilding;
                @PlaceBuilding.started += instance.OnPlaceBuilding;
                @PlaceBuilding.performed += instance.OnPlaceBuilding;
                @PlaceBuilding.canceled += instance.OnPlaceBuilding;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // BuildBarKeys
    private readonly InputActionMap m_BuildBarKeys;
    private IBuildBarKeysActions m_BuildBarKeysActionsCallbackInterface;
    private readonly InputAction m_BuildBarKeys_Slot_1;
    private readonly InputAction m_BuildBarKeys_Slot_2;
    private readonly InputAction m_BuildBarKeys_Slot_3;
    private readonly InputAction m_BuildBarKeys_Slot_4;
    private readonly InputAction m_BuildBarKeys_Slot_5;
    private readonly InputAction m_BuildBarKeys_Slot_6;
    private readonly InputAction m_BuildBarKeys_Slot_7;
    private readonly InputAction m_BuildBarKeys_Slot_8;
    private readonly InputAction m_BuildBarKeys_Slot_9;
    public struct BuildBarKeysActions
    {
        private @GameInputActions m_Wrapper;
        public BuildBarKeysActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Slot_1 => m_Wrapper.m_BuildBarKeys_Slot_1;
        public InputAction @Slot_2 => m_Wrapper.m_BuildBarKeys_Slot_2;
        public InputAction @Slot_3 => m_Wrapper.m_BuildBarKeys_Slot_3;
        public InputAction @Slot_4 => m_Wrapper.m_BuildBarKeys_Slot_4;
        public InputAction @Slot_5 => m_Wrapper.m_BuildBarKeys_Slot_5;
        public InputAction @Slot_6 => m_Wrapper.m_BuildBarKeys_Slot_6;
        public InputAction @Slot_7 => m_Wrapper.m_BuildBarKeys_Slot_7;
        public InputAction @Slot_8 => m_Wrapper.m_BuildBarKeys_Slot_8;
        public InputAction @Slot_9 => m_Wrapper.m_BuildBarKeys_Slot_9;
        public InputActionMap Get() { return m_Wrapper.m_BuildBarKeys; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildBarKeysActions set) { return set.Get(); }
        public void SetCallbacks(IBuildBarKeysActions instance)
        {
            if (m_Wrapper.m_BuildBarKeysActionsCallbackInterface != null)
            {
                @Slot_1.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_1;
                @Slot_1.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_1;
                @Slot_1.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_1;
                @Slot_2.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_2;
                @Slot_2.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_2;
                @Slot_2.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_2;
                @Slot_3.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_3;
                @Slot_3.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_3;
                @Slot_3.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_3;
                @Slot_4.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_4;
                @Slot_4.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_4;
                @Slot_4.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_4;
                @Slot_5.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_5;
                @Slot_5.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_5;
                @Slot_5.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_5;
                @Slot_6.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_6;
                @Slot_6.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_6;
                @Slot_6.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_6;
                @Slot_7.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_7;
                @Slot_7.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_7;
                @Slot_7.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_7;
                @Slot_8.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_8;
                @Slot_8.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_8;
                @Slot_8.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_8;
                @Slot_9.started -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_9;
                @Slot_9.performed -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_9;
                @Slot_9.canceled -= m_Wrapper.m_BuildBarKeysActionsCallbackInterface.OnSlot_9;
            }
            m_Wrapper.m_BuildBarKeysActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Slot_1.started += instance.OnSlot_1;
                @Slot_1.performed += instance.OnSlot_1;
                @Slot_1.canceled += instance.OnSlot_1;
                @Slot_2.started += instance.OnSlot_2;
                @Slot_2.performed += instance.OnSlot_2;
                @Slot_2.canceled += instance.OnSlot_2;
                @Slot_3.started += instance.OnSlot_3;
                @Slot_3.performed += instance.OnSlot_3;
                @Slot_3.canceled += instance.OnSlot_3;
                @Slot_4.started += instance.OnSlot_4;
                @Slot_4.performed += instance.OnSlot_4;
                @Slot_4.canceled += instance.OnSlot_4;
                @Slot_5.started += instance.OnSlot_5;
                @Slot_5.performed += instance.OnSlot_5;
                @Slot_5.canceled += instance.OnSlot_5;
                @Slot_6.started += instance.OnSlot_6;
                @Slot_6.performed += instance.OnSlot_6;
                @Slot_6.canceled += instance.OnSlot_6;
                @Slot_7.started += instance.OnSlot_7;
                @Slot_7.performed += instance.OnSlot_7;
                @Slot_7.canceled += instance.OnSlot_7;
                @Slot_8.started += instance.OnSlot_8;
                @Slot_8.performed += instance.OnSlot_8;
                @Slot_8.canceled += instance.OnSlot_8;
                @Slot_9.started += instance.OnSlot_9;
                @Slot_9.performed += instance.OnSlot_9;
                @Slot_9.canceled += instance.OnSlot_9;
            }
        }
    }
    public BuildBarKeysActions @BuildBarKeys => new BuildBarKeysActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayerActionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnStopBuilding(InputAction.CallbackContext context);
        void OnPlaceBuilding(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
    public interface IBuildBarKeysActions
    {
        void OnSlot_1(InputAction.CallbackContext context);
        void OnSlot_2(InputAction.CallbackContext context);
        void OnSlot_3(InputAction.CallbackContext context);
        void OnSlot_4(InputAction.CallbackContext context);
        void OnSlot_5(InputAction.CallbackContext context);
        void OnSlot_6(InputAction.CallbackContext context);
        void OnSlot_7(InputAction.CallbackContext context);
        void OnSlot_8(InputAction.CallbackContext context);
        void OnSlot_9(InputAction.CallbackContext context);
    }
}
