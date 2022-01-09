using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RevealedTerrain : MonoBehaviour
{
    enum ActionOnTouch
    {
        Appear,
        Disappear
    }

    #region Variable Fields
    //Configurations
    [SerializeField] private ActionOnTouch actionOnTouch = ActionOnTouch.Disappear;
    //Cached component references
    private Animator animator = null;
    //States
    private bool revealed = false;
    //Data storages

    #endregion

    #region Unity Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetDefaultState();
    }

    private void OnEnable()
    {
        SetDefaultState();
    }
    #endregion

    #region Private Methods

    private void SetDefaultState()
    {
        if (revealed)
        {
            if (actionOnTouch == ActionOnTouch.Appear)
                animator.SetBool("Default_Appear", true);
            else if (actionOnTouch == ActionOnTouch.Disappear)
                animator.SetBool("Default_Appear", false);
        }
        else
        {
            if (actionOnTouch == ActionOnTouch.Appear)
                animator.SetBool("Default_Appear", false);
            else if (actionOnTouch == ActionOnTouch.Disappear)
                animator.SetBool("Default_Appear", true);
        }
    }
    #endregion

    #region Public Methods
    public void Reveal()
    {
        revealed = true;
        if (actionOnTouch == ActionOnTouch.Appear)
        {
            animator.SetTrigger("Appear");
            animator.SetBool("Default_Appear", true);
        }
        else if (actionOnTouch == ActionOnTouch.Disappear)
        {
            animator.SetTrigger("Disappear");
            animator.SetBool("Default_Appear", false);
        }
    }

    public void EnableTerrain()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public void DisableTerrain()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    #endregion
}
