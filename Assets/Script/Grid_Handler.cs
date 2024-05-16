using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GridData
{
    public TileProperty[][] TerrainGrid { get; set; }
}

public class TileProperty
{
    public int TileType { get; set; }
}

public class Grid_Handler : MonoBehaviour
{
    public Tiles TilePrefab;
    public Sprite[] env;

    private GridData terrainGrid;
    private int rowCount;
    private int columnCount;

    void Start()
    {
        LoadGridData();
        ProcessTerrainGrid();
        GenerateGrid();
    }

    private void LoadGridData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("data");
        terrainGrid = JsonConvert.DeserializeObject<GridData>(jsonFile.text);
    }

    private void ProcessTerrainGrid()
    {
        if (terrainGrid == null || terrainGrid.TerrainGrid == null)
        {
            Debug.LogError("Failed to load terrain grid data.");
            return;
        }

        rowCount = terrainGrid.TerrainGrid.Length;
        columnCount = rowCount > 0 ? terrainGrid.TerrainGrid[0].Length : 0;

        if (rowCount == 0 || columnCount == 0)
        {
            Debug.LogError("Terrain grid is empty.");
            return;
        }

        Debug.Log($"Rows: {rowCount}, Columns: {columnCount}");

        foreach (var row in terrainGrid.TerrainGrid)
        {
            foreach (var tile in row)
            {
                Debug.Log($"Tile Type at ({row}, {tile}): {tile.TileType}");
            }
        }
    }


    private void GenerateGrid()
    {
        if (TilePrefab == null)
        {
            Debug.LogError("TilePrefab is not assigned.");
            return;
        }

        Tiles[,] grid = new Tiles[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                var tile = Instantiate(TilePrefab, new Vector2(i, j), Quaternion.identity);
                grid[i, j] = tile;

                int tileType = terrainGrid.TerrainGrid[i][j].TileType;
                SetTileSprite(tile, tileType);
                SetTileTypeComponent(tile, tileType);
                tile.transform.parent = transform;
            }
        }
    }

    private void SetTileSprite(Tiles tile, int tileType)
    {
        if (tileType < env.Length && tileType >= 0)
        {
            tile.GetComponent<SpriteRenderer>().sprite = env[tileType];
        }
        else
        {
            Debug.LogError("Invalid tile type index: " + tileType);
        }
    }

    private void SetTileTypeComponent(Tiles tile, int tileType)
    {
        tile.GetComponent<Tiles>().TileType = tileType;
    }

}
