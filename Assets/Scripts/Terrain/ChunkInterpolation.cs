using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChunkInterpolation
{
    public static float Calculate(Vector2Int xy,  float Q11, float Q12, float Q21, float Q22,
        float centerOfInterpolation, Vector2Int coordsOfCenter, ref Biome currentBiome, Biome[] neighboursBiomes)
    {
        float xxE, xxW, xxN, xxS, N, S, W, E;
        int max = Chunk.chunkSize - 1;
       
        float rightVerticalFactor, leftVerticalFactor;
        rightVerticalFactor = coordsOfCenter.y / (float)coordsOfCenter.x;
        leftVerticalFactor = coordsOfCenter.y / (float)(Chunk.chunkSize - coordsOfCenter.x);

        if (xy.x < coordsOfCenter.x)
            xxN = rightVerticalFactor * xy.x;
        else
            xxN = leftVerticalFactor * (Chunk.chunkSize - xy.x);
        if (xy.y > xxN)//+1
            N = 0;
        else
        {
            N = ((xy.y / (xxN + 1)) * (centerOfInterpolation - Q11) + Q11);
            currentBiome = neighboursBiomes[(int)Direction.N];
        }

        rightVerticalFactor = (Chunk.chunkSize - coordsOfCenter.y) / (float)coordsOfCenter.x;
        leftVerticalFactor = (Chunk.chunkSize - coordsOfCenter.y) / (float)(Chunk.chunkSize - coordsOfCenter.x);


        if (xy.x < coordsOfCenter.x)
            xxS = rightVerticalFactor * xy.x;
        else
            xxS = leftVerticalFactor * (Chunk.chunkSize - xy.x);
        if (xy.y < Chunk.chunkSize - xxS)
            S = 0;
        else
        {
            S = ((Chunk.chunkSize - xy.y) / (xxS + 1) * (centerOfInterpolation - Q22) + Q22);
            currentBiome = neighboursBiomes[(int)Direction.S];
        }

        float upHorizontalFactor, downHorizontalFactor;
        upHorizontalFactor = coordsOfCenter.x / (float)coordsOfCenter.y;
        downHorizontalFactor = coordsOfCenter.x / (float)(Chunk.chunkSize - coordsOfCenter.y);

        if (xy.y < coordsOfCenter.y)
            xxE = upHorizontalFactor * xy.y;
        else
            xxE = downHorizontalFactor * (Chunk.chunkSize - xy.y);
        if (xy.x > xxE)
            E = 0;
        else
        {
            E = ((xy.x / (xxE + 1)) * (centerOfInterpolation - Q21) + Q21);
            currentBiome = neighboursBiomes[(int)Direction.E];
        }

        upHorizontalFactor = (Chunk.chunkSize - coordsOfCenter.x) / (float)coordsOfCenter.y;
        downHorizontalFactor = (Chunk.chunkSize - coordsOfCenter.x) / (float)(Chunk.chunkSize - coordsOfCenter.y);

        if (xy.y < coordsOfCenter.y)
            xxW = upHorizontalFactor * xy.y;
        else
            xxW = downHorizontalFactor * (Chunk.chunkSize - xy.y);
        if (xy.x < Chunk.chunkSize - xxW)
            W = 0;
        else
        {
            W = ((Chunk.chunkSize - xy.x) / (xxW + 1) * (centerOfInterpolation - Q12) + Q12);
            currentBiome = neighboursBiomes[(int)Direction.W];
        }

        if (N != 0 && (E != 0 || W != 0) || S != 0 && (E != 0 || W != 0))
        {
            return (N + S + W + E) / 2;
        }


        return N + S + W + E;
    }

    //public static float Calculate(Vector2Int xy, Vector2Int xy1, Vector2Int xy2, float Q11, float Q12, float Q21, float Q22, float centerOfInterpolation, Vector2Int coordsOfCenter, ref bool bb)
    //{
    //    float R1, R2;
    //    R1 = ((xy2.x - xy.x) / (float)(xy2.x - xy1.x)) * Q11 + ((xy.x - xy1.x) / (float)(xy2.x - xy1.x)) * Q21;
    //    R2 = ((xy2.x - xy.x) / (float)(xy2.x - xy1.x)) * Q12 + ((xy.x - xy1.x) / (float)(xy2.x - xy1.x)) * Q22;
    //    return ((xy2.y - xy.y) / (float)(xy2.y - xy1.y)) * R1 + ((xy.y - xy1.y) / (float)(xy2.y - xy1.y)) * R2;
    //}
}
