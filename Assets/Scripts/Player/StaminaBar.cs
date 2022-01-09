using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    #region Variable Fields
    //Configuration
    //Cached component references
    private Slider slider = null;
    //State
    #endregion

    #region Unity Methods
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    #endregion

    #region Private Methods
    #endregion

    public void SetCurrentStamina(float amount)
    {
        slider.value = amount;
    }

    public void SetMaxStamina(float amount)
    {
        slider.maxValue = amount;
    }
}
