using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkGenerator : MonoBehaviour
{
    private List<Vector2> chunkToGenerate;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private TileGenerator tileGenerator;
    [SerializeField] private Grid grid;
    

    private void Awake()
    {
        chunkToGenerate = new List<Vector2>();
    }

    public void GenerateStartChunk(Dictionary<Vector3, Chunk> setOfChunks, Dictionary<Vector3, Chunk> visibleChunks)
    {
        GameObject newChunkObject = new GameObject();
        newChunkObject.transform.position = Vector2.zero;
        newChunkObject.transform.SetParent(chunkParent);
        Chunk newChunk = newChunkObject.AddComponent<Chunk>();
        newChunk.grid = grid;
        newChunk.tilemap.transform.SetParent(grid.transform);
        tileGenerator.GenerateTiles(newChunk, visibleChunks);
        visibleChunks.Add(Vector2.zero, newChunk);
        setOfChunks.Add(Vector2.zero, newChunk);
    }

    private void AddPotentialDirections(float cameraRange, Vector2 currentChunkPosition, Vector2 playerPosition, List<Direction> potentialDirections)
    {

        //var a = playerPosition - currentChunkPosition;
        //if(Mathf.Abs(a)>cameraRange)

        if (playerPosition.x > currentChunkPosition.x + Chunk.chunkSizeHalf - cameraRange)
        {
            potentialDirections.Add(Direction.E);
        }
        if (playerPosition.x < currentChunkPosition.x - Chunk.chunkSizeHalf + cameraRange)
        {
            potentialDirections.Add(Direction.W);
        }
        if (playerPosition.y > currentChunkPosition.y + Chunk.chunkSizeHalf - cameraRange)
        {
            potentialDirections.Add(Direction.N);
        }
        if (playerPosition.y < currentChunkPosition.y - Chunk.chunkSizeHalf + cameraRange)
        {
            potentialDirections.Add(Direction.S);
        }
    }

    private void LoadChunksToGenerate(int cameraRange, Vector3 playerPosition, Chunk currentChunk)
    {
        List<Direction> potentialDirections= new List<Direction>();
        AddPotentialDirections(cameraRange, currentChunk.transform.position, playerPosition, potentialDirections);
        
        NeighbourChunk neighbour;
        foreach (var direction in potentialDirections)
        {            
            neighbour=Array.Find(currentChunk.Neighbours4Chunks, (n => n.direction == direction));
            if(!neighbour.exist)
            {
                neighbour.exist = true;
                chunkToGenerate.Add(neighbour.position);
            }
        }
    }

    private void CheckNeighbours(Dictionary<Vector3, Chunk> chunks, Chunk newChunk, Dictionary<Vector3, Chunk> visibleChunks)
    {
        Chunk chunk;
        int neighbourIndex;
        foreach (var neighbour in newChunk.Neighbours4Chunks)
        {
            if (chunks.TryGetValue(neighbour.position, out chunk))// visible chunks? wymaga zwiększonej strefy widoczności
            {
                neighbourIndex = (int)neighbour.opposedDirection;
                newChunk.Neighbours4Chunks[(int)neighbour.direction].exist = true;
                newChunk.Neighbours4Chunks[(int)neighbour.direction].chunk = chunk;
                chunk.Neighbours4Chunks[neighbourIndex].exist = true;
                chunk.Neighbours4Chunks[neighbourIndex].chunk = newChunk;
            } 
        } 
    }

    private void DiagonallyChunkGenerate(Dictionary<Vector3, Chunk> chunks, Dictionary<Vector3, Chunk> visibleChunks, Chunk currentChunk)
    {
        Vector2 positionDiagonallyChunk = currentChunk.checkDiagonallChunkNeeded(chunks);
        if (!chunks.ContainsKey(positionDiagonallyChunk) && positionDiagonallyChunk != Vector2.zero)// to samo co wyżej z widzialnymi
        {
            CreateChunk(positionDiagonallyChunk,chunks, visibleChunks);
        //Debug.Log("diagorigin "+ newOriginChunkForDiagonal.transform.position);
        }

    }

    private void CreateChunk(Vector2 chunkPosition, Dictionary<Vector3, Chunk> chunks, Dictionary<Vector3, Chunk> visibleChunks)
    {
        GameObject newChunkObject = new GameObject();
        newChunkObject.transform.position = chunkPosition;
        newChunkObject.transform.SetParent(chunkParent);
        Chunk newChunk = newChunkObject.AddComponent<Chunk>();
        newChunk.grid = grid;
        newChunk.tilemap.transform.SetParent(grid.transform);
        tileGenerator.GenerateTiles(newChunk, visibleChunks);
        chunks.Add(chunkPosition, newChunk);
        visibleChunks.Add(chunkPosition, newChunk);
        CheckNeighbours(chunks, newChunk, visibleChunks);
    }

    public void GenerateChunk( Dictionary<Vector3, Chunk> chunks, Dictionary<Vector3, Chunk> visibleChunks, Chunk currentChunk, Vector3 playerPosition, int camerRange)
    {
        LoadChunksToGenerate(camerRange, playerPosition, currentChunk);
        foreach (var newChunkPosition in chunkToGenerate)
        {
            CreateChunk(newChunkPosition,chunks, visibleChunks);
        }
        DiagonallyChunkGenerate(chunks, visibleChunks, currentChunk);
        chunkToGenerate.Clear();
     }
}