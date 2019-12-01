using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    private Dictionary<Vector3, Chunk> setOfChunks;
    public Dictionary<Vector3, Chunk> visibleChunks { get; set; }
    private BiomeGridGenerator biomeGridGenerator;
    private HyperGridGenerator hyperGridGenerator;
    [SerializeField] private ChunkGenerator chunkGenerator;
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController camer;

    private int cameraRange = 10;

    private void Awake()
    {
        setOfChunks = new Dictionary<Vector3, Chunk>();
        visibleChunks = new Dictionary<Vector3, Chunk>();
        biomeGridGenerator = gameObject.AddComponent<BiomeGridGenerator>();
        hyperGridGenerator = gameObject.AddComponent<HyperGridGenerator>();
        camer.AddChunk += addToVisibleChunk;
        camer.EraseChunk += eraseInvisibleChunk;
    }
    
    private void Start()
    {
        biomeGridGenerator.GenerateGridEdges();
        chunkGenerator.GenerateStartChunk(setOfChunks, visibleChunks);
    }
    
    public Transform getChunkTransform(Vector2 id)
    {
        return visibleChunks[id].transform;
    }
    
    public void addToVisibleChunk(Vector2 position, Chunk chunk)
    {
        chunk.gameObject.SetActive(true);
        chunk.tilemap.gameObject.SetActive(true);
        if (!visibleChunks.ContainsKey(position))
            visibleChunks.Add(position, chunk);
    }

    public void eraseInvisibleChunk(Vector2 position, Chunk chunk)
    {
        visibleChunks.Remove(position);
        chunk.tilemap.gameObject.SetActive(false);
    }

    private bool IsChunkToSpawn()
    {
        if (player.currentChunk == null || player.currentChunk.NOTgenerationNeeded) return false;
        Vector3 currentChunkPosition = player.currentChunk.transform.position;
        Vector3 playerPosition = player.transform.position;
        int noSpawnChunkAreaRadius = (int)(Chunk.chunkSizeHalf - camer.rangeCamera);
        return (playerPosition.x + camer.rangeCamera < currentChunkPosition.x - noSpawnChunkAreaRadius ||
            playerPosition.x - camer.rangeCamera > currentChunkPosition.x + noSpawnChunkAreaRadius ||
            playerPosition.y - camer.rangeCamera < currentChunkPosition.y + noSpawnChunkAreaRadius ||
            playerPosition.y + camer.rangeCamera > currentChunkPosition.y - noSpawnChunkAreaRadius);
    }

    public void OnUpdate()
    {
        if (IsChunkToSpawn())
        {
            chunkGenerator.generateChunk(setOfChunks, visibleChunks, player.currentChunk, player.transform.position, camer.rangeCamera);
        }
        if (biomeGridGenerator.IsIncreaseGrid(Vector2.Distance(player.transform.position, Vector3.zero)))
        {
            if (hyperGridGenerator.IsIncreaseGrid(Vector2.Distance(player.transform.position, Vector3.zero)))
            {
                hyperGridGenerator.GenerateGridEdges();
            }
            biomeGridGenerator.GenerateGridEdges();
        }
    }
}
