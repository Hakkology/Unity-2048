using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameSettingsPanel : SettingsBasePanel
{

    public void OnBackButtonClicked()
    {
        GameGUIManager.Instance.OpenMenu();
    }
}
