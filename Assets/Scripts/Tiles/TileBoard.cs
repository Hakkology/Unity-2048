using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour {

    public Tile tilePrefab;
    public TileState[] tileStates;
    public Action gameOverCall;


    TileGrid grid;
    private List<Tile> tiles = new List<Tile>();

    bool waiting = false;

    void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
    }

    void Update()
    {
        if (waiting) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTiles(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveTiles(Vector2Int.down, 0, 1, grid.height-2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTiles(Vector2Int.left, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTiles(Vector2Int.right, grid.width -2, -1, 0, 1);
        }
    }

    void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;
        int maxMergedNumber = 0;

        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied)
                {
                    int moveResult = MoveTile(cell.tile, direction);
                    if (moveResult > 0)
                    {
                        changed = true;
                        if (moveResult > 1) 
                        {
                            if (moveResult > maxMergedNumber)
                            {
                                maxMergedNumber = moveResult;
                            }
                        }
                    }
                }
            }
        }

        if (changed)
        {
            if (maxMergedNumber > 0)
            {
                SoundID sound = maxMergedNumber < 128 ? SoundID.ScoreUpLow : SoundID.ScoreUpHigh;
                SoundController.RequestSound(sound);
            }
            StartCoroutine(WaitForChanges());
        }
    }

    int MoveTile(Tile tile, Vector2Int direction) 
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);
        GameScorePanel.Instance.UpdateMoveCount();
        
        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if (CanMergeTile(tile, adjacent.tile))
                {
                    // Birleştirme oldu, yeni sayıyı döndür
                    int mergedNumber = MergeTile(tile, adjacent.tile);
                    GameScorePanel.Instance.UpdateMergeCount();
                    return mergedNumber; 
                }
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return 1; // Kaydırma oldu
        }

        return 0; // Hareket yok
    }

    bool CanMergeTile(Tile a, Tile b)
    {
        return a.number == b.number && !b.locked;
    }

    int MergeTile(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length -1);
        int number = b.number * 2;
        b.SetState(tileStates[index], number);
        GameScorePanel.Instance.UpdateScore(number);
        return b.number ;
    }

    int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i])
            {
                return i;
            }
        }

        return -1;
    }
    
    public void ClearBoard()
    {
        foreach (var cell in grid.cells)
        {
            cell.tile = null;
        }
        
        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }

    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    IEnumerator WaitForChanges()
    {
        waiting = true;
        yield return new WaitForSeconds(.15f);
        waiting = false;

        foreach (var tile in tiles)
        {
            tile.locked = false;
        }

        if (tiles.Count != grid.size)
        {
            CreateTile();
        }

        if (CheckForGameOver())
        {
            gameOverCall?.Invoke();
        }
    }

    bool CheckForGameOver()
    {
        if (tiles.Count != grid.size)
        {
            return false;
        }

        foreach (var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);

            if (up?.tile != null && CanMergeTile(tile, up.tile)) return false;
            if (down?.tile != null && CanMergeTile(tile, down.tile)) return false;
            if (left?.tile != null && CanMergeTile(tile, left.tile)) return false;
            if (right?.tile != null && CanMergeTile(tile, right.tile)) return false;
        }

        GameScorePanel.Instance.SaveHighScores();
        GameScorePanel.Instance.ResetAndDisplayScores();
        SoundController.RequestSound(SoundID.GameOver);

        return true;
    }
}