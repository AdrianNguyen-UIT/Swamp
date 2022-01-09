using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    #region Variable Fields
    //Configurations

    //Cached component references

    //States

    //Data storages
    #endregion

    #region Unity Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Player.Instance.Die();
    }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    #endregion
}
