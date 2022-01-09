using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class MenuButton : MonoBehaviour
{
    #region Variable Fields
    //Configurations
    [SerializeField] protected MenuButtonEventChannel menuButtonEventChannel = null;

    [Header("UI")]
    [SerializeField] protected Image box = null;
    [SerializeField] protected Image pixels = null;
    [SerializeField] protected TextMeshProUGUI buttonName = null;

    [Header("Configuration")]
    [SerializeField] protected TweenUIConfig deselectedConfig = null;
    [SerializeField] protected TweenUIConfig selectedConfig = null;
    [SerializeField] private TweenUIConfig pressedConfig = null;

    [Space]
    [SerializeField] private bool allowMutilePress = false;
    [Range(0.3f, 2.0f)][SerializeField] private float recoverTime = 1.0f;

    //Cached component references

    [Header("Events")]
    public UnityEvent OnSelectedEvent;
    public UnityEvent OnDeselectedEvent;
    public UnityEvent OnPressedEvent;
    //States
    protected bool pressed = false;
    //Data storages
    private float currentRecoverTime = 0.0f;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (OnSelectedEvent == null)
            OnSelectedEvent = new UnityEvent();

        if (OnDeselectedEvent == null)
            OnDeselectedEvent = new UnityEvent();

        if (OnPressedEvent == null)
            OnPressedEvent = new UnityEvent();
    }

    private void OnEnable()
    {
        menuButtonEventChannel.SelectedEvent += OnSelected;
        menuButtonEventChannel.DeselectedEvent += OnDeselected;
        menuButtonEventChannel.PressedEvent += OnPressed;
        menuButtonEventChannel.HotReloadEvent += OnHotReload;

        Init();
    }

    private void OnDisable()
    {
        menuButtonEventChannel.SelectedEvent -= OnSelected;
        menuButtonEventChannel.DeselectedEvent -= OnDeselected;
        menuButtonEventChannel.PressedEvent -= OnPressed;
        menuButtonEventChannel.HotReloadEvent -= OnHotReload;
    }

    private void Update()
    {
        if (allowMutilePress)
        {
            if (pressed)
            {
                currentRecoverTime += Time.deltaTime;
                if (currentRecoverTime >= recoverTime)
                {
                    pressed = false;
                    currentRecoverTime = 0.0f;
                }
            }
        }
    }
    #endregion

    #region Private Methods
    protected virtual void Init()
    {
        pressed = false;
        buttonName.text = menuButtonEventChannel.ButtonName;

        if (menuButtonEventChannel.DefaultInit)
        {
            transform.localScale = selectedConfig.Scale;
            box.color = selectedConfig.Color;
            pixels.gameObject.SetActive(true);
        }
        else
        {
            transform.localScale = deselectedConfig.Scale;
            box.color = deselectedConfig.Color;
            pixels.gameObject.SetActive(false);
        }

    }

    protected virtual void OnHotReload()
    {

    }
    #endregion

    #region Public Methods

    public void OnSelected()
    {
        pixels.gameObject.SetActive(true);

        LeanTween
            .scale(gameObject, selectedConfig.Scale, selectedConfig.Duration)
            .setIgnoreTimeScale(true);
        LeanTween
            .color(box.rectTransform, selectedConfig.Color, selectedConfig.Duration)
            .setIgnoreTimeScale(true);
        OnSelectedEvent.Invoke();
    }

    public void OnDeselected()
    {
        LeanTween
            .scale(gameObject, deselectedConfig.Scale, deselectedConfig.Duration)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => pixels.gameObject.SetActive(false));
        LeanTween
            .color(box.rectTransform, deselectedConfig.Color, deselectedConfig.Duration)
            .setIgnoreTimeScale(true);
        OnDeselectedEvent.Invoke();
    }

    public void OnPressed()
    {
        if (pressed)
            return;

        pressed = true;
        if (!pixels.gameObject.activeInHierarchy)
            pixels.gameObject.SetActive(true);

        LeanTween
            .color(box.rectTransform, pressedConfig.Color, pressedConfig.Duration)
            .setLoopPingPong(pressedConfig.LoopCount)
            .setIgnoreTimeScale(true);
        LeanTween
            .scale(gameObject, pressedConfig.Scale, pressedConfig.Duration)
            .setLoopPingPong(pressedConfig.LoopCount)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => OnPressedEvent.Invoke());

    }
    #endregion
}
