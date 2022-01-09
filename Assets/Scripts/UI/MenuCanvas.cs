using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuCanvas : MonoBehaviour
{
    #region Variable Fields
    //Configurations
    [SerializeField] private MenuEventChannel eventChannel = null;
    [SerializeField] private TweenUIConfig appear = null;
    [SerializeField] private TweenUIConfig dismiss = null;
    //Cached component references

    //States

    //Data storages
    #endregion

    #region Unity Methods

    private void Awake()
    {
        eventChannel.AppearEvent += OnAppear;
        eventChannel.DismissEvent += OnDismiss;

        Init();
    }

    private void OnDestroy()
    {
        eventChannel.AppearEvent -= OnAppear;
        eventChannel.DismissEvent -= OnDismiss;
    }

    private void OnEnable()
    {
        LeanTween
            .scale(gameObject, appear.Scale, appear.Duration)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                Time.timeScale = 0.0f;
            });
    }

    #endregion

    #region Private Methods
    private void Init()
    {
        ((RectTransform)transform).localScale = new Vector3(dismiss.Scale.x, dismiss.Scale.y, dismiss.Scale.z);
        gameObject.SetActive(false);
    }

    #endregion

    #region Public Methods
    public void OnDismiss()
    {
        Time.timeScale = 1.0f;
        LeanTween
            .scale(gameObject, dismiss.Scale, dismiss.Duration)
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
