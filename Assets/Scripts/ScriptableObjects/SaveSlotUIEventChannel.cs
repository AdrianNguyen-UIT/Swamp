using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/SaveSlotUIEvent Channel", fileName = "New SaveSlotUIEventChannel")]
public class SaveSlotUIEventChannel : MenuButtonEventChannel
{
    public string SaveDate = string.Empty;
    public float HourPlay = 0.0f;
    public bool CurrentUsed = false;
    public SaveSystem.SaveSlotIndex SaveSlotIndex = SaveSystem.SaveSlotIndex.Slot1;
}
