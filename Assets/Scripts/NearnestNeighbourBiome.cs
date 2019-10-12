using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class NearnestNeighbourBiome
{
    private static float Distance(Vector2 position1, Vector2 position2)
    {
        return Mathf.Abs((position1.x - position2.x) * (position1.x - position2.x) +
            (position1.y - position2.y) * (position1.y - position2.y));
    }

    public static Biome Calculate(Vector2 chunkPosition )
    {
        //Debug.Log("XXX");
        Biome nearnestBiome = NormalBiomeGenerator.biomes.First().Value;
        float min = 0, dis = 0;
        min = Distance(chunkPosition, NormalBiomeGenerator.biomes.First().Key);
        foreach (var biome in NormalBiomeGenerator.biomes)                            //todo staly kontener z pobliskimi biomami
        {
            dis = Distance(chunkPosition, biome.Key);
            if (dis<min)
            {
                nearnestBiome = biome.Value;
                min = dis;
            }
        }

        return nearnestBiome;       
    }
}
