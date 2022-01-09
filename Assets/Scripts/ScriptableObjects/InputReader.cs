using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Input Reader", fileName = "New Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenuActions
{
    //Gameplay
    public UnityAction<InputAction.CallbackContext> GrabEvent;
    public UnityAction<InputAction.CallbackContext> JumpEvent;
    public UnityAction<InputAction.CallbackContext> MoveEvent;
    public UnityAction<InputAction.CallbackContext> ReverseEvent;
    public UnityAction<InputAction.CallbackContext> SprintEvent;
    public UnityAction<InputAction.CallbackContext> PauseEvent;
    public UnityAction<InputAction.CallbackContext> ActiveFirstAbilityEvent;
    public UnityAction<InputAction.CallbackContext> ActiveSecondAbilityEvent;
    public UnityAction<InputAction.CallbackContext> CheatEvent;

    //Menu
    public UnityAction<InputAction.CallbackContext> MoveMenuEvent;
    public UnityAction<InputAction.CallbackContext> ConfirmEvent;
    public UnityAction<InputAction.CallbackContext> CancelEvent;
    public UnityAction<InputAction.CallbackContext> SwapAbilityEvent;

    private GameInput gameInput = null;

    private void OnEnable()
    {
        if (gameInput == null)
        {
            gameInput = new GameInput();
            gameInput.Gameplay.SetCallbacks(this);
            gameInput.Menu.SetCallbacks(this);
        }
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    #region GameInput.Gameplay
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (GrabEvent != null)
        {
            GrabEvent.Invoke(context);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (JumpEvent != null)
        {
            JumpEvent.Invoke(context);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (MoveEvent != null)
        {
            MoveEvent.Invoke(context);
        }
    }

    public void OnReverse(InputAction.CallbackContext context)
    {
        if (ReverseEvent != null)
        {
            ReverseEvent.Invoke(context);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (SprintEvent != null)
        {
            SprintEvent.Invoke(context);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (PauseEvent != null)
        {
            PauseEvent.Invoke(context);
        }
    }

    public void OnActiveFirstAbility(InputAction.CallbackContext context)
    {
        if (ActiveFirstAbilityEvent != null)
        {
            ActiveFirstAbilityEvent.Invoke(context);
        }
    }

    public void OnActiveSecondAbility(InputAction.CallbackContext context)
    {
        if (ActiveSecondAbilityEvent != null)
        {
            ActiveSecondAbilityEvent.Invoke(context);
        }
    }
    #endregion


    #region GameInput.Menu
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (CancelEvent != null)
        {
            CancelEvent.Invoke(context);
        }
    }
    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (ConfirmEvent != null)
        {
            ConfirmEvent.Invoke(context);
        }
    }

    public void OnMoveMenu(InputAction.CallbackContext context)
    {
        if (MoveMenuEvent != null)
        {
            MoveMenuEvent.Invoke(context);
        }
    }

    public void OnSwapAbility(InputAction.CallbackContext context)
    {
        if (SwapAbilityEvent != null)
        {
            SwapAbilityEvent.Invoke(context);
        }
    }
    #endregion

    public void EnableMenuInput()
    {
        gameInput.Menu.Enable();
        gameInput.Gameplay.Disable();
    }

    public void EnableGameplayInput()
    {
        gameInput.Gameplay.Enable();
        gameInput.Menu.Disable();
    }

    public void DisableAllInput()
    {
        gameInput.Gameplay.Disable();
    }

    public void OnCheat(InputAction.CallbackContext context)
    {
        if (CheatEvent != null)
        {
            CheatEvent.Invoke(context);
        }
    }
}
