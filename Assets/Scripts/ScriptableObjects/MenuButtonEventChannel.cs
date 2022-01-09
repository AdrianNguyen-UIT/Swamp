using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Events/MenuButtonEvent Channel", fileName = "New MenuButtonEventChannel")]
public class MenuButtonEventChannel : ScriptableObject
{
    public string ButtonName = string.Empty;
    public UnityAction SelectedEvent;
    public UnityAction DeselectedEvent;
    public UnityAction PressedEvent;
    public UnityAction HotReloadEvent;

    [HideInInspector]
    public bool DefaultInit = false;

    public void OnSelected()
    {
        if (SelectedEvent != null)
        {
            SelectedEvent.Invoke();
        }
    }

    public void OnDeselected()
    {
        if (DeselectedEvent != null)
        {
            DeselectedEvent.Invoke();
        }
    }

    public void OnPressed()
    {
        if (PressedEvent != null)
        {
            PressedEvent.Invoke();
        }
    }

    public void OnHotReload()
    {
        if (HotReloadEvent != null)
            HotReloadEvent.Invoke();
    }
}
