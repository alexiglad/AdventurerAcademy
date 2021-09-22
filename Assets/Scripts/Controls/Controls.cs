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
            ""name"": ""UniversalControls"",
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
                },
                {
                    ""name"": ""PauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""5d0eb983-06ca-4487-be44-cb2677fbd2ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""3b854d87-2b8a-41df-bc62-cdbdd8c56d01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""44abf743-f10d-4e0d-81bf-894559e70a61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TEMP"",
                    ""type"": ""Button"",
                    ""id"": ""12dd50e4-4e61-438d-ad50-4518c3a6344e"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""013b110c-df6a-448f-8a42-1fe183bceb84"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7407b2ea-ba49-4be8-bfb5-acc27ab7d080"",
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
                    ""id"": ""8b848da8-ef97-4609-933c-4bd4a5ea7371"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""678400cb-9ca0-458a-941e-f2ee534d732a"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TEMP"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        // UniversalControls
        m_UniversalControls = asset.FindActionMap("UniversalControls", throwIfNotFound: true);
        m_UniversalControls_Select = m_UniversalControls.FindAction("Select", throwIfNotFound: true);
        m_UniversalControls_Submenu = m_UniversalControls.FindAction("Submenu", throwIfNotFound: true);
        m_UniversalControls_Inventory = m_UniversalControls.FindAction("Inventory", throwIfNotFound: true);
        m_UniversalControls_MousePosition = m_UniversalControls.FindAction("MousePosition", throwIfNotFound: true);
        m_UniversalControls_Pan = m_UniversalControls.FindAction("Pan", throwIfNotFound: true);
        m_UniversalControls_Zoom = m_UniversalControls.FindAction("Zoom", throwIfNotFound: true);
        m_UniversalControls_Deselect = m_UniversalControls.FindAction("Deselect", throwIfNotFound: true);
        m_UniversalControls_DoubleMovement = m_UniversalControls.FindAction("DoubleMovement", throwIfNotFound: true);
        m_UniversalControls_PauseMenu = m_UniversalControls.FindAction("PauseMenu", throwIfNotFound: true);
        m_UniversalControls_Interact = m_UniversalControls.FindAction("Interact", throwIfNotFound: true);
        m_UniversalControls_Space = m_UniversalControls.FindAction("Space", throwIfNotFound: true);
        m_UniversalControls_TEMP = m_UniversalControls.FindAction("TEMP", throwIfNotFound: true);
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

    // UniversalControls
    private readonly InputActionMap m_UniversalControls;
    private IUniversalControlsActions m_UniversalControlsActionsCallbackInterface;
    private readonly InputAction m_UniversalControls_Select;
    private readonly InputAction m_UniversalControls_Submenu;
    private readonly InputAction m_UniversalControls_Inventory;
    private readonly InputAction m_UniversalControls_MousePosition;
    private readonly InputAction m_UniversalControls_Pan;
    private readonly InputAction m_UniversalControls_Zoom;
    private readonly InputAction m_UniversalControls_Deselect;
    private readonly InputAction m_UniversalControls_DoubleMovement;
    private readonly InputAction m_UniversalControls_PauseMenu;
    private readonly InputAction m_UniversalControls_Interact;
    private readonly InputAction m_UniversalControls_Space;
    private readonly InputAction m_UniversalControls_TEMP;
    public struct UniversalControlsActions
    {
        private @Controls m_Wrapper;
        public UniversalControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_UniversalControls_Select;
        public InputAction @Submenu => m_Wrapper.m_UniversalControls_Submenu;
        public InputAction @Inventory => m_Wrapper.m_UniversalControls_Inventory;
        public InputAction @MousePosition => m_Wrapper.m_UniversalControls_MousePosition;
        public InputAction @Pan => m_Wrapper.m_UniversalControls_Pan;
        public InputAction @Zoom => m_Wrapper.m_UniversalControls_Zoom;
        public InputAction @Deselect => m_Wrapper.m_UniversalControls_Deselect;
        public InputAction @DoubleMovement => m_Wrapper.m_UniversalControls_DoubleMovement;
        public InputAction @PauseMenu => m_Wrapper.m_UniversalControls_PauseMenu;
        public InputAction @Interact => m_Wrapper.m_UniversalControls_Interact;
        public InputAction @Space => m_Wrapper.m_UniversalControls_Space;
        public InputAction @TEMP => m_Wrapper.m_UniversalControls_TEMP;
        public InputActionMap Get() { return m_Wrapper.m_UniversalControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UniversalControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUniversalControlsActions instance)
        {
            if (m_Wrapper.m_UniversalControlsActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSelect;
                @Submenu.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSubmenu;
                @Submenu.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSubmenu;
                @Submenu.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSubmenu;
                @Inventory.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnInventory;
                @MousePosition.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnMousePosition;
                @Pan.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnPan;
                @Pan.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnPan;
                @Pan.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnPan;
                @Zoom.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnZoom;
                @Deselect.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnDeselect;
                @Deselect.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnDeselect;
                @Deselect.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnDeselect;
                @DoubleMovement.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnDoubleMovement;
                @DoubleMovement.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnDoubleMovement;
                @DoubleMovement.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnDoubleMovement;
                @PauseMenu.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnPauseMenu;
                @Interact.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnInteract;
                @Space.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSpace;
                @Space.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSpace;
                @Space.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnSpace;
                @TEMP.started -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnTEMP;
                @TEMP.performed -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnTEMP;
                @TEMP.canceled -= m_Wrapper.m_UniversalControlsActionsCallbackInterface.OnTEMP;
            }
            m_Wrapper.m_UniversalControlsActionsCallbackInterface = instance;
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
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Space.started += instance.OnSpace;
                @Space.performed += instance.OnSpace;
                @Space.canceled += instance.OnSpace;
                @TEMP.started += instance.OnTEMP;
                @TEMP.performed += instance.OnTEMP;
                @TEMP.canceled += instance.OnTEMP;
            }
        }
    }
    public UniversalControlsActions @UniversalControls => new UniversalControlsActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IUniversalControlsActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnSubmenu(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnPan(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnDeselect(InputAction.CallbackContext context);
        void OnDoubleMovement(InputAction.CallbackContext context);
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSpace(InputAction.CallbackContext context);
        void OnTEMP(InputAction.CallbackContext context);
    }
}
