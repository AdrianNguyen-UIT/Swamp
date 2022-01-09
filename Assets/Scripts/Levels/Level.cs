using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, ISaveable
{
    #region Variable Fields
    //Configurations
    public LevelConfig LevelConfig = null;

    [Header("Broadcast on LevelEventChannel")]
    [SerializeField] private LevelEventChannel levelEventChannel = null;

    //Cached component references
    private PolygonCollider2D cameraBound = null;
    public PolygonCollider2D CameraBound => cameraBound;
    //States

    //Data storages
    private List<InteractableObject> interactableObjects = new List<InteractableObject>();
    private List<string> destroyedInteractableObjectUIDs = new List<string>();
    private List<RevealSystem> revealSystems = new List<RevealSystem>();

    #endregion

    #region Unity Methods
    private void Awake()
    {
        cameraBound = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        levelEventChannel.OnInteractableObjectAdded += AddObject;
        levelEventChannel.OnInteractableObjectRemoved += RemoveObject;
        levelEventChannel.OnRevealSystemAdded += AddRevealSystem;
        levelEventChannel.OnLevelLoaded += Load;
        levelEventChannel.OnLevelUnloaded += Unload;
        levelEventChannel.OnLevelDataSaved += PopulateSaveData;
        levelEventChannel.OnLevelDataLoaded += LoadFromSaveData;
    }

    private void OnDisable()
    {
        levelEventChannel.OnInteractableObjectAdded -= AddObject;
        levelEventChannel.OnInteractableObjectRemoved -= RemoveObject;
        levelEventChannel.OnRevealSystemAdded -= AddRevealSystem;
        levelEventChannel.OnLevelLoaded -= Load;
        levelEventChannel.OnLevelUnloaded -= Unload;
        levelEventChannel.OnLevelDataSaved -= PopulateSaveData;
        levelEventChannel.OnLevelDataLoaded -= LoadFromSaveData;
    }

    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public void AddObject(InteractableObject _object)
    {
        interactableObjects.Add(_object);
    }

    public void RemoveObject(InteractableObject _object)
    {
        destroyedInteractableObjectUIDs.Add(_object.UID);
        interactableObjects.Remove(_object);
        Destroy(_object.gameObject);
    }

    public void AddRevealSystem(RevealSystem revealSystem)
    {
        revealSystems.Add(revealSystem);
    }

    public void Load()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        LevelManager.Instance.SetCameraBound(cameraBound);
    }

    public void Unload()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void PopulateSaveData(SaveData saveData)
    {
        saveData.DestroyInteractableObjectUIDs.AddRange(destroyedInteractableObjectUIDs);

        foreach (var obj in interactableObjects)
        {
            obj.PopulateSaveData(saveData);
        }

        foreach (var revealSystem in revealSystems)
        {
            revealSystem.PopulateSaveData(saveData);
        }
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        InteractableObject[] temp = new InteractableObject[interactableObjects.Count];
        interactableObjects.CopyTo(temp);
        foreach (var obj in temp)
        {
            obj.LoadFromSaveData(saveData);
        }

        foreach (var revealSystem in revealSystems)
        {
            revealSystem.LoadFromSaveData(saveData);
        }
    }
    #endregion
}
