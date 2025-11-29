using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel
{ 
    public GameManager gameManager;

    void Start() 
    {
        HidePanel();
    }

    public void OnRestartPressed()
    {
        SoundController.RequestSound(SoundID.ButtonClick);
        gameManager.NewGame();
        HidePanel();
    }

    public void OnExitPressed()
    {
        SoundController.RequestSound(SoundID.ButtonClick);
        SceneManager.LoadScene("MenuScene");
    }
}