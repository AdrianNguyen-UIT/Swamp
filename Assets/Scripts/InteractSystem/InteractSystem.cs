using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InteractType
{
    Grab,
    Touch,
    Touch_InFrozenTime
}

public class InteractSystem: MonoBehaviour
{
    #region Variable Fields
    //Configuration
    [SerializeField] private LayerMask m_WhatIsObject = 0;
    [SerializeField] private Transform m_ObjectCheck = null;
    [SerializeField] private float objectCheckCircleRadius = 0.5f;
    //Cached component references
    private InteractableObject detectedObject = null;
    private bool isGrabbing = false;
    //State
    #endregion

    #region Unity Methods
    private void Awake()
    {
    }

    private void OnDrawGizmos()
    {
        if (detectedObject)
            Gizmos.color = Color.yellow;
        else
            Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(m_ObjectCheck.position, objectCheckCircleRadius);
    }

    private void Update()
    {
        DetectObject();
    }

    #endregion

    #region Private Methods

    void DetectObject()
    {
        Collider2D checkObjectCollider = Physics2D.OverlapCircle(m_ObjectCheck.position, objectCheckCircleRadius, m_WhatIsObject);
        if (checkObjectCollider)
        {
            detectedObject = checkObjectCollider.GetComponent<InteractableObject>();
            if (detectedObject.IOConfig.InteractType == InteractType.Touch)
            {
                detectedObject.OnTouched();
            }
        }
        else
        {
            detectedObject = null;
        }
    }
    #endregion

    public void OnGrab(InputAction.CallbackContext context)
    {
        bool wasGrabbing = isGrabbing;
        isGrabbing = false;

        if (detectedObject && detectedObject.IOConfig.InteractType == InteractType.Grab)
        {
            if (context.performed)
            {
                isGrabbing = true;
                if (!wasGrabbing)
                {
                    GetComponent<CharacterController2D>().SetAllowFlip(false);
                    detectedObject.OnGrabbed();
                }
            }
            else if (context.canceled)
            {
                isGrabbing = false;
                GetComponent<CharacterController2D>().SetAllowFlip(true);
                detectedObject.OnReleased();

            }
        }
    }

    public bool IsGrabbing()
    {
        return isGrabbing;
    }
}
