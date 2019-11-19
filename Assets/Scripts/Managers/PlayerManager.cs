using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] private Transform player;
    public Transform Player{get { return player; }}

    public Chunk CurrentChunk { get; set; }
    public Transform CurrentChunkTransform { get; set; }

    public int CameraRange { get; set; }
    public float rangeOfVisibility { get; set; }

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
        rangeOfVisibility = Chunk.chunkSize * 2;
    }

    public void SetStartChunk(Dictionary<Vector3, Chunk> setOfChunks)
    {
        CurrentChunkTransform = setOfChunks[Vector2.zero].transform;
        CurrentChunk = CurrentChunkTransform.GetComponent<Chunk>();
        CameraRange = 10;
    }

    private void Update()
    {
        //if (CurrentChunk.Neighbours4Chunks[(int)Direction.N].chunk == null)
        //    Debug.Log("xddd");
        //else
        //    Debug.Log(CurrentChunk.Neighbours4Chunks[(int)Direction.N].direction);
        //if (CurrentChunk.Neighbours4Chunks[(int)Direction.S].chunk == null)
        //    Debug.Log("xddd");
        //else
        //    Debug.Log(CurrentChunk.Neighbours4Chunks[(int)Direction.S].direction);
        //if (CurrentChunk.Neighbours4Chunks[(int)Direction.E].chunk == null)
        //    Debug.Log("xddd");
        //else
        //    Debug.Log(CurrentChunk.Neighbours4Chunks[(int)Direction.E].direction);
        //if (CurrentChunk.Neighbours4Chunks[(int)Direction.W].chunk == null)
        //    Debug.Log("xddd");
        //else
        //    Debug.Log(CurrentChunk.Neighbours4Chunks[(int)Direction.W].direction);
        CheckOnWhichChunkYouStayed();
       // Debug.Log(CurrentChunk.GetFieldNumber(player.position));
    }

    public float DistanceFromCenter()
    {
        return player.position.x * player.position.x + player.position.y * player.position.y;
    }

    private void CheckOnWhichChunkYouStayed()
    {
        Vector2 newCurrentChunkPos = new Vector2();

        newCurrentChunkPos.x=Mathf.Round(player.transform.position.x / Chunk.chunkSize)* Chunk.chunkSize;
        newCurrentChunkPos.y = Mathf.Round(player.transform.position.y / Chunk.chunkSize)* Chunk.chunkSize;

        CurrentChunkTransform = GameManager.instance.getChunkTransform(newCurrentChunkPos);
        CurrentChunk = CurrentChunkTransform.GetComponent<Chunk>();
    }

    public bool IsChunkToSpawn()
    {
        if (CurrentChunk.NOTgenerationNeeded) return false;

        int noSpawnChunkAreaRadius = (int)(Chunk.chunkSizeHalf - CameraRange);
        return (player.position.x + CameraRange < CurrentChunkTransform.position.x - noSpawnChunkAreaRadius ||
            player.position.x - CameraRange > CurrentChunkTransform.position.x + noSpawnChunkAreaRadius ||
            player.position.y - CameraRange < CurrentChunkTransform.position.y + noSpawnChunkAreaRadius ||
            player.position.y + CameraRange > CurrentChunkTransform.position.y - noSpawnChunkAreaRadius);
    }
}
