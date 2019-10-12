using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinNoise
{
    public static float Calculate(float x, float y, float scale, float seed, Vector2 offset, int chunkSize)
    {
        return Mathf.PerlinNoise((seed + x) / chunkSize * scale + offset.x,
            (seed + y) / chunkSize * scale + offset.y);
    }
}
