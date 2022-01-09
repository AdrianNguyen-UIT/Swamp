using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    #region Variable Fields
    //Configurations
    [SerializeField] private MenuEventChannel[] menuEventChannels = null;
    [SerializeField] private MenuEventChannel defaultMenu = null;
    //Cached component references

    //States
    private bool firstUpdate = false;
    //Data storages
    private Stack<int> menuStack = new Stack<int>();
    #endregion

    #region Unity Methods

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        menuStack.Clear();
    }
    #endregion

    #region Private Methods
    private void Update()
    {
        if (!firstUpdate)
        {
            NavToMenu(defaultMenu);
            firstUpdate = true;
        }
    }
    #endregion

    #region Public Methods

    public void NavToMenu(MenuEventChannel menuEventChannel)
    {
        for (int index = 0; index < menuEventChannels.Length; index++)
        {
            if (menuEventChannel == menuEventChannels[index])
            {
                if (menuStack.Count != 0)
                    menuEventChannels[menuStack.Peek()].OnDismissed();
                menuStack.Push(index);
                menuEventChannel.OnAppear();
                break;
            }
        }
    }

    public void NavBack()
    {
        menuEventChannels[menuStack.Pop()].OnDismissed();
        menuEventChannels[menuStack.Peek()].OnAppear();
    }

    #endregion
}
