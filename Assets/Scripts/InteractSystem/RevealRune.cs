using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealRune : InteractableObject
{
    #region Variable Fields
    //Configurations
    [Header("Subcribe To Channel")]
    [SerializeField] private VoidEventChannel eventChannel = null;
    //Cached component references

    //States

    //Data storages
    #endregion

    #region Unity Methods
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public override void OnTouched()
    {
        eventChannel.RaiseEvent();
        base.OnTouched();
    }

    protected override void OnLoadDestroyed()
    {
        base.OnLoadDestroyed();
    }
    #endregion
}
