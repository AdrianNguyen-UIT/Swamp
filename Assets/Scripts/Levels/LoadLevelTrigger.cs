using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTrigger : MonoBehaviour
{
    enum LevelToLoad
    {
        Next,
        Previous
    }

    [SerializeField] private float triggerRadius = 1.0f;

    [SerializeField] private LevelToLoad levelToLoad = LevelToLoad.Next;

    [SerializeField] private bool saveOnLoad = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, triggerRadius, Player.Instance.GetLayerMask()))
        {
            if (levelToLoad == LevelToLoad.Next)
                LevelManager.Instance.LoadNextLevel();
            else if (levelToLoad == LevelToLoad.Previous)
                LevelManager.Instance.LoadPreviousLevel();

            if (saveOnLoad)
                LevelManager.Instance.Save();
        }
    }
}