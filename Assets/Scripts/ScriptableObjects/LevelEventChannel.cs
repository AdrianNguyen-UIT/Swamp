using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/LevelEvent Channel", fileName = "new LevelEventChannel")]
public class LevelEventChannel : ScriptableObject
{
    [SerializeField] private LevelConfig mainLevel = null;
    public LevelConfig MainLevel => mainLevel;

    public UnityAction<InteractableObject> OnInteractableObjectAdded;
    public UnityAction<InteractableObject> OnInteractableObjectRemoved;
    public UnityAction<RevealSystem> OnRevealSystemAdded;
    public UnityAction OnLevelLoaded;
    public UnityAction OnLevelUnloaded;
    public UnityAction<SaveData> OnLevelDataSaved;
    public UnityAction<SaveData> OnLevelDataLoaded;

    public void AddInteractableObject(InteractableObject interactableObject)
    {
        if (OnInteractableObjectAdded != null)
            OnInteractableObjectAdded.Invoke(interactableObject);
        else
            Debug.LogWarning("Interactble Object adding requested, but no Level picked it up!. " +
                "Make sure MainLevel is set");
    }

    public void RemoveInteractableObject(InteractableObject interactableObject)
    {
        if (OnInteractableObjectRemoved != null)
            OnInteractableObjectRemoved.Invoke(interactableObject);
        else
            Debug.LogWarning("Interactble Object removing requested, but no Level picked it up!" +
                "Make sure MainLevel is set");
    }

    public void AddRevealSystem(RevealSystem revealSystem)
    {
        if (OnRevealSystemAdded != null)
            OnRevealSystemAdded.Invoke(revealSystem);
        else
            Debug.LogWarning("Reveal System adding requested, but no Level picked it up!. " +
                "Make sure MainLevel is set");
    }

    public void LoadLevel()
    {
        if (OnLevelLoaded != null)
            OnLevelLoaded.Invoke();
        else
            Debug.LogWarning("Someone requested to Load Level, but no Level picked it up! " +
                "Make sure MainLevel is set!");
    }

    public void UnloadLevel()
    {
        if (OnLevelUnloaded != null)
            OnLevelUnloaded.Invoke();
        else
            Debug.LogWarning("Someone requested to Unload Level, but no Level picked it up! " +
                "Make sure MainLevel is set!");
    }

    public void SaveLevelData(SaveData saveData)
    {
        if (OnLevelDataSaved != null)
            OnLevelDataSaved.Invoke(saveData);
        else
            Debug.LogWarning("Someone requested to Save Level Data, but no Level picked it up! " +
                "Make sure MainLevel is set!");
    }

    public void LoadLevelData(SaveData saveData)
    {
        if (OnLevelDataLoaded != null)
            OnLevelDataLoaded.Invoke(saveData);
        else
            Debug.LogWarning("Someone requested to Load Level Data, but no Level picked it up! " +
                "Make sure MainLevel is set!");
    }
}
