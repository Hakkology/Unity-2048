using UnityEngine;

public class GameEndPanel : BasePanel
{
    [SerializeField] private Transform statsContainer;
    [SerializeField] private GameObject statBlockPrefab;
    public void OnExitToMenuClicked()   => GameGUIManager.Instance.ReturnToMainMenu();
    public void OnReplayClicked()       => GameGUIManager.Instance.RestartGame();

    public override void OpenPanel()
    {
        base.OpenPanel();
        PopulateStats();
        SoundController.RequestSound(SoundID.GameOver);
    }

    private void PopulateStats()
    {
        foreach (Transform child in statsContainer.transform)
            Destroy(child.gameObject);
    }
}
