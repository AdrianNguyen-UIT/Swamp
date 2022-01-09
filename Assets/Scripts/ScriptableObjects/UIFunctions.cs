using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI Functions/Functions", fileName = "New UI Functions")]
public class UIFunctions : ScriptableObject
{
    [SerializeField] private InputReader inputReader = null;
    [SerializeField] private MenuEventChannel saveSlotMenuEventChannel = null;
    [SerializeField] private MenuEventChannel controlsMenuEventChannel = null;
    [SerializeField] private MenuEventChannel pauseMenuCanvasEventChannel = null;
    public void Play()
    {
        Debug.Log("Play");
        GameManager.Instance.PlayGameScene();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void NavSaveSlotMenu()
    {
        FindObjectOfType<MenuController>().NavToMenu(saveSlotMenuEventChannel);
    }

    public void NavControlsMenu()
    {
        FindObjectOfType<MenuController>().NavToMenu(controlsMenuEventChannel);
    }

    public void NavPreviousMenu()
    {
        FindObjectOfType<MenuController>().NavBack();
    }

    public void ChooseSaveSlot1()
    {
        GameManager.Instance.ConfirmSaveSlot(SaveSystem.SaveSlotIndex.Slot1);
    }

    public void ChooseSaveSlot2()
    {
        GameManager.Instance.ConfirmSaveSlot(SaveSystem.SaveSlotIndex.Slot2);
    }

    public void ChooseSaveSlot3()
    {
        GameManager.Instance.ConfirmSaveSlot(SaveSystem.SaveSlotIndex.Slot3);
    }

    public void Continue()
    {
        pauseMenuCanvasEventChannel.OnDismissed();
        inputReader.EnableGameplayInput();
    }

    public void RestartLevel()
    {
        GameManager.Instance.ReloadGameScene();
    }

    public void LoadTitleScreen()
    {
        GameManager.Instance.LoadTitlteScreenFromGame();
    }
}
