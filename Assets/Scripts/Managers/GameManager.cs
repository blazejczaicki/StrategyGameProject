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


    void Update()
    {
        
        if (PlayerManager.instance.IsChunkToSpawn() )
        {
            chunkGenerator.generateChunk(setOfChunks, visibleChunks);
        }
        if (biomeGridGenerator.IsIncreaseGrid(PlayerManager.instance.DistanceFromCenter()))
        {
            if (hyperGridGenerator.IsIncreaseGrid(PlayerManager.instance.DistanceFromCenter()))
            {
                hyperGridGenerator.GenerateGridEdges();
            }
            biomeGridGenerator.GenerateGridEdges();
        }
    }
}
