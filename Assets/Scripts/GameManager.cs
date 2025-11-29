using System;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public TileBoard board;
    public BasePanel gameOverPanel;
    void OnEnable()
    {
        board.gameOverCall += GameOver;
    }

    void OnDisable()
    {
        board.gameOverCall -= GameOver;
    }

    void Start()
    {
        board.CreateTile();
        board.CreateTile();
    }

    public void NewGame()
    {
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOverPanel.OpenPanel();
    }
}