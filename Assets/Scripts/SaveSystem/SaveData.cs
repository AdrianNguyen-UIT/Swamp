using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Serializable]
    public class InteractableObjectData : IComparable<InteractableObjectData>
    {
        public string UID;
        public Vector3 Position;
        public Quaternion Rotation;

        public int CompareTo(InteractableObjectData other)
        {
            return UID.CompareTo(other.UID);
        }
    }

    [Serializable]
    public class RevealSystemData : IComparable<RevealSystemData>
    {
        public string UID;
        public bool Revealed;

        public int CompareTo(RevealSystemData other)
        {
            return UID.CompareTo(other.UID);
        }
    }

    public bool CurrentUsed;
    public string CurrentLevelUID;
    public List<InteractableObjectData> InteractableObjects = new List<InteractableObjectData>();
    public List<string> DestroyInteractableObjectUIDs = new List<string>();
    public List<RevealSystemData> RevealSystems = new List<RevealSystemData>();
    public List<string> CurrentAbilities = new List<string>();
    public string SaveDate;
    public float HourPlay;

    public string ToJSon()
    {
        return JsonUtility.ToJson(this, true);
    }

    public void FromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    public void Clear()
    {
        CurrentUsed = false;
        CurrentLevelUID = string.Empty;
        InteractableObjects.Clear();
        DestroyInteractableObjectUIDs.Clear();
        RevealSystems.Clear();
        CurrentAbilities.Clear();
    }

    public void SortData()
    {
        InteractableObjects.Sort();
        DestroyInteractableObjectUIDs.Sort();
        RevealSystems.Sort();
        CurrentAbilities.Sort();
    }

    public bool IsNew()
    {
        return CurrentLevelUID == null;
    }

    public static bool DefaultCurrentUsed = false;
    public static string DefaultSaveDate = "Empty";
    public static float DefaultHourPlay = 0f;
}

public interface ISaveable
{
    void PopulateSaveData(SaveData saveData);
    void LoadFromSaveData(SaveData saveData);
}