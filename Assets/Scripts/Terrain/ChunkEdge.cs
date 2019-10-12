using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ChunkEdge
{
    protected int x, y;
    public float[] edge { get; set; }

    public ChunkEdge()
    {
        edge = new float[Chunk.chunkSize];
    }

    public void CalculateEdge(Chunk chunk, float topographySeed, Vector2 neighbourPosition)
    {
        float scale = 0;
        Vector2 offset = Vector2.zero;
        CalculateScaleOffset(chunk, topographySeed, neighbourPosition, ref scale, ref offset);
        CalculatePerlineEdge(scale, topographySeed, offset);
    }

    public void SaveEdge(Chunk chunk, float topographySeed)
    {
        CalculatePerlineEdge(chunk.topographyScale, topographySeed, chunk.offsetTopography);
    }

    private void CalculateScaleOffset(Chunk chunk, float topographySeed, Vector2 neighbourPosition, ref float scale, ref Vector2 offset)
    {
        Biome b = NearnestNeighbourBiome.Calculate(neighbourPosition);
        scale = b.topographyScaleOffset;
        offset = new Vector2(-chunk.transform.position.x / Chunk.chunkSize * scale,
                -chunk.transform.position.y / Chunk.chunkSize * scale);
    }

    protected abstract void CalculatePerlineEdge(float scale, float seed, Vector2 offset);
}

public class NorthEdge : ChunkEdge
{
    protected override void CalculatePerlineEdge(float scale, float seed, Vector2 offset)
    {
        for (int i = 0; i < Chunk.chunkSize; i++)
        {
            edge[i] = PerlinNoise.Calculate(i, 0, scale, seed, offset, Chunk.chunkSize);
        }
    }
}
public class SouthEdge : ChunkEdge
{
    protected override void CalculatePerlineEdge(float scale, float seed, Vector2 offset)
    {
        for (int i = 0; i < Chunk.chunkSize; i++)
        {
            edge[i] = PerlinNoise.Calculate(i, Chunk.chunkSize - 1, scale, seed, offset, Chunk.chunkSize);
        }
    }
}
public class EastEdge : ChunkEdge
{
    protected override void CalculatePerlineEdge(float scale, float seed, Vector2 offset)
    {
        for (int i = 0; i < Chunk.chunkSize; i++)
        {
            edge[i] = PerlinNoise.Calculate(0, i, scale, seed, offset, Chunk.chunkSize);
        }
    }
}
public class WestEdge : ChunkEdge
{
    protected override void CalculatePerlineEdge(float scale, float seed, Vector2 offset)
    {
        for (int i = 0; i < Chunk.chunkSize; i++)
        {
            edge[i] = PerlinNoise.Calculate(Chunk.chunkSize - 1, i, scale, seed, offset, Chunk.chunkSize);
        }
    }
}