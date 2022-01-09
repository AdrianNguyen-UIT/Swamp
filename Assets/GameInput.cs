// GENERATED AUTOMATICALLY FROM 'Assets/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""8c59d9e9-f93c-4407-8039-8a78dccd5447"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""02a5f91a-5ea8-4635-a8cc-b987408ca858"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1561b996-ff90-461f-83f3-34ef89ac887b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""25a99a83-b248-49c0-a9d4-a92e989bec41"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1,pressPoint=0.1)""
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""892f5608-d10b-4adc-a2b6-dd093e144ef1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""e002ae2c-a294-409d-9890-d4cb16364dea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActiveFirstAbility"",
                    ""type"": ""Button"",
                    ""id"": ""e0d46a96-28de-48f4-a445-fe73dde91606"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActiveSecondAbility"",
                    ""type"": ""Button"",
                    ""id"": ""5528ced1-7d2a-4f4f-8dca-3191c607e26e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cheat"",
                    ""type"": ""Button"",
                    ""id"": ""188600a4-2580-49d0-a7f6-5d2209a58484"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""444a35d9-8d0b-4eb5-b72c-415ed107a84d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""04f597fb-0c9e-4f7b-aaec-6b87700c0d70"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4881ec65-1210-40e0-a9c8-77133ca85270"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bdf355d5-3032-41ab-a235-86d92cefb30e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9a7bd4ee-c1f1-4d3f-9b41-1dd68f79917c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b779804d-e2d4-41af-be37-2dcbfbec2535"",
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
                    ""id"": ""1beab76b-0f8b-455e-883f-6186a66ec82a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc19f7a2-24e0-4807-a8f5-fed673ccb88c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""971674d4-1a85-4c28-84a0-dec4eda1e3bd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36110341-3232-46fb-8608-f20bd2ce1666"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActiveFirstAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""679eb080-c2fc-4188-98b3-7ed399c887d2"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActiveSecondAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06fc9be9-6be1-4428-b9c2-1953aab54b17"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""f83e13fd-50b4-4926-a5c7-5a34a0769442"",
            ""actions"": [
                {
                    ""name"": ""MoveMenu"",
                    ""type"": ""Value"",
                    ""id"": ""c002588e-1dc5-46ef-aea8-d0e5ab3187e5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""f1360ee4-e32e-4eaa-8ee5-58df5933b6b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""557bacf8-3e85-4510-bef1-f1fe7fb86519"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwapAbility"",
                    ""type"": ""Button"",
                    ""id"": ""e65960a6-5fec-49c4-9b17-41bb852d8d91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""0d07d600-fe82-45ef-84f4-264d9bf7db75"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMenu"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f0760e94-25da-44c4-9c32-435ea1a46ff3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0e3570b5-a48c-43c4-a5db-93ed3221c74b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""90f7cb22-ddd8-4993-b44c-1010262ccf38"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5c885a1-084c-4ee2-a9e1-db33c98916c7"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3687cbe4-e276-4cc7-bf2b-a10d0ab8ec48"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05ba64ec-3716-42b0-b3ab-548fd0e4d33d"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Sprint = m_Gameplay.FindAction("Sprint", throwIfNotFound: true);
        m_Gameplay_Grab = m_Gameplay.FindAction("Grab", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_ActiveFirstAbility = m_Gameplay.FindAction("ActiveFirstAbility", throwIfNotFound: true);
        m_Gameplay_ActiveSecondAbility = m_Gameplay.FindAction("ActiveSecondAbility", throwIfNotFound: true);
        m_Gameplay_Cheat = m_Gameplay.FindAction("Cheat", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_MoveMenu = m_Menu.FindAction("MoveMenu", throwIfNotFound: true);
        m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
        m_Menu_Confirm = m_Menu.FindAction("Confirm", throwIfNotFound: true);
        m_Menu_SwapAbility = m_Menu.FindAction("SwapAbility", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Sprint;
    private readonly InputAction m_Gameplay_Grab;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_ActiveFirstAbility;
    private readonly InputAction m_Gameplay_ActiveSecondAbility;
    private readonly InputAction m_Gameplay_Cheat;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Sprint => m_Wrapper.m_Gameplay_Sprint;
        public InputAction @Grab => m_Wrapper.m_Gameplay_Grab;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @ActiveFirstAbility => m_Wrapper.m_Gameplay_ActiveFirstAbility;
        public InputAction @ActiveSecondAbility => m_Wrapper.m_Gameplay_ActiveSecondAbility;
        public InputAction @Cheat => m_Wrapper.m_Gameplay_Cheat;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Grab.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGrab;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @ActiveFirstAbility.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActiveFirstAbility;
                @ActiveFirstAbility.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActiveFirstAbility;
                @ActiveFirstAbility.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActiveFirstAbility;
                @ActiveSecondAbility.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActiveSecondAbility;
                @ActiveSecondAbility.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActiveSecondAbility;
                @ActiveSecondAbility.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnActiveSecondAbility;
                @Cheat.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCheat;
                @Cheat.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCheat;
                @Cheat.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCheat;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @ActiveFirstAbility.started += instance.OnActiveFirstAbility;
                @ActiveFirstAbility.performed += instance.OnActiveFirstAbility;
                @ActiveFirstAbility.canceled += instance.OnActiveFirstAbility;
                @ActiveSecondAbility.started += instance.OnActiveSecondAbility;
                @ActiveSecondAbility.performed += instance.OnActiveSecondAbility;
                @ActiveSecondAbility.canceled += instance.OnActiveSecondAbility;
                @Cheat.started += instance.OnCheat;
                @Cheat.performed += instance.OnCheat;
                @Cheat.canceled += instance.OnCheat;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_MoveMenu;
    private readonly InputAction m_Menu_Cancel;
    private readonly InputAction m_Menu_Confirm;
    private readonly InputAction m_Menu_SwapAbility;
    public struct MenuActions
    {
        private @GameInput m_Wrapper;
        public MenuActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveMenu => m_Wrapper.m_Menu_MoveMenu;
        public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
        public InputAction @Confirm => m_Wrapper.m_Menu_Confirm;
        public InputAction @SwapAbility => m_Wrapper.m_Menu_SwapAbility;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @MoveMenu.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveMenu;
                @MoveMenu.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveMenu;
                @MoveMenu.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveMenu;
                @Cancel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Confirm.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @SwapAbility.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSwapAbility;
                @SwapAbility.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSwapAbility;
                @SwapAbility.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSwapAbility;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveMenu.started += instance.OnMoveMenu;
                @MoveMenu.performed += instance.OnMoveMenu;
                @MoveMenu.canceled += instance.OnMoveMenu;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @SwapAbility.started += instance.OnSwapAbility;
                @SwapAbility.performed += instance.OnSwapAbility;
                @SwapAbility.canceled += instance.OnSwapAbility;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnActiveFirstAbility(InputAction.CallbackContext context);
        void OnActiveSecondAbility(InputAction.CallbackContext context);
        void OnCheat(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMoveMenu(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnSwapAbility(InputAction.CallbackContext context);
    }
}
