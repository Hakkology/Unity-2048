using TMPro;
using UnityEngine;

public class GameScorePanel : MonoBehaviour 
{
    public static GameScorePanel Instance { get; private set; }
    private int score;
    private int moveCount;
    private int mergeCount;

    private const string HighScoreKey = "HighScore";
    private const string HighMoveCountKey = "HighMoveCount";
    private const string HighMergeCountKey = "HighMergeCount";

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI moveText;
    [SerializeField] private TextMeshProUGUI mergeText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ResetAndDisplayScores()
    {
        score = 0;
        moveCount = 0;
        mergeCount = 0;
        
        UpdateScore(0);
        UpdateMoveCount(0);
        UpdateMergeCount(0);
    }

    public void UpdateScore(int newScore)
    {
        score += newScore;
        
        int highscore = GetHighScore(HighScoreKey);
        scoreText.text = $"{score.ToString()} / {highscore.ToString()}";
    }
    
    public void UpdateMoveCount(int moves = 1)
    {
        moveCount += moves;
        
        int highscore = GetHighScore(HighMoveCountKey);
        string highscoreDisplay = highscore == int.MaxValue ? "-" : highscore.ToString();
        moveText.text = $"{moveCount.ToString()} / {highscoreDisplay}";
    }
    
    public void UpdateMergeCount(int merges = 1)
    {
        mergeCount += merges;
        
        int highscore = GetHighScore(HighMergeCountKey);
        mergeText.text = $"{mergeCount.ToString()} / {highscore.ToString()}";
    }

    private int GetHighScore(string key)
    {
        if (key == HighMoveCountKey)
            return PlayerPrefs.GetInt(key, int.MaxValue);
        
        return PlayerPrefs.GetInt(key, 0);
    }

    public void SaveHighScores()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
        }

        int currentHighMove = PlayerPrefs.GetInt(HighMoveCountKey, int.MaxValue);
        if (moveCount < currentHighMove)
        {
            PlayerPrefs.SetInt(HighMoveCountKey, moveCount);
        }
        
        int currentHighMerge = PlayerPrefs.GetInt(HighMergeCountKey, 0);
        if (mergeCount > currentHighMerge)
        {
            PlayerPrefs.SetInt(HighMergeCountKey, mergeCount);
        }
        
        PlayerPrefs.Save(); 
    }
}