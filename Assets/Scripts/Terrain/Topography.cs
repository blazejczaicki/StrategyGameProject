using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeightAreaType { deepWater, shallowWater, lowLand, upLand, moutain }

[System.Serializable]
public class Topography : MonoBehaviour, ILayerCalculation
{
    public List<Field> terrainKinds;
    private float deepWaterLayer = -0.5f;
    private float shallowWaterLayer = -0.0f;
    private float lowLandLayer = 0.3f;
    private float upLandLayer = 0.6f;
    private float moutainLayer = 1.0f;


    public int CalculateLayer(Biome biome, float tileHeight)
    {
        HeightAreaType heiType;
        if (tileHeight <= deepWaterLayer)//+biome.deepWaterOffset
            heiType = HeightAreaType.deepWater;
        else if (tileHeight <= shallowWaterLayer )//+ biome.shallowWaterOffset
            heiType = HeightAreaType.shallowWater;
        else if (tileHeight <= lowLandLayer )//+ biome.lowLandOffset
            heiType = HeightAreaType.lowLand;
        else if (tileHeight <= upLandLayer+ biome.upLandOffset)
            heiType = HeightAreaType.upLand;
        else
            heiType = HeightAreaType.moutain;
        return (int)heiType;
    }

    public Field GetFieldForTopology(Biome biome, float tileHeight)
    {
        return terrainKinds[CalculateLayer(biome, tileHeight)];
    }
}
