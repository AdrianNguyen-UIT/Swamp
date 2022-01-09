using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MenuButtonController : MonoBehaviour
{
    #region Variable Fields
    //Configurations
    [SerializeField] private MenuEventChannel menuEventChannel = null;
    [SerializeField] private TweenUIConfig appear = null;
    [SerializeField] private TweenUIConfig dismiss = null;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private InputReader inputReader = null;
    [SerializeField] private MenuButtonEventChannel[] menuButtonEventChannels = null;

    [Space]
    [SerializeField] private bool allowLockMove = true;

    //Cached component references
    [Header("Cancel button Event Channel")]
    [SerializeField] private MenuButtonEventChannel defaultButtonEventChannel = null;
    [SerializeField] private MenuButtonEventChannel cancelButtonEventChannel = null;

    [Space]
    [SerializeField] private bool enableAbilitySwap = false;
    //States
    private bool lockMove = false;
    //Data storages
    private int currentMenuButtonIndex = -1;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        menuEventChannel.DismissEvent += OnDismiss;
        menuEventChannel.AppearEvent += OnAppear;

        gameObject.SetActive(false);
        currentMenuButtonIndex = -1;
    }

    private void OnDestroy()
    {
        menuEventChannel.DismissEvent -= OnDismiss;
        menuEventChannel.AppearEvent -= OnAppear;

        if (enableAbilitySwap)
            inputReader.SwapAbilityEvent = null;
    }

    private void OnEnable()
    {
        inputReader.EnableMenuInput();
        inputReader.MoveMenuEvent += OnMoveMenuEvent;
        inputReader.ConfirmEvent += OnConfirm;
        inputReader.CancelEvent += OnCancel;

        if (enableAbilitySwap && AbilitySystem.Instance)
            inputReader.SwapAbilityEvent += AbilitySystem.Instance.OnSwapAbility;

        Init();
    }
    private void OnDisable()
    {
        inputReader.MoveMenuEvent -= OnMoveMenuEvent;
        inputReader.ConfirmEvent -= OnConfirm;
        inputReader.CancelEvent -= OnCancel;

        if (enableAbilitySwap && AbilitySystem.Instance)
            inputReader.SwapAbilityEvent -= AbilitySystem.Instance.OnSwapAbility;
    }
    #endregion

    #region Private Methods
    private void Init()
    {
        if (currentMenuButtonIndex >= 0)
        {
            for (int index = 0; index < menuButtonEventChannels.Length; index++)
            {
                if (index == currentMenuButtonIndex)
                    menuButtonEventChannels[index].DefaultInit = true;
                else
                    menuButtonEventChannels[index].DefaultInit = false;
            }
        }
        else if (defaultButtonEventChannel != null)
        {
            for (int index = 0; index < menuButtonEventChannels.Length; index++)
            {
                if (menuButtonEventChannels[index] == defaultButtonEventChannel)
                {
                    currentMenuButtonIndex = index;
                    menuButtonEventChannels[index].DefaultInit = true;
                }
                else
                {
                    menuButtonEventChannels[index].DefaultInit = false;
                }
            }
        }
        else
        {
            currentMenuButtonIndex = 0;
        }

        lockMove = true;
        canvasGroup.alpha = 0.0f;
        LeanTween
            .alphaCanvas(canvasGroup, appear.Alpha, appear.Duration)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => {
            lockMove = false;
        });
    }

    #endregion

    #region Public Methods
    public void OnMoveMenuEvent(InputAction.CallbackContext context)
    {
        if (allowLockMove)
            if (lockMove)
                return;

        if (context.performed)
        {
            menuButtonEventChannels[currentMenuButtonIndex].OnDeselected();

            float axis = context.ReadValue<float>();
            if (axis > 0.0f)
            {
                --currentMenuButtonIndex;
                if (currentMenuButtonIndex < 0)
                    currentMenuButtonIndex = menuButtonEventChannels.Length - 1;
            }
            else if (axis < 0.0f)
            {
                ++currentMenuButtonIndex;
            }
            currentMenuButtonIndex %= menuButtonEventChannels.Length;

            menuButtonEventChannels[currentMenuButtonIndex].OnSelected();
        }
    }

    public void OnConfirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lockMove = true;
            menuButtonEventChannels[currentMenuButtonIndex].OnPressed();
            Debug.Log($"Confirm Button {currentMenuButtonIndex + 1} pressed");
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            menuButtonEventChannels[currentMenuButtonIndex].OnDeselected();

            lockMove = true;
            cancelButtonEventChannel.OnPressed();
            Debug.Log($"Cancel Button {currentMenuButtonIndex + 1} pressed");
        }
    }

    public void OnDismiss()
    {
        lockMove = true;
        canvasGroup.alpha = 1.0f;
        LeanTween
            .alphaCanvas(canvasGroup, dismiss.Alpha, dismiss.Duration)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => {
            gameObject.SetActive(false);
        });
    }

    public void OnAppear()
    {
        gameObject.SetActive(true);
    }
    #endregion
}
