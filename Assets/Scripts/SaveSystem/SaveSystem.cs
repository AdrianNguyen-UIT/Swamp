using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class SaveSystem
{
    public enum SaveSlotIndex
    {
        Slot1 = 0,
        Slot2 = 1,
        Slot3 = 2
    }
    public readonly static int SaveSlotCount = 3;
    private static SaveSlotIndex currentSaveSlot = SaveSlotIndex.Slot1;
    public static SaveSlotIndex CurrentSaveSlot => currentSaveSlot;
    private readonly static string[] SaveFileNames = { "SaveData00.dat", "SaveData01.dat", "SaveData02.dat" };
    private static SaveData[] saveDatas = new SaveData[SaveSlotCount];

    public static void Initialize()
    {
        for (int index = 0; index < SaveSlotCount; index++)
        {
            saveDatas[index] = LoadJSonData(SaveFileNames[index]);
            if (saveDatas[index] != null && saveDatas[index].CurrentUsed)
            {
                currentSaveSlot = (SaveSlotIndex)index;
            }
        }

        if (IsEmpty())
            InitCurrentSaveData();
    }

    #region Public Methods
    public static bool SaveJSonData()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        saveDatas[(int)currentSaveSlot].SortData();
        if (FileManager.WriteToFile(SaveFileNames[(int)currentSaveSlot],
            saveDatas[(int)currentSaveSlot].ToJSon()))
        {
            stopwatch.Stop();
            UnityEngine.Debug.Log("Save successful. Time elapsed: " + stopwatch.ElapsedMilliseconds + "ms");
            return true;
        }
        stopwatch.Stop();
        return false;
    }

    public static bool LoadJSonData()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        if (FileManager.LoadFromFile(SaveFileNames[(int)currentSaveSlot], out var json))
        {
            saveDatas[(int)currentSaveSlot].FromJson(json);
            stopwatch.Stop();
            UnityEngine.Debug.Log("Load complete. Time elapsed: " + stopwatch.ElapsedMilliseconds + "ms");
            return true;
        }
        return false;
    }

    private static SaveData LoadJSonData(string saveFileName)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        if (FileManager.LoadFromFile(saveFileName, out var json))
        {
            SaveData saveData = new SaveData();
            saveData.FromJson(json);
            stopwatch.Stop();
            UnityEngine.Debug.Log("Load complete. Time elapsed: " + stopwatch.ElapsedMilliseconds + "ms");
            return saveData;
        }
        return null;
    }

    public static void ChooseSaveSlot(SaveSlotIndex saveSlot)
    {
        if (currentSaveSlot == saveSlot)
            return;

        saveDatas[(int)currentSaveSlot].CurrentUsed = false;
        currentSaveSlot = saveSlot;

        if (saveDatas[(int)currentSaveSlot] == null)
            InitCurrentSaveData();
        else
            saveDatas[(int)currentSaveSlot].CurrentUsed = true;

    }

    public static SaveData GetCurrentSaveData()
    {
        return saveDatas[(int)currentSaveSlot];
    }

    public static void InitCurrentSaveData()
    {
        saveDatas[(int)currentSaveSlot] = new SaveData
        {
            CurrentUsed = true,
            SaveDate = SaveData.DefaultSaveDate,
            HourPlay = SaveData.DefaultHourPlay
        };
    }

    public static SaveData GetSaveData(SaveSlotIndex index)
    {
        return saveDatas[(int)index];
    }

    private static bool IsEmpty()
    {
        foreach (var saveData in saveDatas)
        {
            if (saveData != null)
                return false;
        }
        return true;
    }
    #endregion

}