using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoistureAreaType { wet, mid, dry }

[System.Serializable]
public class Moisture : MonoBehaviour, ILayerCalculation
{
    public List<Temperature> moistureKinds;

    private float dry = 0.3f;
    private float mid = 0.7f;
    private float wet = 1f;

    public int CalculateLayer(Biome biome,float tileHeight)
    {
        MoistureAreaType moiType;
        if (tileHeight <= dry)
            moiType = MoistureAreaType.dry;
        else if (tileHeight <= mid)
            moiType = MoistureAreaType.mid;
        else
            moiType = MoistureAreaType.wet;
        return (int)moiType;
    }

    public Temperature GetTemperatureForMoisture(Biome biome,float tileHeight)
    {
        return moistureKinds[CalculateLayer(biome, tileHeight)];
    }
}
