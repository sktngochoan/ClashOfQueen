using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSettingMenu : MonoBehaviour
{
    public void OpenPauseMenu()
    {
        MenuManager.GoToMenu(MenuName.Pause);
    }
}
