using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    public NeighbourChunk[] Neighbours4Chunks { get; set; }
    public int[][] Fields { get; set; }
    private Vector2Int fieldsGlobalBegin = Vector2Int.zero;


    public Tilemap tilemap { get; set; }
    public Biome TerrainBiome { get; set; }
    private bool isIndirectChunk;

    public ChunkEdge[] edges { get; set; }

    public static int chunkSize = 64;
    public static int chunkSizeHalf = (int)(64 * 0.5f);

    public Vector2 offsetTopography { get; set; }
    public Vector2 offsetMoisture { get; set; }
    public Vector2 offsetTemperature { get; set; }
    public Vector2 offsetRiver { get; set; }

    public float topographyScale = 4f;//
    public static float moistureScale =0.6f;
    public static float temperatureScale =0.5f;
    public float riverScale =0.2f;

    private bool NOTneededNW;
    private bool NOTneededSW;
    private bool NOTneededNE;
    private bool NOTneededSE;
    public bool NOTgenerationNeeded { get; set; }

    private void FieldInit()
    {
        Fields = new int[chunkSize][];
        for (int i = 0; i < chunkSize; i++)
        {
            Fields[i] = new int[chunkSize];
        }
    }
    private void edgesInit()
    {
        edges = new ChunkEdge[4];
        edges[(int)Direction.N] = new NorthEdge();
        edges[(int)Direction.S] = new SouthEdge();
        edges[(int)Direction.W] = new WestEdge();
        edges[(int)Direction.E] = new EastEdge();

    }
    private void NeighboursChunksInit()
    {
        Neighbours4Chunks = new NeighbourChunk[]{
            new NeighbourChunk(new Vector2(transform.position.x, transform.position.y+chunkSize), Direction.N,false),
            new NeighbourChunk(new Vector2(transform.position.x, transform.position.y-chunkSize), Direction.S,false),
            new NeighbourChunk(new Vector2(transform.position.x-chunkSize, transform.position.y), Direction.W,false),
            new NeighbourChunk(new Vector2(transform.position.x+chunkSize, transform.position.y), Direction.E,false)
                };
    }

    private void Awake()
    {
        FieldInit();
        NeighboursChunksInit();
        edgesInit();
        offsetTopography=offsetMoisture=offsetRiver=offsetTemperature=Vector2.zero;
        BoxCollider2D coll = gameObject.AddComponent<BoxCollider2D>();
        coll.size = new Vector2(chunkSize, chunkSize);
        coll.offset = new Vector2(1, 1);

        GameObject chunkGameObject = new GameObject();
        tilemap = chunkGameObject.AddComponent<Tilemap>();
        chunkGameObject.AddComponent<TilemapRenderer>();
        chunkGameObject.transform.SetParent(GameManager.instance.gridd.transform);
        fieldsGlobalBegin = new Vector2Int((int)transform.position.x + chunkSizeHalf, (int)transform.position.y + chunkSizeHalf);
    }


    public Vector2 checkDiagonallChunkNeeded( Dictionary<Vector3, Chunk> chunks)
    {
        if (!NOTneededNW && Neighbours4Chunks[(int)Direction.W].exist && Neighbours4Chunks[(int)Direction.N].exist)
        {
            NOTneededNW = true;
            return new Vector2(Neighbours4Chunks[(int)Direction.W].position.x, Neighbours4Chunks[(int)Direction.N].position.y);
        }
        if (!NOTneededSW && Neighbours4Chunks[(int)Direction.W].exist && Neighbours4Chunks[(int)Direction.S].exist)
        {
            NOTneededSW = true;
            return new Vector2(Neighbours4Chunks[(int)Direction.W].position.x, Neighbours4Chunks[(int)Direction.S].position.y);
        }
        if (!NOTneededNE && Neighbours4Chunks[(int)Direction.E].exist && Neighbours4Chunks[(int)Direction.N].exist)
        {
            NOTneededNE = true;
            return new Vector2(Neighbours4Chunks[(int)Direction.E].position.x, Neighbours4Chunks[(int)Direction.N].position.y);
        }
        if (!NOTneededSE && Neighbours4Chunks[(int)Direction.E].exist && Neighbours4Chunks[(int)Direction.S].exist)
        {
            NOTneededSE = true;
            return new Vector2(Neighbours4Chunks[(int)Direction.E].position.x, Neighbours4Chunks[(int)Direction.S].position.y);
        }
        if (NOTneededNE && NOTneededNW && NOTneededSE && NOTneededSW)
            NOTgenerationNeeded = true;
        return Vector2.zero;
    }

    public void UpdateOffset(float terrainScale)
    {
        float x = -transform.position.x / chunkSize;
        float y = -transform.position.y / chunkSize;
        offsetTopography = new Vector2(x * terrainScale,y * terrainScale);
        offsetMoisture = new Vector2(x*moistureScale, y*moistureScale);
        offsetTemperature = new Vector2(x*temperatureScale, y*temperatureScale);
        //offsetRiver = new Vector2(riverScale + originOffsetRiver.x, originOffsetRiver.y);
    }

    public int GetFieldNumber(Vector2 objectPosition)
    {
        try
        {
            return Fields[(int)Mathf.Round(Mathf.Abs(fieldsGlobalBegin.x - objectPosition.x))]
              [(int)Mathf.Round(Mathf.Abs(fieldsGlobalBegin.y - objectPosition.y))];
        }
        catch
        {
            Debug.Log(((int)Mathf.Round(Mathf.Abs(fieldsGlobalBegin.x - objectPosition.x))) + "  " +((int)Mathf.Round(Mathf.Abs(fieldsGlobalBegin.y - objectPosition.y))));
            //64 clamp
            return 0;
        }
    }

}
