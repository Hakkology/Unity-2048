using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuPanel : MonoBehaviour
{
    [SerializeField] private BasePanel menuPanelUI;
    [SerializeField] private GameObject menuButton;

    void Start() 
    {
        OnContinueClicked();
    }

    public void OnMenuClicked()
    {
        SoundController.RequestSound(SoundID.ButtonClick);
        menuPanelUI.OpenPanel();
        menuButton.SetActive(false);
    }

    public void OnContinueClicked()
    {
        SoundController.RequestSound(SoundID.ButtonClick);
        menuPanelUI.ClosePanel();
        menuButton.SetActive(true);
    }

    // public void OnSettingsClicked()
    // {
    //     GameGUIManager.Instance.OpenSettings();
    // }

    public void OnExitMenuClicked()
    {
        SoundController.RequestSound(SoundID.ButtonClick);
        SceneManager.LoadScene("MainMenu");
    }

//     public void OnExitGameClicked()
//     {
//         Application.Quit();
//     }
}
