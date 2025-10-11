using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour {

    public Tile tilePrefab;
    public TileState[] tileStates;

    
    TileGrid grid;
    private List<Tile> tiles = new();

    void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
    }

    void Start()
    {
        CreateTile();
        CreateTile();
    }
    
    void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
    }
}