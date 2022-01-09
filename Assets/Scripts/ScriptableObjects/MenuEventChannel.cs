using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/MenuEvent Channel", fileName = "New MenuEventChannel")]
public class MenuEventChannel : ScriptableObject
{
    public UnityAction DismissEvent;
    public UnityAction AppearEvent;
    public void OnDismissed()
    {
        if (DismissEvent != null)
        {
            DismissEvent.Invoke();
        }
    }

    public void OnAppear()
    {
        if (AppearEvent != null)
        {
            AppearEvent.Invoke();
        }
    }
}
