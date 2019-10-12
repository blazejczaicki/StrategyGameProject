using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
//https://www.youtube.com/watch?v=xNqqfABXTNQ
public class TileAutomata : MonoBehaviour
{

    [SerializeField] [Range(0, 100)] private int initChanceToBeLive = 0;
    [SerializeField] [Range(1, 8)] private int birthLimit = 0;
    [SerializeField] [Range(1, 8)] private int deathLimit = 0;
    [SerializeField] [Range(0, 10)] private int repeateNumber = 0;
    private int countOfSaveFiles = 0;

    [SerializeField] int[][] terrainMap = null;
    [SerializeField] Vector3Int tileMapSize = Vector3Int.zero;

    [SerializeField] Tilemap topMap = null;
    [SerializeField] Tilemap botMap = null;
    [SerializeField] Tile topTile = null;
    [SerializeField] Tile botTile = null;

    private int tilemapHeight = 0;
    private int tilemapWidth = 0;

    public void Simulate()
    {
        clearMap(false);
        tilemapWidth = tileMapSize.x;
        tilemapHeight = tileMapSize.y;

        if (terrainMap == null)
        {
            InitPositions();
        }

        for (int i = 0; i < repeateNumber; i++)
        {
            terrainMap = GenerateTilesPosition(terrainMap);
        }
        //Debug.ClearDeveloperConsole();
        //for (int x = 0; x < tilemapWidth; x++)
        //{
        //    for (int y = 0; y < tilemapHeight; y++)
        //    {
        //        Debug.Log(terrainMap[x][y]);
        //    }
        //}


        for (int x = 0; x < tilemapWidth; x++)
        {
            for (int y = 0; y < tilemapHeight; y++)
            {
                if (terrainMap[x][y] == 1)
                {
                    topMap.SetTile(new Vector3Int((int)(-x + tilemapWidth * 0.5f), (int)(-y + tilemapHeight * 0.5f), 0), topTile);
                }
                botMap.SetTile(new Vector3Int((int)(-x + tilemapWidth * 0.5f), (int)(-y + tilemapHeight * 0.5f), 0), botTile);

            }
        }

    }

    public int[][] GenerateTilesPosition(int[][] oldMap)
    {
        int[][] newTerrainMap = new int[tilemapWidth][];
        int neighbour;
        BoundsInt boundBox = new BoundsInt(new Vector3Int(-1, -1, 0), new Vector3Int(3, 3, 1));

        for (int x = 0; x < tilemapWidth; x++)
        {
            newTerrainMap[x] = new int[tilemapHeight];
            for (int y = 0; y < tilemapHeight; y++)
            {
                neighbour = 0;
                foreach (var bound in boundBox.allPositionsWithin)
                {
                    if (bound.x == 0 && bound.y == 0) continue;
                    if (x + bound.x >= 0 && x + bound.x < tilemapWidth && y + bound.y >= 0 && y + bound.y < tilemapHeight)
                    {
                        neighbour += oldMap[x + bound.x][y + bound.y];
                    }
                    else
                    {
                        neighbour++;
                    }
                }

                if (oldMap[x][y] == 1)
                {
                    if (neighbour < deathLimit)
                        newTerrainMap[x][y] = 0;
                    else
                        newTerrainMap[x][y] = 1;
                }

                if (oldMap[x][y] == 0)
                {
                    if (neighbour > birthLimit)
                        newTerrainMap[x][y] = 1;
                    else
                        newTerrainMap[x][y] = 0;
                }
            }
        }
        return newTerrainMap;
    }

    public void InitPositions()
    {
        terrainMap = new int[tilemapWidth][];
        for (int x = 0; x < tilemapWidth; x++)
        {
            terrainMap[x] = new int[tilemapHeight];
            for (int y = 0; y < tilemapHeight; y++)
            {
                terrainMap[x][y] = Random.Range(1, 101) < initChanceToBeLive ? 1 : 0;
            }
        }

    }

    public void clearMap(bool complete)
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Simulate();
        }
        if (Input.GetMouseButtonDown(1))
        {
            clearMap(true);
        }
    }
}
