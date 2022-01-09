using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRune : InteractableObject
{
    #region Variable Fields
    //Configurations
    [SerializeField] private Ability ability = null;
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
        if (AbilitySystem.Instance.PickUpAbility(ability))
            base.OnTouched();
    }
    #endregion
}
