using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObject : MonoBehaviour
{
    public Chunk CurrentChunk { get; set; }
    public Transform CurrentChunkTransform { get; set; }

    protected void CheckOnWhichChunkYouStayed()
    {
        Vector2 newCurrentChunkPos = new Vector2();

        newCurrentChunkPos.x = Mathf.Round(transform.position.x / Chunk.chunkSize) * Chunk.chunkSize;
        newCurrentChunkPos.y = Mathf.Round(transform.position.y / Chunk.chunkSize) * Chunk.chunkSize;

        CurrentChunkTransform = GameManager.instance.getChunkTransform(newCurrentChunkPos);
        CurrentChunk = CurrentChunkTransform.GetComponent<Chunk>();
    }
}
