using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Interactable Object Config", fileName = "New Interactable Object Config")]
public class InteractableObjectConfig : ScriptableObject
{
    #region Variable Fields
    [SerializeField] private InteractType interactType = InteractType.Touch;
    [SerializeField] private PhysicsMaterial2D idleFriction = null;
    [SerializeField] private PhysicsMaterial2D grabbedFriction = null;

    public InteractType InteractType => interactType;
    public PhysicsMaterial2D IdleFriction => idleFriction;
    public PhysicsMaterial2D GrabbedFriction => grabbedFriction;

    #endregion

    #region Unity Methods
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    #endregion
}
