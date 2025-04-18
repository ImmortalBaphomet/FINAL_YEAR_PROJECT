//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Fragments_Of_Lights/Scripts/PlayerLoco.inputactions
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

public partial class @PlayerLoco: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerLoco()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerLoco"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""00558c87-1047-4890-86e5-f786efcd1bc4"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4d9fe05c-282c-40ad-b70a-1011acc2d084"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""7abe912c-0585-46ed-896b-a531e10069d4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""8db66bf6-5571-445e-98e2-2f34aaa7f829"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ColorChangeRed"",
                    ""type"": ""Button"",
                    ""id"": ""0cc51d66-54fb-4e37-b526-c7b6abfd3955"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ColorChangeViolet"",
                    ""type"": ""Button"",
                    ""id"": ""71f03cde-8496-425e-ae7a-06fbf6c37156"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""7e0b6012-5f35-4eaf-a897-81c59eaf2830"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LIght_Emission"",
                    ""type"": ""Button"",
                    ""id"": ""63938ccf-ab0a-4f41-85a2-c10355796f06"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateObj"",
                    ""type"": ""Button"",
                    ""id"": ""10323015-21d0-4611-9c4a-c453c79e8cde"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""74d5806a-179a-4ec3-8192-a848311a2be4"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""096ef2a1-707d-4b95-82ab-e9c1aac510cb"",
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
                    ""id"": ""46b9316e-4c73-4bb1-af15-efb335355fd3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b7eeda00-50fe-402b-8673-06f64f38e8b6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""44e2914e-ddcd-444c-a107-c595403fd5b5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""158a63ef-47e4-4212-84a0-58bf7c809eb2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""980a9974-7dd2-45e9-bf4f-d8bc499a6b4e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8272b1e3-83f4-47eb-b3bb-68e78f1efb49"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60dc096c-a414-4ee0-8489-95c920ab3eed"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0428004a-31bd-4c83-a820-0e5146a68b32"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99d3e031-06da-42fb-8046-8e4ed4989967"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ColorChangeRed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee287451-8057-418f-aea4-75590e4de672"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ColorChangeRed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4a01c81-80ec-4339-a4c9-9b664f5a3dfb"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ColorChangeViolet"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5fc8f23-7894-4162-a58c-f94cda60bedc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ColorChangeViolet"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f196627a-5fe8-42a7-acaa-7a2dfc123021"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c409d82a-6109-4766-ae98-f55ab21e3c44"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1560d2c8-b233-42fa-8ca5-6a4f3a9ead29"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LIght_Emission"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86be12b8-90b8-4f34-a573-880d23f81900"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LIght_Emission"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42b0e6a7-cd1a-4f94-ae6a-45d97d6f96ac"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""RotateObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66574275-f593-449b-835d-db1179b4ca11"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""RotateObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0dbb7953-d430-4d6f-8356-394e40d4630d"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""RotateObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef5bf0ee-2963-4f26-9465-1a3786e41765"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Def"",
                    ""action"": ""RotateObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New Control Scheme"",
            ""bindingGroup"": ""New Control Scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Def"",
            ""bindingGroup"": ""Def"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_ColorChangeRed = m_Player.FindAction("ColorChangeRed", throwIfNotFound: true);
        m_Player_ColorChangeViolet = m_Player.FindAction("ColorChangeViolet", throwIfNotFound: true);
        m_Player_Interaction = m_Player.FindAction("Interaction", throwIfNotFound: true);
        m_Player_LIght_Emission = m_Player.FindAction("LIght_Emission", throwIfNotFound: true);
        m_Player_RotateObj = m_Player.FindAction("RotateObj", throwIfNotFound: true);
    }

    ~@PlayerLoco()
    {
        UnityEngine.Debug.Assert(!m_Player.enabled, "This will cause a leak and performance issues, PlayerLoco.Player.Disable() has not been called.");
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_ColorChangeRed;
    private readonly InputAction m_Player_ColorChangeViolet;
    private readonly InputAction m_Player_Interaction;
    private readonly InputAction m_Player_LIght_Emission;
    private readonly InputAction m_Player_RotateObj;
    public struct PlayerActions
    {
        private @PlayerLoco m_Wrapper;
        public PlayerActions(@PlayerLoco wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @ColorChangeRed => m_Wrapper.m_Player_ColorChangeRed;
        public InputAction @ColorChangeViolet => m_Wrapper.m_Player_ColorChangeViolet;
        public InputAction @Interaction => m_Wrapper.m_Player_Interaction;
        public InputAction @LIght_Emission => m_Wrapper.m_Player_LIght_Emission;
        public InputAction @RotateObj => m_Wrapper.m_Player_RotateObj;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @ColorChangeRed.started += instance.OnColorChangeRed;
            @ColorChangeRed.performed += instance.OnColorChangeRed;
            @ColorChangeRed.canceled += instance.OnColorChangeRed;
            @ColorChangeViolet.started += instance.OnColorChangeViolet;
            @ColorChangeViolet.performed += instance.OnColorChangeViolet;
            @ColorChangeViolet.canceled += instance.OnColorChangeViolet;
            @Interaction.started += instance.OnInteraction;
            @Interaction.performed += instance.OnInteraction;
            @Interaction.canceled += instance.OnInteraction;
            @LIght_Emission.started += instance.OnLIght_Emission;
            @LIght_Emission.performed += instance.OnLIght_Emission;
            @LIght_Emission.canceled += instance.OnLIght_Emission;
            @RotateObj.started += instance.OnRotateObj;
            @RotateObj.performed += instance.OnRotateObj;
            @RotateObj.canceled += instance.OnRotateObj;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @ColorChangeRed.started -= instance.OnColorChangeRed;
            @ColorChangeRed.performed -= instance.OnColorChangeRed;
            @ColorChangeRed.canceled -= instance.OnColorChangeRed;
            @ColorChangeViolet.started -= instance.OnColorChangeViolet;
            @ColorChangeViolet.performed -= instance.OnColorChangeViolet;
            @ColorChangeViolet.canceled -= instance.OnColorChangeViolet;
            @Interaction.started -= instance.OnInteraction;
            @Interaction.performed -= instance.OnInteraction;
            @Interaction.canceled -= instance.OnInteraction;
            @LIght_Emission.started -= instance.OnLIght_Emission;
            @LIght_Emission.performed -= instance.OnLIght_Emission;
            @LIght_Emission.canceled -= instance.OnLIght_Emission;
            @RotateObj.started -= instance.OnRotateObj;
            @RotateObj.performed -= instance.OnRotateObj;
            @RotateObj.canceled -= instance.OnRotateObj;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_NewControlSchemeSchemeIndex = -1;
    public InputControlScheme NewControlSchemeScheme
    {
        get
        {
            if (m_NewControlSchemeSchemeIndex == -1) m_NewControlSchemeSchemeIndex = asset.FindControlSchemeIndex("New Control Scheme");
            return asset.controlSchemes[m_NewControlSchemeSchemeIndex];
        }
    }
    private int m_DefSchemeIndex = -1;
    public InputControlScheme DefScheme
    {
        get
        {
            if (m_DefSchemeIndex == -1) m_DefSchemeIndex = asset.FindControlSchemeIndex("Def");
            return asset.controlSchemes[m_DefSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnColorChangeRed(InputAction.CallbackContext context);
        void OnColorChangeViolet(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnLIght_Emission(InputAction.CallbackContext context);
        void OnRotateObj(InputAction.CallbackContext context);
    }
}
