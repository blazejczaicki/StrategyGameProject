using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Jobs;
//using UnityEngine.Jobs;
//using Unity.Collections;
//using Unity.Burst;
using System;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private StaticObjectSpawn staticObjectGenerator;
    [SerializeField] private EnemySpawn enemySpawn;
    [SerializeField] private Moisture tileContainer; //3d list moisture/temperatures/topologies

    private static float topographySeed = 554788;
    private static float moistureSeed = 464788;
    private static float temperatureSeed = 664788;
    //private static float riverSeed = 774788;

    //private float RiverNoise(float x, float y, float scale, float seed, Chunk chunk)
    //{
    //    float river = Mathf.PerlinNoise((topographySeed + x) / Chunk.chunkSize * 0.2f + chunk.offsetRiver.x,
    //(topographySeed + y) / Chunk.chunkSize * 0.2f + chunk.offsetRiver.y);
    //    river = river * 2 - 1;
    //    river = river + river * 0.8f;
    //    river = Mathf.Abs(river) - 1;
    //    return (river <= -0.95f) ? river : 0;
    //}

    public void CheckMembershipToBiome(Chunk chunk)
    {
        chunk.TerrainBiome = NearnestNeighbourBiome.Calculate(chunk.transform.position);
    }

    public void CheckNeighbourBiome(Chunk chunk, Biome[] neighboursBiomes, Dictionary<Vector3, Chunk> visibleChunks)
    {
        Chunk neighbourChunk;
        for (int i = 0; i < chunk.Neighbours4Chunks.Length; i++)
        {
            if (visibleChunks.TryGetValue(chunk.Neighbours4Chunks[i].position, out neighbourChunk))
            {
                    neighboursBiomes[i] = chunk.TerrainBiome;
            }
            else
            {
                neighboursBiomes[i] = NearnestNeighbourBiome.Calculate(chunk.Neighbours4Chunks[i].position);
            }
        }
    }

    public bool CheckNeighbourScaleTheSame(Chunk chunk, Dictionary<Vector3, Chunk> visibleChunks)
    {
        Chunk neighbourChunk;
        Biome biome;
        for (int i = 0; i < chunk.Neighbours4Chunks.Length; i++)
        {
            visibleChunks.TryGetValue(chunk.Neighbours4Chunks[i].position, out neighbourChunk);
            if (neighbourChunk != null)
            {
                biome = neighbourChunk.TerrainBiome;
            }
            else
            {
                biome = NearnestNeighbourBiome.Calculate(chunk.Neighbours4Chunks[i].position);
            }
            if (biome.topographyScaleOffset != chunk.TerrainBiome.topographyScaleOffset)
                return false;
        }
        return true;
    }

    public void GenerateTiles(Chunk chunk, Dictionary<Vector3, Chunk> visibleChunks)
    {
            CheckMembershipToBiome(chunk);
            chunk.topographyScale = chunk.TerrainBiome.topographyScaleOffset;
        if (chunk.transform.position == Vector3.zero)
        {
            StartCoroutine(GenerateTilesForNormalChunks( chunk));
        }
        else
        {
            Biome[] neighboursBiomes = new Biome[4];
            CheckNeighbourBiome(chunk, neighboursBiomes, visibleChunks);
            chunk.UpdateOffset(chunk.TerrainBiome.topographyScaleOffset);
            if (CheckNeighbourScaleTheSame(chunk, visibleChunks))
            {
                //StartCoroutine(jobjob(chunk));
                StartCoroutine(GenerateTilesForNormalChunks(chunk));
            }
            else
                StartCoroutine(GenerateTilesForIndirectChunks(chunk, neighboursBiomes, visibleChunks));
        }
    }

    private void CalculateTile(int x, int y, Chunk chunk, float heightCell, Biome biome)
    {
        float  moistureCell, temperatureCell;
        moistureCell = PerlinNoise.Calculate(x, y, Chunk.moistureScale, moistureSeed, chunk.offsetMoisture, Chunk.chunkSize);
        temperatureCell = PerlinNoise.Calculate(x, y, Chunk.temperatureScale, temperatureSeed, chunk.offsetTemperature, Chunk.chunkSize);
        Field field = ChooseFieldType(biome, heightCell, moistureCell, temperatureCell);
        chunk.Fields[x][y] = field.ID;
        chunk.tilemap.SetTile(new Vector3Int((int)(-x + chunk.transform.position.x + Chunk.chunkSizeHalf),
            (int)(-y + chunk.transform.position.y + Chunk.chunkSizeHalf), 0), field.TileRepresentation);
    }

    private IEnumerator GenerateTilesForNormalChunks( Chunk chunk)
    {
        SaveEdges(chunk);
        int breakGeneration = (int)(Chunk.chunkSize * 0.1f);
        float heightCell, moistureCell, temperatureCell;
        Vector3Int[] positionArray = new Vector3Int[Chunk.chunkSize* Chunk.chunkSize];
        TileBase[] tileArray = new TileBase[Chunk.chunkSize* Chunk.chunkSize];
        for (int x = 0; x < Chunk.chunkSize; x++)
        {
            for (int y = 0; y < Chunk.chunkSize; y++)
            {
                heightCell = PerlinNoise.Calculate(x, y, chunk.topographyScale, topographySeed, chunk.offsetTopography, Chunk.chunkSize);
                moistureCell = PerlinNoise.Calculate(x, y, Chunk.moistureScale, moistureSeed, chunk.offsetMoisture, Chunk.chunkSize);
                temperatureCell = PerlinNoise.Calculate(x, y, Chunk.temperatureScale, temperatureSeed, chunk.offsetTemperature, Chunk.chunkSize);
                Field field = ChooseFieldType(chunk.TerrainBiome, heightCell, moistureCell, temperatureCell);
                chunk.Fields[x][y] = field.ID;
                positionArray[x * Chunk.chunkSize + y] = new Vector3Int((int)(-x + chunk.transform.position.x + Chunk.chunkSizeHalf),
                (int)(-y + chunk.transform.position.y + Chunk.chunkSizeHalf), 0);
                tileArray[x * Chunk.chunkSize + y] = field.TileRepresentation;
            }
        }
        chunk.tilemap.SetTiles(positionArray, tileArray);
        yield return StartCoroutine(staticObjectGenerator.Generate(chunk));
        enemySpawn.SpawnOnChunk(chunk);
    }
    
    private void CalculateInterpolationPeak(ref float centerOfInterpolation, ref Vector2Int coordsOfCenter)
    {
        centerOfInterpolation = UnityEngine.Random.Range(0.4f, 0.6f);
        coordsOfCenter = new Vector2Int(UnityEngine.Random.Range((int)(Chunk.chunkSize - Chunk.chunkSize * 0.6f), (int)(Chunk.chunkSize - Chunk.chunkSize * 0.3f)),
            UnityEngine.Random.Range((int)(Chunk.chunkSize - Chunk.chunkSize * 0.6f), (int)(Chunk.chunkSize - Chunk.chunkSize * 0.3f)));
    }

    private ChunkEdge[] InitEdges()
    {
        ChunkEdge[] edges = new ChunkEdge[4];
        edges[(int)Direction.N] = new NorthEdge();
        edges[(int)Direction.S] = new SouthEdge();
        edges[(int)Direction.W] = new WestEdge();
        edges[(int)Direction.E] = new EastEdge();
        return edges;
    }

    private IEnumerator GenerateTilesForIndirectChunks(Chunk chunk, Biome[] neighboursBiomes, Dictionary<Vector3, Chunk> visibleChunks)
    {
        Vector2Int xy = Vector2Int.zero;
        ChunkEdge[] edges = InitEdges();
        DesignateInterpolationNodes(chunk, edges, visibleChunks);
        SaveEdgesIndirectChunk(chunk, edges);

        int breakGeneration = (int)(Chunk.chunkSize * 0.1f);
        float heightCell, centerOfInterpolation=0;
        Biome currentBiome = neighboursBiomes[0];
        Vector2Int coordsOfCenter=Vector2Int.zero;
        CalculateInterpolationPeak(ref centerOfInterpolation, ref coordsOfCenter);
        for (int x = 0; x < Chunk.chunkSize; x++)
        {
            for (int y = 0; y < Chunk.chunkSize; y++)
            {
                xy.x = x;
                xy.y = y;
                heightCell = ChunkInterpolation.Calculate(xy,//float Q11, float Q12, float Q21, float Q22
                    edges[(int)Direction.N].edge[x], edges[(int)Direction.W].edge[y], edges[(int)Direction.E].edge[y], edges[(int)Direction.S].edge[x], 
                    centerOfInterpolation, coordsOfCenter, ref currentBiome, neighboursBiomes);
                CalculateTile(x, y, chunk, heightCell, currentBiome);
            }
            if (x == breakGeneration)
            {
                breakGeneration += breakGeneration;
                yield return null;
            }
        }
        heightCell = centerOfInterpolation;        
        CalculateTile(coordsOfCenter.x, coordsOfCenter.y, chunk, heightCell, currentBiome);
        StartCoroutine(staticObjectGenerator.Generate(chunk));
    }
    
    private void SaveEdgesIndirectChunk(Chunk chunk, ChunkEdge[] edges)
    {
        chunk.edges = edges;
    }

    private void DesignateInterpolationNodes(Chunk chunk,   ChunkEdge[] edges, Dictionary<Vector3, Chunk> visibleChunks)
    {
        Chunk neighbourChunk;
        foreach (var neighbour in chunk.Neighbours4Chunks)
        {
            if (visibleChunks.TryGetValue(neighbour.position, out neighbourChunk))
            {
                edges[(int)neighbour.direction] = neighbourChunk.edges[(int)neighbour.opposedDirection];
            }
            else
            {
                edges[(int)neighbour.direction].CalculateEdge(chunk, topographySeed, neighbour.position);
            }
        }

    }

    private void SaveEdges(Chunk chunk)
    {
        foreach (var edge in chunk.edges)
        {
            edge.SaveEdge(chunk, topographySeed);
        }
    }
    
    private Field ChooseFieldType(Biome biome, float height, float moisture, float temperature)
    {
        return tileContainer.GetTemperatureForMoisture(biome, moisture).GetTopologyForTemperature(biome, temperature).GetFieldForTopology(biome, height);
    }

    //private float DistortionNoise(float x, float y, float scale, float seed, Vector2 offset)
    //{
    //    float xDistortion = 20.0f * PerlinNoise.Calculate((x + 2.3f) * 4.7f, (y + 2.9f) * 4.7f, scale, seed, offset, Chunk.chunkSize);
    //    float yDistortion = 20.0f * PerlinNoise.Calculate((x - 3.1f) * 4.7f, (y - 4.3f) * 4.7f, scale, seed, offset, Chunk.chunkSize);
    //    return PerlinNoise.Calculate(x + xDistortion, y + yDistortion, scale, seed, offset, Chunk.chunkSize);
    //}



    // jako część badawcza
    #region jobsy 
    //private NativeArray<int> input;
    //private NativeArray<float> outputHeight;
    //private NativeArray<float> outputTemperature;
    //private NativeArray<float> outputMoisture;

    //private void Start()
    //{
    //    int index = 0;
    //    input = new NativeArray<int>(Chunk.chunkSize * Chunk.chunkSize, Allocator.Persistent);
    //    for (int i = 0; i < input.Length; i++)
    //    {
    //        if (i % 64 == 0)
    //            index = 0;
    //        input[i] = index;
    //        index++;
    //    }
    //    outputHeight = new NativeArray<float>(Chunk.chunkSize * Chunk.chunkSize, Allocator.Persistent);
    //    outputTemperature = new NativeArray<float>(Chunk.chunkSize * Chunk.chunkSize, Allocator.Persistent);
    //    outputMoisture = new NativeArray<float>(Chunk.chunkSize * Chunk.chunkSize, Allocator.Persistent);
    //}

    //private void OnApplicationQuit()
    //{
    //    input.Dispose();
    //    outputHeight.Dispose();
    //    outputMoisture.Dispose();
    //    outputTemperature.Dispose();
    //}

    //private IEnumerator jobjob(Chunk chunk)
    //{
    //    int index = 0;
    //    SaveEdges(chunk);
    //    var job = new MyJob()
    //    {
    //        input = input,
    //        chunkSize = Chunk.chunkSize,
    //        moistureScale = Chunk.moistureScale,
    //        moistureSeed = moistureSeed,
    //        moistureOffset = chunk.offsetMoisture,
    //        temperatureScale = Chunk.temperatureScale,
    //        temperatureSeed = temperatureSeed,
    //        temperatureOffset = chunk.offsetTemperature,
    //        topographyScale = chunk.topographyScale,
    //        topographySeed = topographySeed,
    //        topographyOffset = chunk.offsetTopography,
    //        outputHeight = outputHeight,
    //        outputMoisture = outputMoisture,
    //        outputTemperature = outputTemperature
    //    };

    //    JobHandle jobhandle = job.Schedule(input.Length, 512);
    //    jobhandle.Complete();

    //    Debug.Log(outputHeight[0]);
    //    int breakGeneration = (int)(Chunk.chunkSize * 0.2f);
    //    int y = 0;
    //    index = 0;
    //    float[] heightCells = outputHeight.ToArray();
    //    float[] moistureCells = outputMoisture.ToArray();
    //    float[] temperatureCells = outputTemperature.ToArray();

    //    float heightCell, moistureCell, temperatureCell;
    //    Vector3Int[] positionArray = new Vector3Int[Chunk.chunkSize * Chunk.chunkSize];
    //    TileBase[] tileArray = new TileBase[Chunk.chunkSize * Chunk.chunkSize];
    //    for (int x = 0; x < Chunk.chunkSize; x++)
    //    {
    //        for (int y = 0; y < Chunk.chunkSize; y++)
    //        {
    //            heightCell = PerlinNoise.Calculate(x, y, chunk.topographyScale, topographySeed, chunk.offsetTopography, Chunk.chunkSize);
    //            moistureCell = PerlinNoise.Calculate(x, y, Chunk.moistureScale, moistureSeed, chunk.offsetMoisture, Chunk.chunkSize);
    //            temperatureCell = PerlinNoise.Calculate(x, y, Chunk.temperatureScale, temperatureSeed, chunk.offsetTemperature, Chunk.chunkSize);
    //            Field field = ChooseFieldType(chunk.TerrainBiome, heightCell, moistureCell, temperatureCell);
    //            chunk.Fields[x][y] = field.ID;
    //            positionArray[x * Chunk.chunkSize + y] = new Vector3Int((int)(-x + chunk.transform.position.x + Chunk.chunkSizeHalf),
    //            (int)(-y + chunk.transform.position.y + Chunk.chunkSizeHalf), 0);
    //            tileArray[x * Chunk.chunkSize + y] = field.TileRepresentation;
    //        }
    //    }

    //    chunk.tilemap.SetTiles(positionArray, tileArray);
    //    //for (int i = 0; i < input.Length; i++)
    //    //{
    //    //    y = i % 64;
    //    //    Field field = ChooseFieldType(chunk.TerrainBiome, heightCells[i], moistureCells[i], temperatureCells[i]);
    //    //    chunk.tilemap.SetTile(new Vector3Int((int)(-index + chunk.transform.position.x + Chunk.chunkSizeHalf),
    //    //        (int)(-y + chunk.transform.position.y + Chunk.chunkSizeHalf), 0), field.TileRepresentation);
    //    //    //chunk.tilemap.SetTiles
    //    //    if (y == 63)
    //    //        index++;
    //    //    if (index == breakGeneration)
    //    //    {
    //    //        breakGeneration += breakGeneration;
    //    //        yield return null;
    //    //    }
    //    //}
    //    //yield return null;

    //}

    //[BurstCompile]
    //public struct MyJob : IJobParallelFor
    //{
    //    [ReadOnly]
    //    public NativeArray<int> input;


    //    public int chunkSize;
    //    public float moistureScale;
    //    public float moistureSeed;
    //    public Vector2 moistureOffset;
    //    public float temperatureScale;
    //    public float temperatureSeed;
    //    public Vector2 temperatureOffset;
    //    public float topographyScale;
    //    public float topographySeed;
    //    public Vector2 topographyOffset;


    //    public NativeArray<float> outputHeight;
    //    public NativeArray<float> outputTemperature;
    //    public NativeArray<float> outputMoisture;

    //    public void Execute(int index)
    //    {
    //        outputHeight[index] = PerlinNoise.Calculate(index / chunkSize, input[index], topographyScale, topographySeed, topographyOffset, chunkSize);
    //        outputMoisture[index] = PerlinNoise.Calculate(index / chunkSize, input[index], moistureScale, moistureSeed, moistureOffset, chunkSize);
    //        outputTemperature[index] = PerlinNoise.Calculate(index / chunkSize, input[index], temperatureScale, temperatureSeed, temperatureOffset, chunkSize);
    //    }
    //}

    #endregion
}
