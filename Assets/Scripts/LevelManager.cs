using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LevelManager : Singleton<LevelManager>, ISaveable
{
    #region Variable Fields
    //Configurations

    [SerializeField] private LevelConfig defaultLevel = null;
    //Cached component references
    private CinemachineConfiner confiner = null;
    //States
    private bool firstUpdate = false;
    //Data storages
    public LevelEventChannel[] levelEventChannels;
    private LevelConfig currentLevel;
    public string CurrentLevelUID => currentLevel.UID;
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!firstUpdate)
        {
            if (SaveSystem.GetCurrentSaveData().IsNew())
            {
                InitNewLevels();
            }
            else
            {
                InitSavedLevels();
            }
            GameManager.Instance.DoIntro(() => { });
            firstUpdate = true;
        }
    }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public void InitNewLevels()
    {
        LoadLevel(defaultLevel.UID);
        Player.Instance.transform.position = currentLevel.StartingPos;
    }

    public void InitSavedLevels()
    {
        LoadFromSaveData(SaveSystem.GetCurrentSaveData());
        LoadLevel(CurrentLevelUID);
    }

    public void LoadNextLevel()
    {
        LoadLevel(currentLevel.NextLevel.UID);
    }

    public void LoadPreviousLevel()
    {
        LoadLevel(currentLevel.PreviousLevel.UID);
    }

    public void LoadLevel(string uid)
    {
        foreach (var levelChannel in levelEventChannels)
        {
            if (levelChannel.MainLevel.UID == uid)
            {
                currentLevel = levelChannel.MainLevel;
                levelChannel.LoadLevel();
            }
            else
                levelChannel.UnloadLevel();
        }
    }

    public void PopulateSaveData(SaveData saveData)
    {
        saveData.CurrentLevelUID = CurrentLevelUID;

        foreach (var levelChannel in levelEventChannels)
        {
            levelChannel.SaveLevelData(saveData);
        }

        AbilitySystem.Instance.PopulateSaveData(saveData);
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        foreach (var levelChannel in levelEventChannels)
        {
            if (levelChannel.MainLevel.UID == saveData.CurrentLevelUID)
                currentLevel = levelChannel.MainLevel;
            levelChannel.LoadLevelData(saveData);
        }
        Player.Instance.transform.position = currentLevel.StartingPos;
        AbilitySystem.Instance.LoadFromSaveData(saveData);
    }

    public void Save()
    {
        SaveSystem.GetCurrentSaveData().Clear();
        PopulateSaveData(SaveSystem.GetCurrentSaveData());
        SaveSystem.SaveJSonData();
    }

    public void SetCameraBound(PolygonCollider2D collider2D)
    {
        confiner.m_BoundingShape2D = collider2D;
    }
    #endregion
}
