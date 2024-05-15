using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UIElements;
public class GridInfo
{
    public TilesProperty[][] TerrainGrid { get; set; }
}
public class TilesProperty
{
    public int TileType { get; set; }
}
public class Grid_Handler : MonoBehaviour
{
    public Tiles gridTilePrefab;
    public GameObject horizontalGrid, verticalGrid;
    GridInfo gridData;
    public Sprite[] environmentSprites;
    int rowCount;
    int columnCount;


    //public GridTile[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        LoadTerrainData();
    }

    void LoadTerrainData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("terrainData");

        if (jsonData == null)
        {
            LogError("Failed to load JSON.");
            return;
        }

        // Deserialize JSON into TerrainData object
        TerrainData terrainData = DeserializeTerrainData(jsonData);

        //if (terrainData == null || terrainData.Grid == null)
        //{
        //    LogError("Failed to parse JSON data.");
        //    return;
        //}

        //int rowCount = terrainData.Grid.Length;
        //int columnCount = terrainData.Grid[0].Length;

        LogTerrainDimensions(rowCount, columnCount);

        PrintTileTypes(terrainData, rowCount, columnCount);
    }

    void LogError(string message)
    {
        Debug.LogError(message);
    }

    TerrainData DeserializeTerrainData(TextAsset jsonData)
    {
        return JsonConvert.DeserializeObject<TerrainData>(jsonData.text);
    }

    void LogTerrainDimensions(int rowCount, int columnCount)
    {
        Debug.Log("Rows: " + rowCount + ", Columns: " + columnCount);
    }

    void PrintTileTypes(TerrainData terrainData, int rowCount, int columnCount)
    {
        // Print and save the tile type of each block
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                //int tileType = terrainData.Grid[i][j].TileType;
                //Debug.Log("Tile Type at (" + i + ", " + j + "): " + tileType);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
