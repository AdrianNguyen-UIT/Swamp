using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlotUI : MenuButton
{
    #region Variable Fields
    //Configurations
    [SerializeField] private TextMeshProUGUI saveDate = null;
    [SerializeField] private TextMeshProUGUI hourPlay = null;
    [SerializeField] private Image currentUsedCheck = null;
    //Cached component references

    //States

    //Data storages

    #endregion

    #region Unity Methods
    #endregion

    #region Private Methods
    protected override void Init()
    {
        pressed = false;
        buttonName.text = menuButtonEventChannel.ButtonName;

        SaveSlotUIEventChannel channel = (SaveSlotUIEventChannel)menuButtonEventChannel;
        if (channel.CurrentUsed)
        {
            transform.localScale = selectedConfig.Scale;
            box.color = selectedConfig.Color;
            pixels.gameObject.SetActive(true);
        }
        else
        {
            transform.localScale = deselectedConfig.Scale;
            box.color = deselectedConfig.Color;
            pixels.gameObject.SetActive(false);
        }

        OnHotReload();
    }

    protected override void OnHotReload()
    {
        Debug.Log("Hot Reload");

        SaveSlotUIEventChannel channel = (SaveSlotUIEventChannel)menuButtonEventChannel;
        saveDate.text = channel.SaveDate;
        hourPlay.text = channel.HourPlay.ToString() + " Hour(s)";
        currentUsedCheck.gameObject.SetActive(channel.CurrentUsed);
    }
    #endregion

    #region Public Methods
    #endregion
}
