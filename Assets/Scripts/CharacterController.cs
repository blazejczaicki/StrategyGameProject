using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [SerializeField] private GeneratorManager generator;
    protected ObjectMovement movement;
    protected CharacterStats stats;
    protected Chunk _currentChunk;
    public Chunk currentChunk => _currentChunk;

    public abstract void OnUpdate();

    protected Chunk CheckOnWhichChunkYouStayed(Vector2 position)
    {
        Vector2 newCurrentChunkPos = new Vector2();
        newCurrentChunkPos.x = Mathf.Round(position.x / Chunk.chunkSize) * Chunk.chunkSize;
        newCurrentChunkPos.y = Mathf.Round(position.y / Chunk.chunkSize) * Chunk.chunkSize;
        return generator.getChunkTransform(newCurrentChunkPos).GetComponent<Chunk>();
    }
}
