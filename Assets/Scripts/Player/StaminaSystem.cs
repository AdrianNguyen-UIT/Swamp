using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    #region Variable Fields
    //Configuration
    [SerializeField] private StaminaBar staminaBar = null;
    [SerializeField] private StaminaConfig staminaConfig = null;
    //Cached component references

    private float currentStamina = 0.0f;
    private float currentRecoverDelayTime = 0.0f;
    private bool allowRecoverStamina = true;
    //State
    #endregion

    #region Unity Methods
    private void Start()
    {
        currentStamina = staminaConfig.MaxStamina;
        staminaBar.SetMaxStamina(staminaConfig.MaxStamina);
        staminaBar.SetCurrentStamina(currentStamina);
        allowRecoverStamina = true;
    }

    private void Update()
    {
        if (!allowRecoverStamina)
            return;

        if (IsFull())
            return;

        currentRecoverDelayTime += Time.deltaTime;
        if (currentRecoverDelayTime >= staminaConfig.RecoverDelayTime)
        {
            RecoverStamina(staminaConfig.RecoverRate * Time.deltaTime);
        }
    }

    #endregion

    #region Private Methods
    private void RecoverStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina >= staminaConfig.MaxStamina)
        {
            currentStamina = staminaConfig.MaxStamina;
            currentRecoverDelayTime = 0.0f;
        }
        staminaBar.SetCurrentStamina(currentStamina);
    }

    #endregion

    public void ConsumeStamina(float amount)
    {
        currentStamina -= amount;
        currentRecoverDelayTime = 0.0f;
        if (currentStamina < 0.0f)
        {
            currentStamina = 0.0f;
        }
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public bool OutOfStamina()
    {
        return (currentStamina <= 0.0f);
    }

    public bool EnoughStamina(float amount)
    {
        return (currentStamina >= amount);
    }

    public bool IsFull()
    {
        return (currentStamina >= staminaConfig.MaxStamina);
    }

    public void ForceStopRecoverStamina()
    {
        allowRecoverStamina = false;
    }

    public void ForceStartRecoverStamina()
    {
        allowRecoverStamina = true;
    }

    public void RecoverFullStamina()
    {
        currentStamina = staminaConfig.MaxStamina;
        staminaBar.SetCurrentStamina(currentStamina);
    }
}
