using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaRune : InteractableObject
{
    #region Variable Fields
    //Configurations

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
        Player.Instance.GetComponent<StaminaSystem>().RecoverFullStamina();
        base.OnTouched();
    }
    #endregion
}
