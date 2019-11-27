using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Dictionary<Vector3, Chunk> setOfChunks;
    public Dictionary<Vector3, Chunk> visibleChunks { get; set; }
    private BiomeGridGenerator biomeGridGenerator;
    private HyperGridGenerator hyperGridGenerator;
    [SerializeField] private ChunkGenerator chunkGenerator;
    [SerializeField] private PlayerController player;
    private int cameraRange = 10;

    [SerializeField] private Grid grid;
    public Grid gridd
    {
        get { return grid; }
        set { grid = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        setOfChunks = new Dictionary<Vector3, Chunk>();
        visibleChunks = new Dictionary<Vector3, Chunk>();
        biomeGridGenerator = gameObject.AddComponent<BiomeGridGenerator>();
        hyperGridGenerator = gameObject.AddComponent<HyperGridGenerator>();
    }


    private void Start()
    {
        biomeGridGenerator.GenerateGridEdges();
        chunkGenerator.GenerateStartChunk(setOfChunks);
    }


    public Transform getChunkTransform(Vector2 id)
    {
        return visibleChunks[id].transform;
    }


     public void addToVisibleChunk(Vector2 position, Chunk chunk)
    {
        chunk.gameObject.SetActive(true);
        chunk.tilemap.gameObject.SetActive(true);
        if(!visibleChunks.ContainsKey(position))
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
        Vector3 playerPosition= player.transform.position;
        int noSpawnChunkAreaRadius = (int)(Chunk.chunkSizeHalf - cameraRange);
        return (playerPosition.x + cameraRange < currentChunkPosition.x - noSpawnChunkAreaRadius ||
            playerPosition.x - cameraRange > currentChunkPosition.x + noSpawnChunkAreaRadius ||
            playerPosition.y - cameraRange < currentChunkPosition.y + noSpawnChunkAreaRadius ||
            playerPosition.y + cameraRange > currentChunkPosition.y - noSpawnChunkAreaRadius);
    }

    void Update()
    {
        
        if (IsChunkToSpawn() )
        {
            chunkGenerator.generateChunk(setOfChunks, visibleChunks, player.currentChunk, player.transform.position, cameraRange);
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
