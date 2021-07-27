// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Roaming"",
            ""id"": ""95833317-556d-41b8-b9e0-340a2922d4c9"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""488c01de-de84-4e79-b412-c0f48f8eec91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""04216e0c-1c0e-4dd1-9cce-14b69085dd4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""aa88aad0-c5ef-4203-bb14-d320f8e6ff79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""f84436cd-1104-4c52-9397-21f0bb6407c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d1b3b252-ba2f-45ee-94d5-b044441e4ae0"",
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
                    ""id"": ""8efa8d64-1aa6-4a8b-adc8-edb5313a950f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7b24ca34-7d82-48df-87d3-8d4dc29e4049"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8e9ce1d5-ad88-445b-848e-f107202e387f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ba7fe8b3-3f36-4fbc-b7de-e6320c5948d7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""06f0b044-ce6c-406b-8315-78f1c74692b1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95c47abb-39df-48d0-b6fc-e178435337c8"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77b1c0d7-0b28-4cee-a83b-6569bf58d3f2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""717adb0a-c09f-4f13-9b49-2bf3e40c6099"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""1f55472c-5d59-44fa-b49e-1ec888b5b83c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submenu"",
                    ""type"": ""Button"",
                    ""id"": ""f87761c8-729b-476f-bb8c-f7f7c90af992"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""cf34c12a-bdc8-4f9f-a239-51d43fc1c85b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c6415255-8b19-492b-80b5-b2868e9fc308"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pan"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c857e682-0c1d-475e-9d9b-68d12e8b9a14"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b935760a-5c83-4d3f-8a96-01d684af64c5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Deselect"",
                    ""type"": ""Button"",
                    ""id"": ""b8336885-e669-4a74-8787-9f0e6fbd61fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DoubleMovement"",
                    ""type"": ""Button"",
                    ""id"": ""bcf00b55-43cd-4838-8e20-7ec29b0edfb7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eba5a5fc-b9e4-4555-b6d4-31bc7a9343a6"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84586241-4fb4-40ce-9f42-e13110f4fe28"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46b07c30-042d-4d56-bfe4-ca623ce3c047"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Submenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53758380-0380-4b95-aa27-c8befd8efd1a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""KeyboardPan"",
                    ""id"": ""8455d1b6-bcc7-4916-ac1d-783cf7054f38"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f7f11a86-aa8b-438b-a24a-606e252c8f60"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""06ebe74e-dfdb-45e7-9120-1d9f44bcd5c9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bf3d06d7-0d92-44ce-ab93-51429e178007"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""31c6e826-8ea0-4c39-95cc-0749da9a8574"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""KeyboardZoom"",
                    ""id"": ""ee01a20d-bbd6-401b-b8bb-eed5d37cbc31"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""dc85648d-611f-4a55-b6e9-b3e6e9412546"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""98136378-4059-41fb-b3e9-01ed1ec340aa"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""aa7c9673-4c8c-4c2b-a810-571d32130205"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0853cf55-50e4-4966-b7c2-4855d031a20d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Deselect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3346a2e-7cae-466a-82eb-62583c23268d"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DoubleMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogue"",
            ""id"": ""cf7ef379-0b8c-450f-826e-f90bca4b1a98"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""671ceb3e-9082-4d80-96b0-39945629f973"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Up and Down (Keyboard)"",
                    ""id"": ""e6e5d907-1424-4681-980c-7ac3be693374"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9a620b98-cd29-4594-a80c-7d09f56cfd9d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9f8ba948-52fc-4b47-991d-c0c152c941db"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""26f87b3a-14c6-4d79-b5ec-adcbe35a68e6"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""8a81736d-abfe-4c30-8331-932814489887"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""99a5a1a4-e953-48b6-91e0-203ce46ba5db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""026395e3-b850-4214-af1c-61f98a690d11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b8539bdd-db10-4657-8e90-52dfd248152f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4aa480f-b809-4791-9f49-e0afcf95a45d"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52a3775d-8d5a-46f8-93a6-e35a439f934f"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5aad7e0-fc17-4e1f-801b-dc119de326ac"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccc7f383-9789-4de2-ab0b-84f5ffc6a63b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""b84a81b0-ec32-4799-99f9-4439c7f87d97"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7a446d2b-6127-4d7d-af40-3e378a8d34b9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5bcbd3c1-3f97-4511-b32b-e407486159e8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
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
        // Roaming
        m_Roaming = asset.FindActionMap("Roaming", throwIfNotFound: true);
        m_Roaming_Movement = m_Roaming.FindAction("Movement", throwIfNotFound: true);
        m_Roaming_Interact = m_Roaming.FindAction("Interact", throwIfNotFound: true);
        m_Roaming_Inventory = m_Roaming.FindAction("Inventory", throwIfNotFound: true);
        m_Roaming_Select = m_Roaming.FindAction("Select", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Select = m_Combat.FindAction("Select", throwIfNotFound: true);
        m_Combat_Submenu = m_Combat.FindAction("Submenu", throwIfNotFound: true);
        m_Combat_Inventory = m_Combat.FindAction("Inventory", throwIfNotFound: true);
        m_Combat_MousePosition = m_Combat.FindAction("MousePosition", throwIfNotFound: true);
        m_Combat_Pan = m_Combat.FindAction("Pan", throwIfNotFound: true);
        m_Combat_Zoom = m_Combat.FindAction("Zoom", throwIfNotFound: true);
        m_Combat_Deselect = m_Combat.FindAction("Deselect", throwIfNotFound: true);
        m_Combat_DoubleMovement = m_Combat.FindAction("DoubleMovement", throwIfNotFound: true);
        // Dialogue
        m_Dialogue = asset.FindActionMap("Dialogue", throwIfNotFound: true);
        m_Dialogue_Navigate = m_Dialogue.FindAction("Navigate", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Escape = m_Menu.FindAction("Escape", throwIfNotFound: true);
        m_Menu_Select = m_Menu.FindAction("Select", throwIfNotFound: true);
        m_Menu_Navigate = m_Menu.FindAction("Navigate", throwIfNotFound: true);
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

    // Roaming
    private readonly InputActionMap m_Roaming;
    private IRoamingActions m_RoamingActionsCallbackInterface;
    private readonly InputAction m_Roaming_Movement;
    private readonly InputAction m_Roaming_Interact;
    private readonly InputAction m_Roaming_Inventory;
    private readonly InputAction m_Roaming_Select;
    public struct RoamingActions
    {
        private @Controls m_Wrapper;
        public RoamingActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Roaming_Movement;
        public InputAction @Interact => m_Wrapper.m_Roaming_Interact;
        public InputAction @Inventory => m_Wrapper.m_Roaming_Inventory;
        public InputAction @Select => m_Wrapper.m_Roaming_Select;
        public InputActionMap Get() { return m_Wrapper.m_Roaming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RoamingActions set) { return set.Get(); }
        public void SetCallbacks(IRoamingActions instance)
        {
            if (m_Wrapper.m_RoamingActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_RoamingActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_RoamingActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_RoamingActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_RoamingActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_RoamingActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_RoamingActionsCallbackInterface.OnInteract;
                @Inventory.started -= m_Wrapper.m_RoamingActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_RoamingActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_RoamingActionsCallbackInterface.OnInventory;
                @Select.started -= m_Wrapper.m_RoamingActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_RoamingActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_RoamingActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_RoamingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public RoamingActions @Roaming => new RoamingActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_Select;
    private readonly InputAction m_Combat_Submenu;
    private readonly InputAction m_Combat_Inventory;
    private readonly InputAction m_Combat_MousePosition;
    private readonly InputAction m_Combat_Pan;
    private readonly InputAction m_Combat_Zoom;
    private readonly InputAction m_Combat_Deselect;
    private readonly InputAction m_Combat_DoubleMovement;
    public struct CombatActions
    {
        private @Controls m_Wrapper;
        public CombatActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_Combat_Select;
        public InputAction @Submenu => m_Wrapper.m_Combat_Submenu;
        public InputAction @Inventory => m_Wrapper.m_Combat_Inventory;
        public InputAction @MousePosition => m_Wrapper.m_Combat_MousePosition;
        public InputAction @Pan => m_Wrapper.m_Combat_Pan;
        public InputAction @Zoom => m_Wrapper.m_Combat_Zoom;
        public InputAction @Deselect => m_Wrapper.m_Combat_Deselect;
        public InputAction @DoubleMovement => m_Wrapper.m_Combat_DoubleMovement;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnSelect;
                @Submenu.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnSubmenu;
                @Submenu.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnSubmenu;
                @Submenu.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnSubmenu;
                @Inventory.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnInventory;
                @MousePosition.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMousePosition;
                @Pan.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnPan;
                @Pan.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnPan;
                @Pan.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnPan;
                @Zoom.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnZoom;
                @Deselect.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnDeselect;
                @Deselect.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnDeselect;
                @Deselect.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnDeselect;
                @DoubleMovement.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnDoubleMovement;
                @DoubleMovement.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnDoubleMovement;
                @DoubleMovement.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnDoubleMovement;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Submenu.started += instance.OnSubmenu;
                @Submenu.performed += instance.OnSubmenu;
                @Submenu.canceled += instance.OnSubmenu;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Pan.started += instance.OnPan;
                @Pan.performed += instance.OnPan;
                @Pan.canceled += instance.OnPan;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Deselect.started += instance.OnDeselect;
                @Deselect.performed += instance.OnDeselect;
                @Deselect.canceled += instance.OnDeselect;
                @DoubleMovement.started += instance.OnDoubleMovement;
                @DoubleMovement.performed += instance.OnDoubleMovement;
                @DoubleMovement.canceled += instance.OnDoubleMovement;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);

    // Dialogue
    private readonly InputActionMap m_Dialogue;
    private IDialogueActions m_DialogueActionsCallbackInterface;
    private readonly InputAction m_Dialogue_Navigate;
    public struct DialogueActions
    {
        private @Controls m_Wrapper;
        public DialogueActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Dialogue_Navigate;
        public InputActionMap Get() { return m_Wrapper.m_Dialogue; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogueActions set) { return set.Get(); }
        public void SetCallbacks(IDialogueActions instance)
        {
            if (m_Wrapper.m_DialogueActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_DialogueActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_DialogueActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_DialogueActionsCallbackInterface.OnNavigate;
            }
            m_Wrapper.m_DialogueActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
            }
        }
    }
    public DialogueActions @Dialogue => new DialogueActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Escape;
    private readonly InputAction m_Menu_Select;
    private readonly InputAction m_Menu_Navigate;
    public struct MenuActions
    {
        private @Controls m_Wrapper;
        public MenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_Menu_Escape;
        public InputAction @Select => m_Wrapper.m_Menu_Select;
        public InputAction @Navigate => m_Wrapper.m_Menu_Navigate;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Escape.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnEscape;
                @Select.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Navigate.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnNavigate;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IRoamingActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnSubmenu(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnPan(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnDeselect(InputAction.CallbackContext context);
        void OnDoubleMovement(InputAction.CallbackContext context);
    }
    public interface IDialogueActions
    {
        void OnNavigate(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnEscape(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
    }
}
