using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class Player : Singleton<Player>
{
    #region Variable Fields
    //Configurations
    [SerializeField] private MenuEventChannel gameOverMenuEventChannel = null;
    //Cached component references

    //States

    //Data storages
    #endregion

    #region Unity Methods
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public void Die()
    {
        LockPlayerMovement();
        Debug.Log("Die");

        gameOverMenuEventChannel.OnAppear();
    }

    public int GetLayerMask()
    {
        return 1 << gameObject.layer;
    }

    public void LockPlayerMovement()
    {
        GetComponent<CharacterController2D>().LockMovement(true);
        GetComponent<Animator>().enabled = false;
    }

    public void UnlockPlayerMovement()
    {
        GetComponent<CharacterController2D>().LockMovement(false);
        GetComponent<Animator>().enabled = true;
    }
    #endregion
}
