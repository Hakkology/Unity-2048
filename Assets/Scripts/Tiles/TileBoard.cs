using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour {

    public Tile tilePrefab;
    public TileState[] tileStates;


    TileGrid grid;
    private List<Tile> tiles = new List<Tile>();

    bool waiting = false;

    void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
    }

    void Start()
    {
        CreateTile();
        CreateTile();
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

        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY)
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied)
                {
                    changed |= MoveTile(cell.tile, direction);
                    // changed bir tanesi bile true olduğunda direk true olarak dönecektir.
                }
            }
        }

        if (changed)
            StartCoroutine(WaitForChanges());
    }

    bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if (CanMergeTile(tile, adjacent.tile))
                {
                    MergeTile(tile, adjacent.tile);
                    return true;
                }
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    bool CanMergeTile(Tile a, Tile b)
    {
        return a.number == b.number && !b.locked;
    }

    void MergeTile(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length -1);
        int number = b.number * 2;
        b.SetState(tileStates[index], number);
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

    void CreateTile()
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

        // TODO CHECK FOR GAME OVER
    }
}