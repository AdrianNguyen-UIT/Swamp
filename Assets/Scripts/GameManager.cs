using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.Events;

public enum SceneIndex
{
    TITLE = 1,
    GAME = 2
}

public class GameManager : Singleton<GameManager>
{
    #region Variable Fields
    //Configurations

    [SerializeField] private MenuButtonEventChannel saveSlotMenuButtonChannel = null;
    [SerializeField] private SaveSlotUIEventChannel[] saveSlotUIChannels = new SaveSlotUIEventChannel[SaveSystem.SaveSlotCount];
    [SerializeField] private Image vortex = null;
    [SerializeField] private Image blackImage = null;
    [SerializeField] private TweenUIConfig intro = null;
    [SerializeField] private TweenUIConfig outro = null;
    //Cached component references

    //States
    //Data storages
    private readonly string saveFixedName = "SAVE: SLOT ";
    #endregion

    #region Unity Methods
    private void Start()
    {
        SaveSystem.Initialize();

        DoIntro(LoadTitleScreen);
    }
    #endregion

    #region Private Methods

    #endregion

    #region Public Methods
    public void DoIntro(System.Action action)
    {
        action.Invoke();

        LeanTween
            .scale(vortex.rectTransform, intro.Scale, intro.Duration)
            .setIgnoreTimeScale(true);
        LeanTween
            .scale(blackImage.rectTransform, intro.Scale, intro.Duration)
            .setIgnoreTimeScale(true);
        LeanTween
            .rotateAround(vortex.rectTransform, Vector3.forward, intro.Rotate.z, intro.Duration)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                vortex.enabled = false;
                blackImage.enabled = false;
            });
    }

    public void DoOutro(System.Action action)
    {
        vortex.enabled = true;
        blackImage.enabled = true;
        LeanTween.scale(vortex.rectTransform, outro.Scale, outro.Duration).setIgnoreTimeScale(true);
        LeanTween.scale(blackImage.rectTransform, outro.Scale, outro.Duration).setIgnoreTimeScale(true);
        LeanTween.rotateAround(vortex.rectTransform, Vector3.forward, outro.Rotate.z, outro.Duration).setIgnoreTimeScale(true).setOnComplete(action);
    }

    public void PlayGameScene()
    {
        DoOutro(() => {
            SceneManager.UnloadSceneAsync((int)SceneIndex.TITLE);
            SceneManager.LoadSceneAsync((int)SceneIndex.GAME, LoadSceneMode.Additive);
        });
    }

    public void LoadTitlteScreenFromGame()
    {
        DoOutro(() => {
            Time.timeScale = 1.0f;
            SceneManager.UnloadSceneAsync((int)SceneIndex.GAME);
            DoIntro(LoadTitleScreen);
        });
    }

    public void ReloadGameScene()
    {
        DoOutro(() => {
            Time.timeScale = 1.0f;
            SceneManager.UnloadSceneAsync((int)SceneIndex.GAME);
            SceneManager.LoadSceneAsync((int)SceneIndex.GAME, LoadSceneMode.Additive);

        });
    }

    public void LoadTitleScreen()
    {
        saveSlotMenuButtonChannel.ButtonName = saveFixedName + ((int)SaveSystem.CurrentSaveSlot + 1).ToString();

        for (int index = 0; index < saveSlotUIChannels.Length; index++)
        {
            ReloadSaveSlotUI((SaveSystem.SaveSlotIndex)index);
        }

        SceneManager.LoadScene((int)SceneIndex.TITLE, LoadSceneMode.Additive);
    }

    private void ReloadSaveSlotUI(SaveSystem.SaveSlotIndex index)
    {
        SaveData saveData = SaveSystem.GetSaveData(index);
        if (saveData == null)
        {
            saveSlotUIChannels[(int)index].CurrentUsed = false;
            saveSlotUIChannels[(int)index].SaveDate = SaveData.DefaultSaveDate;
            saveSlotUIChannels[(int)index].HourPlay = SaveData.DefaultHourPlay;
        }
        else
        {
            saveSlotUIChannels[(int)index].CurrentUsed = saveData.CurrentUsed;
            saveSlotUIChannels[(int)index].SaveDate = saveData.SaveDate;
            saveSlotUIChannels[(int)index].HourPlay = saveData.HourPlay;
        }
    }

    public void ConfirmSaveSlot(SaveSystem.SaveSlotIndex slotIndex)
    {
        SaveSystem.ChooseSaveSlot(slotIndex);
        saveSlotMenuButtonChannel.ButtonName = saveFixedName + ((int)SaveSystem.CurrentSaveSlot + 1).ToString();
        
        for (int index = 0; index < saveSlotUIChannels.Length; index++)
        {
            ReloadSaveSlotUI((SaveSystem.SaveSlotIndex)index);
            saveSlotUIChannels[index].OnHotReload();
        }
    }
    #endregion
}
